using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.AspNetCore;
using Minio.DataModel.Args;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Providers
{
    public class MinioProvider : IFileProvider
    {
        private readonly IMinioClient _minioClient;
        private readonly ILogger<MinioProvider> _logger;

        public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
        {
            _minioClient = minioClient;
            _logger = logger;
        }

        public async Task<Result<string, Error>> DeleteFile(
            FileMetaData fileMetaData,
            CancellationToken cancellationToken)
        {
            try
            {
                var removeObjectArgs = new RemoveObjectArgs()
                    .WithBucket(fileMetaData.Bucketname)
                    .WithObject(fileMetaData.ObjectName.ToString());

                await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);

                return fileMetaData.ObjectName.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Fail to delete file in minio");
                return Error.Failure("file.delete", "Fail to delete file in minio");
            }
        }

        public async Task<Result<string, Error>> GetFileUrl(FileMetaData fileMetaData, CancellationToken cancellationToken)
        {
            try
            {
                var presignedGetObjectArgs = new PresignedGetObjectArgs()
                    .WithBucket(fileMetaData.Bucketname)
                    .WithObject(fileMetaData.ObjectName.ToString())
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

        public async Task<Result<string, Error>> UploadFile(
            FileData fileData,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var bucketExistArgs = new BucketExistsArgs()
                                .WithBucket("photos");

                var bucketExist = await _minioClient.BucketExistsAsync(bucketExistArgs, cancellationToken);
                if (bucketExist == false)
                {
                    var makeBucketArgs = new MakeBucketArgs()
                        .WithBucket("photos");

                    await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
                }

                var path = Guid.NewGuid();

                var putObjectArs = new PutObjectArgs()
                    .WithBucket("photos")
                    .WithStreamData(fileData.Stream)
                    .WithObjectSize(fileData.Stream.Length)
                    .WithObject(path.ToString());

                var result = await _minioClient.PutObjectAsync(putObjectArs, cancellationToken);

                return result.ObjectName;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Fail to upload file in minio");
                return Error.Failure("file.upload", "Fail to upload file in minio");
            }


        }
    }
}
