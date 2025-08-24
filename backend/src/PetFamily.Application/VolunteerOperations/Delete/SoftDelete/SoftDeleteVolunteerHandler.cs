using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Application.VolunteerOperations.Delete.HardDelete;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.Delete.SoftDelete
{
    public class SoftDeleteVolunteerHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<DeleteVolunteerCommand> _validator;
        private readonly ILogger<SoftDeleteVolunteerHandler> _logger;

        public SoftDeleteVolunteerHandler(
            IVolunteerRepository volunteerRepository,
            IValidator<DeleteVolunteerCommand> validator,
            ILogger<SoftDeleteVolunteerHandler> logger)
        {
            _volunteerRepository = volunteerRepository;
            _validator = validator;
            _logger = logger;
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

            volunteerResult.Value.Delete();

            var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Volunteer deleted with id {volunteerId}", command.VolunteerId);

            return result;
        }
    }
}
