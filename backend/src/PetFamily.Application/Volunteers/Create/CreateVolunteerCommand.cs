using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Application.Volunteers.Dtos;

namespace PetFamily.Application.Volunteers.Create
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
