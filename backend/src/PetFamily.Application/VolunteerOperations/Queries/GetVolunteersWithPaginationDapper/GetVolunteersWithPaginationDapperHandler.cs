using Dapper;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Models;
using PetFamily.Application.VolunteerOperations.Queries.GetVolunteersWithPagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PetFamily.Application.VolunteerOperations.Queries.GetVolunteersWithPaginationDapper
{
    public class GetVolunteersWithPaginationDapperHandler : IQueryHandler<PagedList<VolunteerDto>, GetVolunteersWithPaginationDapperQuery>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetVolunteersWithPaginationDapperHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<PagedList<VolunteerDto>> Handle(
            GetVolunteersWithPaginationDapperQuery query,
            CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.Create();

            var parameters = new DynamicParameters();

            var totalCount = await connection.ExecuteScalarAsync<long>("SELECT COUNT(*) FROM volunteers");

            var sql = new StringBuilder(
                """
                SELECT id, surname, email,firstname,patronymic,phone_number,description, social_networks FROM volunteers
                """);

            if (!string.IsNullOrWhiteSpace(query.Surname))
            {
                sql.Append(" Where surname ILIKE @Surname");
                parameters.Add("@Surname", $"%{query.Surname}%");
            }

            if (!string.IsNullOrWhiteSpace(query.Email))
            {
                sql.Append(" Where email ILIKE @Email");
                parameters.Add("@Email", $"%{query.Email}%");
            }

            sql.ApplySorting(query.SortBy, query.SortDirection);
            sql.ApplyPagination(parameters, query.Page, query.PageSize);

            var volunteer = await connection.QueryAsync<VolunteerDto, string, VolunteerDto>(
                sql.ToString(),
                (volunteer, jsonSocialNetwork) =>
                {
                    volunteer.SocialNetworks = JsonSerializer.Deserialize<SocialNetworksDto[]>(jsonSocialNetwork) ?? [];

                    return volunteer;
                },
                splitOn: "social_networks",
                param: parameters);

            return new PagedList<VolunteerDto>()
            {
                Items = volunteer.ToList(),
                TotalCount = totalCount,
                PageSize = query.PageSize,
                Page = query.Page
            };
        }
    }


    public static class SqlExtentions
    {
        public static void ApplyPagination(
            this StringBuilder sqlBuilder,
            DynamicParameters parameters,
            int Page,
            int PageSize)
        {
            parameters.Add("@PageSize", PageSize);
            parameters.Add("@Offset", (Page - 1) * PageSize);

            sqlBuilder.Append(" LIMIT @PageSize OFFSET @Offset");
        }

        public static void ApplySorting(
            this StringBuilder sqlBuilder,
            string? SortBy,
            string? SortDirection)
        {
            var sortBy = string.IsNullOrWhiteSpace(SortBy) ? "id" : SortBy;
            var sortDirection = SortDirection?.ToLower() == "desc" ? "DESC" : "asc";

            sqlBuilder.Append($" ORDER BY {sortBy} {sortDirection}");
        }
    }
}
