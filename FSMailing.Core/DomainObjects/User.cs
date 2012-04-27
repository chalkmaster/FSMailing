using FSMailing.Core.Infrastructure;

namespace FSMailing.Core.DomainObjects
{
    public class User: PersistentEntity
    {
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }
    }
}
