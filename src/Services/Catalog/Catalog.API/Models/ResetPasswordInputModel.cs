using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models
{
    public class ResetPasswordInputModel
    {
        public string OTP { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
