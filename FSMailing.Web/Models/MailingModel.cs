using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSMailing.Web.Models
{
    public class MailingModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string To { get; set; }
        public string SendReturn { get; set; }
    }
}