using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.IdVO
{
    public record PetId
    {
        private PetId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static PetId NewPetId => new(Guid.NewGuid());
        public static PetId Empty => new(Guid.Empty);
        public static PetId Create(Guid id) => new(id);
    }
}
