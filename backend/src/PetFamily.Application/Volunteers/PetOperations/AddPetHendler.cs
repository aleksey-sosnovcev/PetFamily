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
    public class AddPetHendler
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<AddPetHendler> _logger;

        public AddPetHendler(IFileProvider fileProvider,
            ILogger<AddPetHendler> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }

        public async Task<Result<string, Error>> Handle(FileData fileDara, CancellationToken cancellationToken)
        {
            var result =  await _fileProvider.UploadFile(fileDara, cancellationToken);

            _logger.LogInformation("Added file {fileName}", fileDara.ObjectName);

            return result;
        }
    }
}
