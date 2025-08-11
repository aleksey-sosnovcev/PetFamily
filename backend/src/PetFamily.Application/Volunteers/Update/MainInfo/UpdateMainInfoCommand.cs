using FluentValidation.AspNetCore;
using PetFamily.Application.Volunteers.Dtos;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Volunteers.Update.MainInfo
{
    public record UpdateMainInfoCommand(
        Guid VolunteerId,
        string Surname,
        string FirstName,
        string Patronymic,
        string Description,
        string PhoneNumber);
}
