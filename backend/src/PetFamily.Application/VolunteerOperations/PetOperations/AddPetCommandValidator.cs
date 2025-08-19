using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Application.VolunteerOperations.Update.DetailsInfo;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using PetFamily.Domain.Species;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.PetOperations
{
    public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
    {
        public AddPetCommandValidator() 
        {
            RuleFor(p => p.VolunteerId)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

            RuleFor(p => p.Name).MustBeValueObject(Name.Create);

            RuleFor(p => new { p.SpeciesId, p.BreedId }).MustBeValueObject(s => SpeciasAndBreed.Create(s.SpeciesId, s.BreedId));

            RuleFor(p => p.Description).MustBeValueObject(Description.Create);
            
            RuleFor(p => p.Color)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

            RuleFor(p => p.InfoHealth).MustBeValueObject(InfoHealth.Create);

            RuleFor(p => new { p.City, p.Street, p.HouseNumber, p.Apartment, p.PostalCode })
                .MustBeValueObject(i => Address.Create(i.City, i.Street, i.HouseNumber, i.Apartment, i.PostalCode));

            RuleFor(p => p.Weight)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

            RuleFor(p => p.Grouwth)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

            RuleFor(p => p.PhoneNumber).MustBeValueObject(PhoneNumber.Create);

            RuleFor(p => p.Status)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

            RuleFor(p => new {p.DetailsName, p.DetailsDescription})
                .MustBeValueObject(d => Details.Create(d.DetailsName, d.DetailsDescription));
        }
    }
}
