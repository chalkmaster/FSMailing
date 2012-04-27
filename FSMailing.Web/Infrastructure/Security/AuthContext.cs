using System;
using System.Web;
using FSMailing.Core.DomainObjects;
using FSMailing.Core.Infrastructure.Repository.NHibernate;
using NHibernate;

namespace FSMailing.Web.Infrastructure.Security
{
    public class AuthContext
    {
        private static readonly object Lock = new object();
        private static volatile AuthContext _instance;
        private const string UserIdSessionKey = "CurrentUserId";
        private static volatile ISession Session;

        public static AuthContext CurrentContext
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new AuthContext();
                        }
                    }
                }
                return _instance;
            }
        }

        private static ISession CurrentSession
        {
            get
            {
                if (Session == null)
                {
                    lock (Lock)
                    {
                        if (Session == null)
                        {
                            Session = PersistenceHelper.OpenSession();
                        }
                    }
                }
                return Session;
            }
        }

        private HttpContext HttpContext
        {
            get
            {
                return HttpContext.Current;
            }
        }

        public UserRepository UserRepository
        {
            get
            {
                return new UserRepository(CurrentSession);
            }
        }

        public User CurrentUser
        {
            get
            {
                return HttpContext.Session[UserIdSessionKey] == null
                           ? null
                           : UserRepository.GetById(Int32.Parse(HttpContext.Session[UserIdSessionKey].ToString()));
            }
            set
            {
                HttpContext.Session[UserIdSessionKey] = value.Id;
            }
        }
    }
}
