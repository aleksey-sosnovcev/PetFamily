using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.SpeciesOperations;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Repositories
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public SpeciesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<bool, Error>> GetBySpeciesAndBreed(
            SpeciesId speciesId,
            BreedId breedId,
            CancellationToken cancellationToken)
        {
            var speciesAndBreed = await _dbContext.Species
                .AnyAsync(s => s.Id == speciesId && s.Breeds
                .Any(b => b.Id == breedId), cancellationToken);

            if (speciesAndBreed == false)
                return Errors.General.NotFound();

            return speciesAndBreed;
        }
    }
}
