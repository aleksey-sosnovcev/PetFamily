using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Species
{
    internal class Species
    {
        private readonly List<Breed> _breeds = [];
<<<<<<< Updated upstream
        public Guid Id { get; }
        public string Name { get; }
        public IReadOnlyList<Breed> Breeds => _breeds;

=======
        public string Name { get; private set; } = default!;
        public IReadOnlyList<Breed> Breeds => _breeds;

        public Species(SpeciesId speciesId, string name) : base(speciesId)
        {
            Name = name;
        }
>>>>>>> Stashed changes
        public void AddBreed(Breed breed)
        {
            _breeds.Add(breed);
        }
<<<<<<< Updated upstream
=======

        public static Result<Species, Error> Create(SpeciesId speciesId, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Errors.General.ValueIsInvalid("Name");

            var species = new Species(speciesId, name);

            return species;
        }

>>>>>>> Stashed changes
    }
}
