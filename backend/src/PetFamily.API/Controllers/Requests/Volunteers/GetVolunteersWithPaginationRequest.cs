using PetFamily.Application.VolunteerOperations.Queries.GetVolunteersWithPagination;
using PetFamily.Application.VolunteerOperations.Queries.GetVolunteersWithPaginationDapper;

namespace PetFamily.API.Controllers.Requests.Volunteers
{
    public record GetVolunteersWithPaginationRequest(
        string? Surname,
        string? Email,
        string? SortBy,
        string? SortDirection,
        int Page,
        int PageSize)
    {
        public GetVolunteersWithPaginationQuery ToQuery() =>
            new(Surname, Email, SortBy, SortDirection, Page, PageSize);
    }

    public record GetVolunteersWithPaginationDapperRequest(
        string? Surname,
        string? Email,
        string? SortBy,
        string? SortDirection,
        int Page,
        int PageSize)
    {
        public GetVolunteersWithPaginationDapperQuery ToQuery() =>
            new(Surname, Email, SortBy, SortDirection, Page, PageSize);
    }
}
