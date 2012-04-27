using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FSMailing.Core.Infrastructure;
using FSMailing.Core.Infrastructure.Repository.NHibernate;
using FSMailing.Web.Infrastructure.Security;
using FSMailing.Web.Models;
using FSMailing.Core;
using FSMailing.Core.DomainObjects;

namespace FSMailing.Web.Controllers
{
    public class MailingController : Controller
    {
        public ActionResult Index()
        {
            return AuthContext.CurrentContext.CurrentUser == null ? View("Login") : View();
        }

        public ActionResult Send(MailingModel model)
        {
            if (AuthContext.CurrentContext.CurrentUser == null)
                return View("Login");

            if (!ModelState.IsValid)
                return View("Index", model);

            var recipients = new List<Recipient>();

            if (model.To == "base" || model.To == "todos")
                using (var session = PersistenceHelper.OpenSession())
                    recipients = new RecipientRepository(session).GetActive().ToList();
            else
                model.To.Replace(",", ";").Replace(" ", "").Split(';').ToList().ForEach(
                        r => recipients.Add(new Recipient {IsActive = true, Email = r, Name = r, CreatedAt = DateTime.Now, Id = 0}));

            var hosts = new Hosts();
            var path = AppDomain.CurrentDomain.BaseDirectory + "MailHosts.xml";
            var sender = new NewsSenderManager(hosts.Load(path));

            sender.ProcessQueue(model.Body, model.Subject, recipients);
            return View("Index", new MailingModel { SendReturn = "enviado com sucesso." });
        }

        public ActionResult Test(string q)
        {
            if (AuthContext.CurrentContext.CurrentUser == null)
                return View("Login");

            var hosts = new Hosts();
            var path = AppDomain.CurrentDomain.BaseDirectory + "MailHosts.xml";
            var sender = new NewsSenderManager(hosts.Load(path));
            sender.TestAllHosts(new Recipient{Name = q, Email = q});

            return View("Index", new MailingModel { SendReturn = "Um e-mail de teste foi enviado usando cada um dos SMTPs configurados." });
        }

        public ActionResult Login()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Login(string login, string password)
        {
            var user = AuthContext.CurrentContext.UserRepository.GetByLogin(login);
            if (user == null || user.Password != password)
                return View(new LoginModel {Message = "Usuário e/ou senha inválidos"});

            AuthContext.CurrentContext.CurrentUser = user;

            return View("Index");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Subscrible(string name, string email)
        {
            var recipient = new Recipient {Email = email, Name = name, IsActive = true};
            using (var session = PersistenceHelper.OpenSession())
            {
                var repository = new RecipientRepository(session);
                var recipientGet = repository.GetByEmail(email);
                if (recipientGet == null)
                    repository.CreateOrUpdate(recipient);
                else
                {
                    recipientGet.IsActive = true;
                    repository.CreateOrUpdate(recipientGet);
                }
            }

            return Json(new { messageStatus = "OK", status = "Subscribled", recipient }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult Unsubscrible(string email)
        {            
            using (var session = PersistenceHelper.OpenSession())
            {
                var repository = new RecipientRepository(session);
                var recipient = repository.GetByEmail(email);
                recipient.IsActive = false;
                repository.CreateOrUpdate(recipient);
            }

            return Json(new { messageStatus = "OK", status = "Unsubscribled" }, JsonRequestBehavior.AllowGet);
        }
    }
}
