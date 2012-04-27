using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FSMailing.Core;
using FSMailing.Core.DomainObjects;
using FSMailing.Core.Extensions;
using FSMailing.Core.Infrastructure;
using FSMailing.Core.Infrastructure.Repository.NHibernate;

namespace FSMailing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Digite:\n T para testar todos os SMTPClients\n" + 
                " S para enviar um e-mail \n "+
                "C para criar o schema no banco de dados. \n L para carregar os e-mails válidos" +
                "\n I para importar os contatos do arquivo csv");
            var k = Console.ReadKey();
            switch (k.KeyChar.ToString(CultureInfo.InvariantCulture).ToUpper())
            {
                case "T": 
                    TestAll();
                    break;
                case "S":
                    SendOne();
                    break;
                case "C":
                    CreateSchema();
                    break;
                case "L":
                    LoadMails();
                    break;
                case "I":
                    ImportRecipients();
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }

        private static void ImportRecipients()
        {
            Console.Clear();
            Console.WriteLine("Informe o caminho do arquivo csv");
            var path = Console.ReadLine();
            var file = new System.IO.StreamReader(path);
            
            while (!file.EndOfStream)
            {
                var data = file.ReadLine().Split(',');
                Console.WriteLine("Importando \"contato: {0} - Email: {1}\"".FormatWith(data[0], data[1]));
                var recipient = new Recipient {Name = data[0], Email = data[1], IsActive = true};
                using (var session = PersistenceHelper.OpenSession())
                {
                    var repository = new RecipientRepository(session);
                    repository.CreateOrUpdate(recipient);                    
                }
            }
            file.Close();
            Console.WriteLine("Importação conluida");
            Console.ReadKey();
        }

        private static void LoadMails()
        {
            using (var session = PersistenceHelper.OpenSession())
            {
                var repository = new RecipientRepository(session);
                repository.GetActive().ToList().ForEach(r => Console.WriteLine(r.Name));
            }
            Console.WriteLine("recuperado ativos");
            Console.Read();
        }

        private static void CreateSchema()
        {
            PersistenceHelper.GenerateSchema();
            Console.WriteLine("Schema criado");
            Console.Read();
        }

        static void TestAll()
        {
            var hosts = new Hosts();
            var path = AppDomain.CurrentDomain.BaseDirectory + "MailHosts.xml";
            var newsSender = new NewsSenderManager(hosts.Load(path));
            newsSender.TestAllHosts(new Recipient {Email = "chalkmaster@gmail.com", Name = "chalk"});
            Console.WriteLine("Sent Test");
            Console.Read();
        }

        static void SendOne()
        {
            var hosts = new Hosts();
            var path = AppDomain.CurrentDomain.BaseDirectory + "MailHosts.xml";
            var newsSender = new NewsSenderManager(hosts.Load(path));
            newsSender.ProcessQueue("Testando envio", "Testando envio", new List<Recipient>(new[] { new Recipient { Email = "chalkmaster@gmail.com", Name = "chalk" } }));
            Console.WriteLine("Sent Test");
            Console.Read();
        }
    }
}
