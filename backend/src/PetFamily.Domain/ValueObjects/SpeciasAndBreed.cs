using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
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

        public static Result<SpeciasAndBreed, Error> Create(SpeciesId speciesId, BreedId breedId)
        {
            if (speciesId == null)
            {
                return Errors.General.ValueIsInvalid("SpeciesId");
            }
            if (breedId == null)
            {
                return Errors.General.ValueIsInvalid("BreedId");
            }

            var speciasAndBreed = new SpeciasAndBreed(speciesId, breedId);

            return speciasAndBreed;
        }
    }
}
