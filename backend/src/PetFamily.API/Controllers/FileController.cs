using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using PetFamily.API.Extensions;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Infrastructure.Options;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Threading;

namespace PetFamily.API.Controllers
{
    public class FileController : ApplicationController
    {
        private readonly string BUCKET_NAME = "photos";

        [HttpPost]
        public async Task<IActionResult> CreateFile(
            IFormFile file,
            [FromServices] AddPetHendler handler,
            CancellationToken cancellationToken)
        {
            await using var stream = file.OpenReadStream();

            var path = Guid.NewGuid();

            var fileData = new FileData(stream, BUCKET_NAME, path);

            var result = await handler.Handle(fileData, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpDelete("{objectName}")]
        public async Task<IActionResult> DeleteFile(
            [FromRoute] Guid objectName,
            [FromServices] DeletePetHendler hendler,
            CancellationToken cancellationToken)
        {
            var fileMetaData = new FileMetaData(BUCKET_NAME, objectName);
            var result = await hendler.Handle(fileMetaData, cancellationToken);
            if(result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }
        [HttpGet("{objectName}")] 
        public async Task<IActionResult> GetFileUrl(
            [FromRoute] Guid objectName,
            [FromServices] GetPetHendler hendler,
            CancellationToken cancellationToken)
        {
            var fileMetaData = new FileMetaData(BUCKET_NAME, objectName);
            var result = await hendler.Handle(fileMetaData, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }
    }
}
