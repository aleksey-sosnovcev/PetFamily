using PetFamily.Application.VolunteerOperations.Update.DetailsInfo;

namespace PetFamily.API.Controllers.Requests.Volunteers.Update
{
    public record UpdateDetailsRequest(
        string Name,
        string Description)
    {
        public UpdateDetailsCommand ToCommand(Guid volunteerId) =>
            new(volunteerId, Name, Description);
    }
}
