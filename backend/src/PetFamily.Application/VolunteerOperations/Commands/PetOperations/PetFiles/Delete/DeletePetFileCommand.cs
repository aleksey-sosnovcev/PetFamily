using PetFamily.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.Commands.PetOperations.PetFiles.Delete
{
    public record DeletePetFileCommand(
    Guid VolunteerId,
    Guid PetId,
    string Files) : ICommand;
}
