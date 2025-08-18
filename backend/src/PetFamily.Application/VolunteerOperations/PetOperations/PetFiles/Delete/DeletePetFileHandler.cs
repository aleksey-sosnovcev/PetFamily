using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Application.VolunteerOperations.PetOperations.PetFiles.Add;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.PetOperations.PetFiles.Delete
{
    public class DeletePetFileHandler
    {
        private const string BUCKET_NAME = "photos";

        private readonly IVolunteerRepository _repository;
        private readonly IFileProvider _fileProvider;
        private readonly IValidator<DeletePetFileCommand> _validator;
        private readonly ILogger<DeletePetFileHandler> _logger;

        public DeletePetFileHandler(
            IVolunteerRepository repository,
            IFileProvider fileProvider,
            IValidator<DeletePetFileCommand> validator,
            ILogger<DeletePetFileHandler> logger)
        {
            _repository = repository;
            _fileProvider = fileProvider;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<string, ErrorList>> Handle(
            DeletePetFileCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ErrorList();
            }

            var volunteerResult = await _repository.GetById(
                VolunteerId.Create(command.VolunteerId), cancellationToken);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

            var petExist = volunteerResult.Value.Pets.FirstOrDefault(
                p => p.Id == PetId.Create(command.PetId));

            if (petExist is null)
                return Errors.General.NotFound().ToErrorList();

            var filePath = FilePath.Create(command.Files);
            if (filePath.IsFailure)
                return Errors.General.NotFound().ToErrorList();

            var result = await _fileProvider.DeleteFile(
                new(filePath.Value, BUCKET_NAME), cancellationToken);

            if (result.IsFailure)
                return result.Error;

            petExist.RemovePhoto(filePath.Value);

            await _repository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Deleted file {fileName}", filePath.Value);

            return result;
        }
    }
}
