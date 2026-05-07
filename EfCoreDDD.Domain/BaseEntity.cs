using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreDDD.Domain
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        protected void SetUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
    public abstract class BaseEntity : BaseEntity<int>
    {

    }

}
