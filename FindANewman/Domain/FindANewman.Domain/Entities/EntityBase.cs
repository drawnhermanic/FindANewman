using System;

namespace FindANewman.Domain.Entities
{
    public class EntityBase
    {
        public virtual int Id { get; protected set; }

        public virtual DateTime? Created { get; set; }

        public virtual DateTime? Updated { get; set; }

        public override bool Equals(object obj)
        {
            var equal = false;
            if (ReferenceEquals(this, obj))
            {
                equal = true;
            }
            else if (obj != null && obj.GetType() == GetType())
            {
                var id = ((EntityBase)obj).Id;

                equal = id == Id;
            }
            return equal;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}