using PetFamily.Application.VolunteerOperations.Commands.Update.MainInfo;

namespace PetFamily.API.Controllers.Requests.Volunteers.Update
{
    public record UpdateMainInfoRequest(
        string Surname,
        string FirstName,
        string Patronymic,
        string Description,
        string PhoneNumber)
    {
        public UpdateMainInfoCommand ToCommand(Guid volunteerId) =>
            new(volunteerId,
                Surname,
                FirstName,
                Patronymic,
                Description,
                PhoneNumber);
    }
}
