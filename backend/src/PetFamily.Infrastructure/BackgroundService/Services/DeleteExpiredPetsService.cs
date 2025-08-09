using Microsoft.EntityFrameworkCore;
using PetFamily.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.BackgroundServices.Services
{
    public class DeleteExpiredPetsService
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteExpiredPetsService(
            ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Process(CancellationToken cancellationToken)
        {
            var volunteers = await GetVolunteersWithPetsAsync(cancellationToken);

            foreach (var volunteer in volunteers)
            {
                volunteer.DeleteExpiredPets();
            }
        }

        private async Task<IEnumerable<Volunteer>> GetVolunteersWithPetsAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Volunteers.Include(v => v.Pets).ToListAsync(cancellationToken);
        }
    }
}
