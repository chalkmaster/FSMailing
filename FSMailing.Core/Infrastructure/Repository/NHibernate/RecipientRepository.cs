using System.Collections.Generic;
using NHibernate;
using FSMailing.Core.DomainObjects;

namespace FSMailing.Core.Infrastructure.Repository.NHibernate
{
    public class RecipientRepository: Repository<Recipient>
    {
        public RecipientRepository(ISession session) : base(session)
        {
        }

        public IList<Recipient> GetActive()
        {
            return Session.QueryOver<Recipient>()
                .Where(n => n.IsActive).List();
        }
        public IList<Recipient> GetInactive()
        {
            return Session.QueryOver<Recipient>()
                .Where(n => !n.IsActive).List();
        }

        public Recipient GetByEmail(string email)
        {
            return Session.QueryOver<Recipient>()
                .Where(n => n.Email == email).SingleOrDefault();
        }
    }
}
