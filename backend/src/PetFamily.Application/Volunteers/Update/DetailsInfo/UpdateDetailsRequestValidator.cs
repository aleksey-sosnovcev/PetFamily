using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Update.DetailsInfo
{
    public class UpdateDetailsRequestValidator : AbstractValidator<UpdateDetailsRequest>
    {
        public UpdateDetailsRequestValidator()
        {
            RuleFor(r => r.VolunteerId)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

            RuleFor(r => r.Details)
                .MustBeValueObject(d => Details.Create(d.Name, d.Description));
        }
    }
}
