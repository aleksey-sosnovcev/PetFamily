using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Application.VolunteerOperations.Dtos;

namespace PetFamily.Application.VolunteerOperations.Create
{
    public record CreateVolunteerCommand(
       string Surname,
       string FirstName,
       string Patronymic,
       string Email,
       string Description,
       string PhoneNumber,
       string DetailsName,
       string DetailsDescription,
       IEnumerable<SocialNetworksDto> SocialNetworks);
}
