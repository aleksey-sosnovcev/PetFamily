using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteerOperations.Commands.Delete
{
    public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;
}
