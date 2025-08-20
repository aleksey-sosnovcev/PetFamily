using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.AspNetCore;
using Minio.DataModel.Args;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Providers
{
    public class MinioProvider : IFileProvider
    {
        private const int MAX_DEGREE_OF_PARALLELISM = 10;

        private readonly IMinioClient _minioClient;
        private readonly ILogger<MinioProvider> _logger;

        public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
        {
            _minioClient = minioClient;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<FilePath>, ErrorList>> DeleteFiles(
            IEnumerable<StreamFileData> filesData,
            CancellationToken cancellationToken = default)
        {
            var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);

            var filesList = filesData.ToList();

            try
            {
                var tasks = filesList.Select(async file =>
                    await RemoveObject(file, semaphoreSlim, cancellationToken));

                var pathResult = await Task.WhenAll(tasks);
                if (pathResult.Any(p => p.IsFailure))
                    return pathResult.First().Error;

                var result = pathResult.Select(p => p.Value).ToList();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Fail to remove file in minio");
                return Error.Failure("file.remove", "Fail to remove file in minio").ToErrorList();
            }
        }

        public async Task<Result<string, ErrorList>> DeleteFile(
                    FileData fileData,
                    CancellationToken cancellationToken)
        {
            try
            {
                var removeObjectArgs = new RemoveObjectArgs()
                    .WithBucket(fileData.BucketName)
                    .WithObject(fileData.FilePath.PathToStorage);

                await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);

                return fileData.FilePath.PathToStorage;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Fail to delete file in minio");
                return Error.Failure("file.delete", "Fail to delete file in minio").ToErrorList();
            }
        }

        public async Task<Result<IReadOnlyList<FilePath>, ErrorList>> UploadFile(
                    IEnumerable<StreamFileData> filesData,
                    CancellationToken cancellationToken = default)
        {
            var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM);

            var filesList = filesData.ToList();

            try
            {
                await IfBucketsNotExistCreateBucket(filesList, cancellationToken);

                var tasks = filesList.Select(async file =>
                    await PutObject(file, semaphoreSlim, cancellationToken));

                var pathResult = await Task.WhenAll(tasks);
                if (pathResult.Any(p => p.IsFailure))
                    return pathResult.First().Error;

                var result = pathResult.Select(p => p.Value).ToList();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Fail to upload file in minio");
                return Error.Failure("file.upload", "Fail to upload file in minio").ToErrorList();
            }
        }

        public async Task<Result<string, Error>> GetFileUrl(FileData fileData, CancellationToken cancellationToken)
        {
            try
            {
                var presignedGetObjectArgs = new PresignedGetObjectArgs()
                    .WithBucket(fileData.BucketName)
                    .WithObject(fileData.FilePath.PathToStorage)
                    .WithExpiry(60 * 60 * 24);

                if (presignedGetObjectArgs is null)
                    return Error.NotFound("object.not.found", "URL not found");

                var result = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Fail to get URl file in minio");
                return Error.Failure("file.get.url", "Fail to get URl file in minio");
            }
        }

        private async Task<Result<FilePath, ErrorList>> PutObject(
        StreamFileData fileData,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
        {
            await semaphoreSlim.WaitAsync(cancellationToken);

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(fileData.FileData.BucketName)
                .WithStreamData(fileData.Stream)
                .WithObjectSize(fileData.Stream.Length)
                .WithObject(fileData.FileData.FilePath.PathToStorage);

            try
            {
                await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

                return fileData.FileData.FilePath;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Fail to upload file in minio with path {path} in buket {bucket}",
                    fileData.FileData.FilePath.PathToStorage,
                    fileData.FileData.BucketName);

                return Error.Failure("file.upload", "Fail to upload file to minio")
                    .ToErrorList();
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        private async Task<Result<FilePath, ErrorList>> RemoveObject(
        StreamFileData fileData,
        SemaphoreSlim semaphoreSlim,
        CancellationToken cancellationToken)
        {
            await semaphoreSlim.WaitAsync(cancellationToken);

            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(fileData.FileData.BucketName)
                .WithObject(fileData.FileData.FilePath.PathToStorage);

            try
            {
                await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);

                return fileData.FileData.FilePath;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Fail to remove file in minio with path {path} in buket {bucket}",
                    fileData.FileData.FilePath.PathToStorage,
                    fileData.FileData.BucketName);

                return Error.Failure("file.remove", "Fail to remove file to minio")
                    .ToErrorList();
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        private async Task IfBucketsNotExistCreateBucket(
        IEnumerable<StreamFileData> filesData,
        CancellationToken cancellationToken)
        {
            HashSet<string> bucketsName = [.. filesData.Select(file => file.FileData.BucketName)];

            foreach (var bucketName in bucketsName)
            {
                var bucketExistArgs = new BucketExistsArgs()
                    .WithBucket(bucketName);

                var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
                if (bucketExist == false)
                {
                    var makeBucketArgs = new MakeBucketArgs()
                        .WithBucket(bucketName);

                    await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
                }
            }
        }
    }
}
