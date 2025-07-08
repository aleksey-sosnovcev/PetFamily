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

<<<<<<< Updated upstream:backend/src/PetFamily.Domain/IdVO/SpeciesId.cs
        public static SpeciesId NewPetId => new(Guid.NewGuid());
        public static SpeciesId Empty => new(Guid.Empty);
=======
        public static SpeciesId NewSpeciesId() => new(Guid.NewGuid());
        public static SpeciesId Empty() => new(Guid.Empty);
>>>>>>> Stashed changes:backend/src/PetFamily.Domain/Species/SpeciesId.cs
        public static SpeciesId Create(Guid id) => new(id);
    }
}
