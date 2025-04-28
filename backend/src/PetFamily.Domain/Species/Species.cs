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
        public Guid Id { get; }
        public string Name { get; }
        public IReadOnlyList<Breed> Breeds => _breeds;

        public void AddBreed(Breed breed)
        {
            _breeds.Add(breed);
        }
    }
}
