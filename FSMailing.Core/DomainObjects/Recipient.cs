using FSMailing.Core.Infrastructure;
namespace FSMailing.Core.DomainObjects
{
    public class Recipient: PersistentEntity
    {
        public virtual bool IsActive { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
    }
}
