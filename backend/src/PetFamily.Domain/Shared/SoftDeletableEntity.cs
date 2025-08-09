using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Shared
{
    public abstract class SoftDeletableEntity<TId> : Entity<TId> where TId : notnull
    {
        protected SoftDeletableEntity(TId id) : base(id) { }

        public bool IsDeleted { get; private set; }
        public DateTime? DeletionDate { get; private set; }

        public virtual void Delete()
        {
            if (IsDeleted == true) return;

            IsDeleted = true;
            //DeletionDate = DateTime.UtcNow;
        }

        public virtual void Restore()
        {
            if (IsDeleted == false) return;

            IsDeleted = false;
            DeletionDate = null;
        }
    }
}
