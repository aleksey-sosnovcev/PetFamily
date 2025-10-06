using PetFamily.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.Queries.GetVolunteersWithPaginationDapper
{
    public record GetVolunteersWithPaginationDapperQuery(
        string? Surname,
        string? Email,
        string? SortBy,
        string? SortDirection,
        int Page,
        int PageSize) : IQuery;
        
}
