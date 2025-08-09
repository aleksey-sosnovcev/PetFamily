using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Volunteers.Create
{
    public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
    {
        public CreateVolunteerRequestValidator()
        {
            RuleFor(c => c.FullName)
                .MustBeValueObject(f => FullName.Create(f.Surname, f.FirstName, f.Patronymic));

            RuleFor(c => c.Email)
                .MustBeValueObject(Email.Create);

            RuleFor(c => c.Description)
                .MustBeValueObject(Description.Create);

            RuleFor(c => c.PhoneNumber)
                .MustBeValueObject(PhoneNumber.Create);

            RuleFor(c => c.Details)
                .MustBeValueObject(d => Details.Create(d.Name, d.Description));

            RuleForEach(c => c.SocialNetworks).ChildRules(s =>
            {
                s.RuleFor(x => new { x.Link, x.Name })
                    .MustBeValueObject(sn => SocialNetwork.Create(sn.Link, sn.Name));
            });
        }
    }
}
