using CSharpFunctionalExtensions;
using PetFamily.Domain.Species;

namespace PetFamily.Domain.ValueObjects
{
    public record SpeciasAndBreed
    {
        public SpeciesId SpeciesId { get; }
        public BreedId BreedId { get; }

        private SpeciasAndBreed(SpeciesId speciesId, BreedId breedId)
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }

        public static Result<SpeciasAndBreed> Create(SpeciesId speciesId, BreedId breedId)
        {
            if (speciesId == null)
            {
                return Result.Failure<SpeciasAndBreed>("speciesId is invalid");
            }
            if (breedId == null)
            {
                return Result.Failure<SpeciasAndBreed>("breedId is invalid");
            }

            return new SpeciasAndBreed(speciesId, breedId);
        }
    }
}
