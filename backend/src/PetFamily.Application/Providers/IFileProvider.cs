using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Providers
{
    public interface IFileProvider
    {
        Task<Result<string, Error>> UploadFile(FileData fileData, CancellationToken cancellationToken = default);
        Task<Result<string, Error>> DeleteFile(FileMetaData fileMetaData, CancellationToken cancellationToken);
        Task<Result<string, Error>> GetFileUrl(FileMetaData fileMetaData, CancellationToken cancellationToken);
    }
}
