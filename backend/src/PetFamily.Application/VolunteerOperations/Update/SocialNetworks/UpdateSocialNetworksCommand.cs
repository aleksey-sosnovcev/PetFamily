using PetFamily.Application.VolunteerOperations.Dtos;
using PetFamily.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.Update.SocialNetworks
{
    public record UpdateSocialNetworksCommand(Guid VolunteerId, IEnumerable<SocialNetworksDto> SocialNetworks);
}
