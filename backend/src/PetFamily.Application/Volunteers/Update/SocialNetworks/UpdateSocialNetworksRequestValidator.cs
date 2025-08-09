using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Application.Volunteers.Update.DetailsInfo;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Update.SocialNetworks
{
    public class UpdateSocialNetworksRequestValidator : AbstractValidator<UpdateSocialNetworksRequest>
    {
        public UpdateSocialNetworksRequestValidator()
        {
            RuleFor(r => r.VolunteerId)
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

            RuleForEach(c => c.Dto.SocialNetworks).ChildRules(s =>
            {
                s.RuleFor(x => new { x.Link, x.Name })
                    .MustBeValueObject(sn => SocialNetwork.Create(sn.Link, sn.Name));
            });
        }
    }
}
