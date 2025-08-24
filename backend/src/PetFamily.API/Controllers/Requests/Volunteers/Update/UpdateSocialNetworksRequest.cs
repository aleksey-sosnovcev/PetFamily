using PetFamily.Application.VolunteerOperations.Dtos;
using PetFamily.Application.VolunteerOperations.Update.SocialNetworks;

namespace PetFamily.API.Controllers.Requests.Volunteers.Update
{
    public record UpdateSocialNetworksRequest(IEnumerable<SocialNetworksDto> SocialNetworks)
    {
        public UpdateSocialNetworksCommand ToCommand(Guid volunteerId) =>
            new(volunteerId, SocialNetworks);

    }
}
