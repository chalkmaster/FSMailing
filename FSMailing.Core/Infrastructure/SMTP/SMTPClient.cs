using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using FSMailing.Core.DomainObjects;
using FSMailing.Core.Extensions;

namespace FSMailing.Core.Infrastructure.SMTP
{
    public class SMTPClient
    {
        private readonly string _hostName;
        private readonly NetworkCredential _credencial;

        public SMTPClient(String hostNome, NetworkCredential credential)
        {
            _hostName = hostNome;
            _credencial = credential;
        }

        public void SendMail(string mailMessage, string mailSubject, IEnumerable<Recipient> mailRecipients)
        {
            var mail = new MailMessage();
            mailRecipients.AsParallel().ForAll(m => mail.Bcc.Add(m.Email));

            mail.ReplyToList.Add("raptors@raptors.com.br");
            mail.From = new MailAddress("raptors@raptors.com.br", "DotNetRaptors");
            mail.Subject = mailSubject;
            mail.Body = mailMessage;
            mail.IsBodyHtml = true;

            var smtp = new SmtpClient(_hostName)
                           {Credentials = _credencial};

            smtp.SendAsync(mail, new object());
        }
    }
}
