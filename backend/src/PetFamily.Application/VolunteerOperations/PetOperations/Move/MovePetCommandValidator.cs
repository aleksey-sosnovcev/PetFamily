using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.PetOperations.Move
{
    public class MovePetCommandValidator : AbstractValidator<MovePetCommand>
    {
        public MovePetCommandValidator()
        {
            RuleFor(p => p.VolunteerId)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

            RuleFor(p => p.PetId)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

            RuleFor(p => p.NewPosition).MustBeValueObject(Position.Create);
        }
    }
}
