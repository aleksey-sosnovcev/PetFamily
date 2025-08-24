using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Application.VolunteerOperations.PetOperations.PetFiles.Add;
using PetFamily.Domain.Pets;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.PetOperations.Move
{
    public class MovePetHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<MovePetCommand> _validator;
        private readonly ILogger<MovePetHandler> _logger;

        public MovePetHandler(
            IVolunteerRepository volunteerRepository,
            IValidator<MovePetCommand> validator,
            ILogger<MovePetHandler> logger) 
        {
            _volunteerRepository = volunteerRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Guid, ErrorList>> Handle(
            MovePetCommand command,
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

            var newPosition = Position.Create(command.NewPosition).Value;

            var result = volunteerResult.Value.MovePet(petExist, newPosition);
            if(result.IsFailure)
                return result.Error.ToErrorList();

            await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("volunteer with {volunteerId} moved pet with {petId}", command.VolunteerId, command.PetId);

            return command.PetId;
        }
    }
}
