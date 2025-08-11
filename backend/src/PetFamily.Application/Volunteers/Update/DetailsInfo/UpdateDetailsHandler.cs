using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Application.Volunteers.Dtos;
using PetFamily.Application.Volunteers.Update.MainInfo;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Update.DetailsInfo
{
    public class UpdateDetailsHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<UpdateDetailsCommand> _validator;
        private readonly ILogger<UpdateDetailsHandler> _logger;

        public UpdateDetailsHandler(
            IVolunteerRepository volunteerRepository,
            IValidator<UpdateDetailsCommand> validator,
            ILogger<UpdateDetailsHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _validator = validator;
            _logger = logger;
        }
        public async Task<Result<Guid, ErrorList>> Handle(
            UpdateDetailsCommand command,
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

            var detailsResult = Details.Create(command.Name, command.Description).Value;

            volunteerResult.Value.UpdateDetailsInfo(detailsResult);

            var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Updated volunteer's details info with id {volunteerId}", command.VolunteerId);

            return result;
        }
    }
}
