using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Species
{
    public record BreedId
    {
        private BreedId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }
        public static BreedId NewBreedId() => new(Guid.NewGuid());
        public static BreedId Empty() => new(Guid.Empty);
        public static BreedId Create(Guid id) => new(id);
    }
}
