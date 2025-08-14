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
    public class GetPetHendler
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger<GetPetHendler> _logger;

        public GetPetHendler(IFileProvider fileProvider,
            ILogger<GetPetHendler> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }

        public async Task<Result<string, Error>> Handle(FileMetaData fileMetaData, CancellationToken cancellationToken)
        {
            var result = await _fileProvider.GetFileUrl(fileMetaData, cancellationToken);

            _logger.LogInformation("Getting file Url {fileName}", fileMetaData.ObjectName);

            return result;
        }
    }
}
