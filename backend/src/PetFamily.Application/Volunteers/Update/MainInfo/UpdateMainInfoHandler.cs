using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Update.MainInfo
{
    public class UpdateMainInfoHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<UpdateMainInfoCommand> _validator;
        private readonly ILogger<UpdateMainInfoHandler> _logger;

        public UpdateMainInfoHandler(
            IVolunteerRepository volunteerRepository,
            IValidator<UpdateMainInfoCommand> validator,
            ILogger<UpdateMainInfoHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _validator = validator;
            _logger = logger;
        }
        public async Task<Result<Guid, ErrorList>> Handle(
            UpdateMainInfoCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ErrorList();
            }

            var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error.ToErrorList();
            }

            var fullNameResult = FullName.Create(
                command.Surname,
                command.FirstName,
                command.Patronymic).Value;

            var descriptionResut = Description.Create(command.Description).Value;

            var phoneNumberResult = PhoneNumber.Create(command.PhoneNumber).Value;

            volunteerResult.Value.UpdateMainInfo(
                fullNameResult,
                descriptionResut,
                phoneNumberResult);

            var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Updated volunteer's main info with id {volunteerId}", command.VolunteerId);

            return result;
        }
    }
}
