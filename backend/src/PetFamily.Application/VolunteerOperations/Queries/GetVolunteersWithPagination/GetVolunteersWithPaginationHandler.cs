
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Extensions;
using PetFamily.Application.Models;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static PetFamily.Domain.Shared.Errors;

namespace PetFamily.Application.VolunteerOperations.Queries.GetVolunteersWithPagination
{
    public class GetVolunteersWithPaginationHandler : IQueryHandler<PagedList<VolunteerDto>, GetVolunteersWithPaginationQuery>
    {
        private readonly IReadDbContext _readDbContext;

        public GetVolunteersWithPaginationHandler(
           IReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<PagedList<VolunteerDto>> Handle(
            GetVolunteersWithPaginationQuery query,
            CancellationToken cancellationToken)
        {
            var volunteerQuery = _readDbContext.Volunteers.AsNoTracking().AsQueryable();

            // будущая фильтрация и сортировка
            volunteerQuery = volunteerQuery.WhereIf(
                !string.IsNullOrWhiteSpace(query.Surname),
                v => v.Surname.ToLower().Contains(query.Surname!.ToLower()));

            if(!string.IsNullOrWhiteSpace(query.Email))
            {
                volunteerQuery = volunteerQuery
                    .Where(v => v.Email.ToLower().Contains(query.Email.ToLower()));
            }

            Expression<Func<VolunteerDto, object>> keySelector = query.SortBy?.ToLower() switch
            {
                "surname" => (volunteer) => volunteer.Surname,
                "email" => (volunteer) => volunteer.Email,
                _ => (volunteer) => volunteer.Id
            };

            volunteerQuery = query.SortDirection?.ToLower() == "desc"
                ? volunteerQuery.OrderByDescending(keySelector)
                : volunteerQuery.OrderBy(keySelector);

            var pagedList = await volunteerQuery
                .ToPagedList(query.Page, query.PageSize, cancellationToken);

            return pagedList;
        }
    }
}
