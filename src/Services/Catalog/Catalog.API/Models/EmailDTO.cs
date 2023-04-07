using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models
{
    public class EmailDTO
    {
        public EmailAddressDTO From { get; set; }
        public List<EmailAddressDTO> Tos { get; set; }
        public List<EmailAddressDTO> Ccs { get; set; }
        public string TemplateId { get; set; }
        public object TemplateData { get; set; }
    }
}
