using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.IdVO
{
    public record SpeciesId
    {
        private SpeciesId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static SpeciesId NewPetId => new(Guid.NewGuid());
        public static SpeciesId Empty => new(Guid.Empty);
        public static SpeciesId Create(Guid id) => new(id);
    }
}
