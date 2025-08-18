using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.SpeciesOperations
{
    public interface ISpeciesRepository
    {
        Task<Result<bool, Error>> GetBySpeciesAndBreed(
            SpeciesId speciesId,
            BreedId breedId,
            CancellationToken cancellationToken);
    }
}
