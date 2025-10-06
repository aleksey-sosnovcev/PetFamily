using PetFamily.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.Queries.GetVolunteerById
{
    public record GetVolunteerByIdQuery(Guid VolunteerId) : IQuery;
}
