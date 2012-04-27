using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FSMailing.Web.Models
{
    public class LoginModel
    {
        public string Message { get; set; }

        [Display(Description = "Login")]
        public string Login { get; set; }

        [Display(Description = "Senha")]
        public string Password { get; set; }
    }
}