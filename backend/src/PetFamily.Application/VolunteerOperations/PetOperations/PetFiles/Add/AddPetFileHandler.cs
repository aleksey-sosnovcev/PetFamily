using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.PetOperations.PetFiles.Add
{
    public class AddPetFileHandler
    {
        private const string BUCKET_NAME = "photos";

        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IFileProvider _fileProvider;
        private readonly IValidator<AddPetFileCommand> _validator;
        private readonly ILogger<AddPetFileHandler> _logger;

        public AddPetFileHandler(
            IVolunteerRepository volunteerRepository,
            IFileProvider fileProvider,
            IValidator<AddPetFileCommand> validator,
            ILogger<AddPetFileHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _fileProvider = fileProvider;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<IReadOnlyList<FilePath>, ErrorList>> Handle(
            AddPetFileCommand command,
            CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ErrorList();
            }

            var volunteerResult = await _volunteerRepository.GetById(
                VolunteerId.Create(command.VolunteerId), cancellationToken);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

            var petExist = volunteerResult.Value.Pets.FirstOrDefault(
                p => p.Id == PetId.Create(command.PetId));

            if (petExist is null)
                return Errors.General.NotFound().ToErrorList();

            List<StreamFileData> filesData = [];
            foreach (var file in command.Files)
            {
                
                var filePath = FilePath.Create(Guid.NewGuid().ToString());
                if(filePath.IsFailure)
                    return filePath.Error.ToErrorList();

                var fileContent = new StreamFileData(file.Stream, new FileData(filePath.Value, BUCKET_NAME));
                filesData.Add(fileContent);
            }

            var result = await _fileProvider.UploadFile(filesData, cancellationToken);
            if (result.IsFailure)
                return result.Error;

            foreach(var file in filesData)
            {
                petExist.AddPhoto(file.FileData.FilePath);
                _logger.LogInformation("Added file {fileName}", file.FileData.FilePath.PathToStorage);
            }

            var saveResult = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);
            if (saveResult.IsFailure)
            {
                var removeResult = await _fileProvider.DeleteFiles(filesData, cancellationToken);
                if (removeResult.IsFailure)
                    return removeResult.Error;

                return Error.Failure("fail.save.data", "Fail to save data in database").ToErrorList();
            }

            return result;
        }
    }
}
