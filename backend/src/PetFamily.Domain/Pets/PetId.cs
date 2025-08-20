using PetFamily.Domain.Volunteers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Pets
{
    public record PetId
    {
        private PetId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public static PetId NewPetId() => new(Guid.NewGuid());
        public static PetId Empty() => new(Guid.Empty);
        public static PetId Create(Guid id) => new(id);

        public static implicit operator PetId(Guid id) => new(id);

        public static implicit operator Guid(PetId petId)
        {
            //if (volunteerId is null)
            //  throw new ArgumentNullException();
            ArgumentNullException.ThrowIfNull(petId);

            return petId.Value;
        }
    }
}
