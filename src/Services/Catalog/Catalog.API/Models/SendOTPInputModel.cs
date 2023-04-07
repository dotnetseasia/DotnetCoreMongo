using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models
{
    public class SendOTPInputModel
    {
        [Required]
        public String Username { get; set; }
        [Required]
        public string EmailId { get; set; }
    }
}
