using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Shared
{
    public abstract class Entity<TId> where TId : notnull
    {
        protected Entity(TId id) => Id = id;

        public TId Id { get; private set; }
    }
}
