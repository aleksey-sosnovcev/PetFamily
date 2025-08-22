using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Providers
{
    public interface IFileProvider
    {
        Task<Result<IReadOnlyList<FilePath>, ErrorList>> UploadFile(
            IEnumerable<StreamFileData> filesData,
            CancellationToken cancellationToken = default);
        Task<Result<string, ErrorList>> DeleteFile(
            FileData fileData,
            CancellationToken cancellationToken);
        Task<Result<IReadOnlyList<FilePath>, ErrorList>> DeleteFiles(
            IEnumerable<StreamFileData> filesData,
            CancellationToken cancellationToken = default);

        Task<UnitResult<Error>> RemoveFile(
            FileData fileDatas,
            CancellationToken cancellationToken = default);
        Task<Result<string, Error>> GetFileUrl(
            FileData fileData,
            CancellationToken cancellationToken);
    }
}
