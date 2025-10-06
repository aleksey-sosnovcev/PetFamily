using PetFamily.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.Commands.Update.DetailsInfo
{
    public record UpdateDetailsCommand(
        Guid VolunteerId,
        string Name,
        string Description) : ICommand;
}
