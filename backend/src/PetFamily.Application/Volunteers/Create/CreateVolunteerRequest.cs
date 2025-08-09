using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Application.Volunteers.Dtos;

namespace PetFamily.Application.Volunteers.Create
{
    public record CreateVolunteerRequest(
       FullNameDto FullName,
       string Email,
       string Description,
       string PhoneNumber,
       DetailsDto Details,
       IEnumerable<SocialNetworksDto> SocialNetworks);
}
