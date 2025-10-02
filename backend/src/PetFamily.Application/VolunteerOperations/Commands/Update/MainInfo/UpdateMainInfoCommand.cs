using FluentValidation.AspNetCore;
using PetFamily.Application.Abstractions;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.VolunteerOperations.Commands.Update.MainInfo
{
    public record UpdateMainInfoCommand(
        Guid VolunteerId,
        string Surname,
        string FirstName,
        string Patronymic,
        string Description,
        string PhoneNumber) : ICommand;
}
