using FluentValidation.AspNetCore;
using PetFamily.Application.VolunteerOperations.Dtos;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.VolunteerOperations.Update.MainInfo
{
    public record UpdateMainInfoCommand(
        Guid VolunteerId,
        string Surname,
        string FirstName,
        string Patronymic,
        string Description,
        string PhoneNumber);
}
