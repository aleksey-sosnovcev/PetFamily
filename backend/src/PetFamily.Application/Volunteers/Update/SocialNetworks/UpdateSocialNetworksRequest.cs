using PetFamily.Application.Volunteers.Dtos;
using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Update.SocialNetworks
{
    public record UpdateSocialNetworksRequest(Guid VolunteerId, UpdateSocialNetworksDto Dto);

    public record UpdateSocialNetworksDto(IEnumerable<SocialNetworksDto> SocialNetworks);
}
