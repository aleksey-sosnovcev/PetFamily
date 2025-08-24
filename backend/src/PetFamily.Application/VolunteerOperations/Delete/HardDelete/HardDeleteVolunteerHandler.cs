using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Application.VolunteerOperations.Update.DetailsInfo;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.Delete.HardDelete
{
    public class HardDeleteVolunteerHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<DeleteVolunteerCommand> _validator;
        private readonly ILogger<HardDeleteVolunteerHandler> _logger;

        public HardDeleteVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            IValidator<DeleteVolunteerCommand> validator,
            ILogger<HardDeleteVolunteerHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            DeleteVolunteerCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid == false)
            {
                return validationResult.ToErrorList();
            }

            var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error.ToErrorList();
            }

            var result = await _volunteerRepository.Delete(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Volunteer deleted with id {volunteerId}", command.VolunteerId);

            return result;
        }
    }
}
