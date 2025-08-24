using PetFamily.Application.VolunteerOperations.PetOperations.PetFiles.Delete;

namespace PetFamily.API.Controllers.Requests.Volunteers.Pet.DeleteFile
{
    public record DeletePetFileRequest(string FileName)
    {
        public DeletePetFileCommand ToCommand(Guid volunteerId, Guid petId) =>
            new(volunteerId, petId, FileName);
    }
}
