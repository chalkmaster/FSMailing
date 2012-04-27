using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using FSMailing.Core.Infrastructure;
using System.Configuration;
using FSMailing.Core.Infrastructure.SMTP;
using FSMailing.Core.DomainObjects;

namespace FSMailing.Core
{
    public class NewsSenderManager
    {
        private List<string> _queue;
        private readonly Dictionary<Host, SMTPClient> _smtpMailHosts; 
        
        public NewsSenderManager(Hosts hosts)
        {
            _smtpMailHosts = new Dictionary<Host, SMTPClient>();
            hosts.HostList.ForEach(h => _smtpMailHosts.Add(h,
                new SMTPClient(h.HostName, new NetworkCredential(h.User, h.Pass))));
        }

        public void TestAllHosts(Recipient sendTo)
        {            
            _smtpMailHosts.AsParallel().ForAll(h =>
                                                   {
                                                       var testText = "Teste Mail From " + h.Key.Mail;
                                                       h.Value.SendMail(testText,testText, new[] {sendTo});
                                                   });
        }

        public void ProcessQueue(string message, string subject, List<Recipient> recipients)
        {
            new Thread(() =>
                           {
                               while (recipients.Count > 0)
                               {
                                   _smtpMailHosts.AsParallel().ForAll(h =>
                                                                          {
                                                                              List<Recipient> selected;
                                                                              lock (recipients)
                                                                              {
                                                                                  selected =
                                                                                      recipients.Take(100).ToList();
                                                                                  selected.ForEach(
                                                                                      r => recipients.Remove(r));
                                                                              }
                                                                              h.Value.SendMail(message, subject,
                                                                                               selected);
                                                                          });
                                   if (recipients.Count > 0)
                                       Thread.Sleep(new TimeSpan(0, 0, 30, 0));
                               }
                           }).Start();
        }
    }
}
