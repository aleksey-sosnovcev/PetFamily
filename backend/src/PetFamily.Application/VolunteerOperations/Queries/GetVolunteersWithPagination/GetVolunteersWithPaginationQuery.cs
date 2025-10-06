using PetFamily.Application.Abstractions;

namespace PetFamily.Application.VolunteerOperations.Queries.GetVolunteersWithPagination
{
    public record GetVolunteersWithPaginationQuery(
        string? Surname,
        string? Email,
        string? SortBy,
        string? SortDirection,
        int Page,
        int PageSize) : IQuery;
}
