using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.VolunteerOperations.Update.MainInfo
{
    public class UpdateMainInfoCommandValidator : AbstractValidator<UpdateMainInfoCommand>
    {
        public UpdateMainInfoCommandValidator()
        {
            RuleFor(r => r.VolunteerId)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

            RuleFor(r => new { r.Surname, r.FirstName, r.Patronymic })
                .MustBeValueObject(f => FullName.Create(f.Surname, f.FirstName, f.Patronymic));

            RuleFor(r => r.Description)
                .MustBeValueObject(Description.Create);

            RuleFor(r => r.PhoneNumber)
                .MustBeValueObject(PhoneNumber.Create);
        }
    }
}
