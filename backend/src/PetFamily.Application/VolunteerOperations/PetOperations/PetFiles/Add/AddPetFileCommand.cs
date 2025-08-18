using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Application.VolunteerOperations.Dtos;

namespace PetFamily.Application.VolunteerOperations.PetOperations.PetFiles.Add
{
    public record AddPetFileCommand(
        Guid VolunteerId, 
        Guid PetId, 
        IEnumerable<CreateFileDto> Files);
}
