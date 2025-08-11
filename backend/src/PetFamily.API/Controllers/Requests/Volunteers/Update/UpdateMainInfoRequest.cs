namespace PetFamily.API.Controllers.Requests.Volunteers.Update
{
    public record UpdateMainInfoRequest(
        string Surname,
        string FirstName,
        string Patronymic,
        string Description,
        string PhoneNumber);
}
