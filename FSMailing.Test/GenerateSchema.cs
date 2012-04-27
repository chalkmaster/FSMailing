using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace FSMailing.Test
{
    [TestClass]
    public class GenerateSchema
    {
        [TestMethod]
        public void CanGenerateSchema()
        {
            var configuration = new Configuration();
            configuration.Configure();

            new SchemaExport(configuration)
                .SetOutputFile("d:\\FSMailing_DDL.sql").Execute(true, true, false);
        }
    }
}
