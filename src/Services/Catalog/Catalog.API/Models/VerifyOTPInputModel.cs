using CompanyAdmin.API.Entities.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models
{
    public class VerifyOTPInputModel
    {
        public string OTP { get; set; }
        public string EmailID { get; set; }
    }
}
