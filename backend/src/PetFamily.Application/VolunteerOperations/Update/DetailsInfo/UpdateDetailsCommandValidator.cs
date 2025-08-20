using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.Update.DetailsInfo
{
    public class UpdateDetailsCommandValidator : AbstractValidator<UpdateDetailsCommand>
    {
        public UpdateDetailsCommandValidator()
        {
            RuleFor(r => r.VolunteerId)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

            RuleFor(r => new { r.Name, r.Description })
                .MustBeValueObject(d => Details.Create(d.Name, d.Description));
        }
    }
}
