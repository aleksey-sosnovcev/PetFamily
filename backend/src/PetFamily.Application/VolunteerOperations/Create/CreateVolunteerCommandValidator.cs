using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Application.VolunteerOperations.Create;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.VolunteerOperations.Create
{
    public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
    {
        public CreateVolunteerCommandValidator()
        {
            RuleFor(c => new { c.Surname, c.FirstName, c.Patronymic })
                .MustBeValueObject(f => FullName.Create(f.Surname, f.FirstName, f.Patronymic));

            RuleFor(c => c.Email)
                .MustBeValueObject(Email.Create);

            RuleFor(c => c.Description)
                .MustBeValueObject(Description.Create);

            RuleFor(c => c.PhoneNumber)
                .MustBeValueObject(PhoneNumber.Create);

            RuleFor(c => new { c.DetailsName, c.DetailsDescription })
                .MustBeValueObject(d => Details.Create(d.DetailsName, d.DetailsDescription));

            RuleForEach(c => c.SocialNetworks).ChildRules(s =>
            {
                s.RuleFor(x => new { x.Link, x.Name })
                    .MustBeValueObject(sn => SocialNetwork.Create(sn.Link, sn.Name));
            });
        }
    }
}
