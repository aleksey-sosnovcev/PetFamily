using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.VolunteerOperations.Commands.PetOperations.PetFiles.Add
{
    public record AddPetFileCommand(
        Guid VolunteerId,
        Guid PetId,
        IEnumerable<CreateFileDto> Files) : ICommand;
}
