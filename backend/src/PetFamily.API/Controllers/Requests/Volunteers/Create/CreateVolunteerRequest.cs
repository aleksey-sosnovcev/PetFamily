using PetFamily.Application.VolunteerOperations.Dtos;

namespace PetFamily.API.Controllers.Requests.Volunteers.Create
{
    public record CreateVolunteerRequest(
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
