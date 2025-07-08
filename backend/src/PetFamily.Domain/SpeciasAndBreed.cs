using CSharpFunctionalExtensions;
<<<<<<< Updated upstream:backend/src/PetFamily.Domain/SpeciasAndBreed.cs
using PetFamily.Domain.IdVO;

namespace PetFamily.Domain
=======
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species;


namespace PetFamily.Domain.ValueObjects
>>>>>>> Stashed changes:backend/src/PetFamily.Domain/ValueObjects/SpeciasAndBreed.cs
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
            if(breedId == null)
            {
                return Errors.General.ValueIsInvalid("BreedId");
            }

            var speciasAndBreed = new SpeciasAndBreed(speciesId, breedId);

            return speciasAndBreed;
        }
    }
}
