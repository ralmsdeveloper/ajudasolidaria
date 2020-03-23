using System;

namespace AjudaSolidaria.Domain.Abstract
{
    public abstract class EntityBase
    {
        private Guid _key;
        public Guid Key => _key;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        protected EntityBase() : this(Guid.NewGuid()) { }
        protected EntityBase(Guid key) => _key = key;
    }
}
