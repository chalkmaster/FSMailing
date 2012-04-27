using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSMailing.Core.DomainObjects;
using NHibernate;

namespace FSMailing.Core.Infrastructure.Repository.NHibernate
{
    public class UserRepository: Repository<User>
    {
        public UserRepository(ISession session) : base(session)
        {
        }
        public User GetByLogin(string login)
        {
            return Session.QueryOver<User>()
                .Where(u => u.Login == login)
                .SingleOrDefault();
        }
    }
}
