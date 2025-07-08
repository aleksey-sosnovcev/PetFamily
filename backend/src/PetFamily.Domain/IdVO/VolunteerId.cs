using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.IdVO
{
    public record VolunteerId
    {
        private VolunteerId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

<<<<<<< Updated upstream:backend/src/PetFamily.Domain/IdVO/VolunteerId.cs
        public static VolunteerId NewPetId => new(Guid.NewGuid());
        public static VolunteerId Empty => new(Guid.Empty);
=======
        public static VolunteerId NewVolunteerId() => new(Guid.NewGuid());
        public static VolunteerId Empty() => new(Guid.Empty);
>>>>>>> Stashed changes:backend/src/PetFamily.Domain/Volunteer/VolunteerId.cs
        public static VolunteerId Create(Guid id) => new(id);

        public static implicit operator Guid(VolunteerId volunteerId)
        {
            //if (volunteerId is null)
            //  throw new ArgumentNullException();
            ArgumentNullException.ThrowIfNull(volunteerId);

            return volunteerId.Value;
        }
    }
}
