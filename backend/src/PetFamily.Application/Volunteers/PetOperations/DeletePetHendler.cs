using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.AddPet
{
    public class DeletePetHendler
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<DeletePetHendler> _logger;

        public DeletePetHendler(IFileProvider fileProvider,
            ILogger<DeletePetHendler> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }

        public async Task<Result<string, Error>> Handle(FileMetaData fileMetaData, CancellationToken cancellationToken)
        {
            var result =  await _fileProvider.DeleteFile(fileMetaData, cancellationToken);

            _logger.LogInformation("Deleted file {fileName}", fileMetaData.ObjectName);

            return result;
        }
    }
}
