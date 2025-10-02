using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Models;
using PetFamily.Application.VolunteerOperations.Queries.GetVolunteersWithPagination;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.VolunteerOperations.Queries.GetVolunteerById
{
    public class GetVolunteerByIdHandler : IQueryHandler<Result<VolunteerDto, Error>, GetVolunteerByIdQuery>
    {
        private readonly IReadDbContext _readDbContext;

        public GetVolunteerByIdHandler(IReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task<Result<VolunteerDto, Error>> Handle(
            GetVolunteerByIdQuery query,
            CancellationToken cancellationToken)
        {
            var volunteer = await _readDbContext.Volunteers
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == query.VolunteerId, cancellationToken);

            if (volunteer is null)
                return Errors.General.NotFound(query.VolunteerId);

            return volunteer;
        }
    }
}
