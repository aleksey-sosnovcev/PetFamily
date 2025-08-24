using PetFamily.Application.VolunteerOperations.PetOperations.Move;

namespace PetFamily.API.Controllers.Requests.Volunteers.Pet.Move
{
    public record MovePetRequest(int NewPosition)
    {
        public MovePetCommand ToCommand(Guid volunteerId, Guid petId) =>
            new(volunteerId, petId, NewPosition);
    }
}
