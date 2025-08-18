using PetFamily.Application.VolunteerOperations.Dtos;

namespace PetFamily.API.Controllers.Requests.Volunteers.Update
{
    public record UpdateSocialNetworksRequest(IEnumerable<SocialNetworksDto> SocialNetworks);
}
