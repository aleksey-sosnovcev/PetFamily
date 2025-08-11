using PetFamily.Application.Volunteers.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Update.DetailsInfo
{
    public record UpdateDetailsCommand(
        Guid VolunteerId,
        string Name,
        string Description);
}
