using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Volunteers.Update.MainInfo
{
    public class UpdateMainInfoRequestValidator : AbstractValidator<UpdateMainInfoRequest>
    {
        public UpdateMainInfoRequestValidator()
        {
            RuleFor(r => r.VolunteerId)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());
        }
    }

    public class UpdateMainInfoDtoValidator : AbstractValidator<UpdateMainInfoDto>
    {
        public UpdateMainInfoDtoValidator()
        {
            RuleFor(r => r.FullName)
                .MustBeValueObject(f => FullName.Create(f.Surname, f.FirstName, f.Patronymic));

            RuleFor(r => r.Description)
                .MustBeValueObject(Description.Create);

            RuleFor(r => r.PhoneNumber)
                .MustBeValueObject(PhoneNumber.Create);
        }
    }
}
