using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace FSMailing.Core.Infrastructure.Repository.NHibernate
{
    public class PersistenceHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static void GenerateSchema()
        {
            var configuration = new Configuration();
            configuration.Configure();

            new SchemaExport(configuration)
                .SetOutputFile("d:\\FSMailing_DDL.sql").Execute(true, true, false);
        }

    }
}
