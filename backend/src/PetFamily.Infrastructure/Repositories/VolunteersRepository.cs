using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Volunteers;
using CSharpFunctionalExtensions;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Infrastructure.Repositories
{
    public class VolunteersRepository : IVolunteerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public VolunteersRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default)
        {
            await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return volunteer.Id; 
        }

        public async Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId)
        {
            var volunteer = await _dbContext.Volunteers
                .Include(v => v.Pets)
                .FirstOrDefaultAsync(v =>  v.Id == volunteerId);

            if(volunteer is null)
            {
                return Errors.General.NotFound(volunteerId);
            }

            return volunteer;
        }
        public async Task<Result<Volunteer, Error>> GetByEmail(Email email)
        {
            var volunteer = await _dbContext.Volunteers
                .Include (v => v.Pets)
                .FirstOrDefaultAsync(v => v.Email == email);

            if(volunteer is null)
                return Errors.General.NotFound();

            return volunteer;
        }
    }
}
