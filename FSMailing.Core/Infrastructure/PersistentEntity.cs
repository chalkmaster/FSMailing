using System;

namespace FSMailing.Core.Infrastructure
{
    public class PersistentEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedAt { get; set; }
    }
}
