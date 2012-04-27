using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using FSMailing.Core.DomainObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using FSMailing.Core.Infrastructure.Repository;
using FSMailing.Core.Infrastructure.Repository.NHibernate;

namespace FSMailing.Test
{
    [TestClass]
    public class RecipienteRepositoryTest
    {
        [TestMethod]
        public void CanCreateNew()
        {
            using (var session = PersistenceHelper.OpenSession())
            {
                var repository = new RecipientRepository(session);
                var r = new Recipient {Email = "a@a.ccom", IsActive = true, Name = "Chalk"};
                repository.CreateOrUpdate(r);

                Assert.IsTrue(repository.GetActive().Count > 0);
                repository.Remove(r);
            }
        }
    }
}
