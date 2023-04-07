using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Entities
{
    public class ColClientDetails
    {
        public string Email { get; set; }

        public string SkypeId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string Country { get; set; }

        public string CompanyName { get; set; }

        public string MobileNumber { get; set; }

        public string Code { get; set; }

        public string AlterNateNumber { get; set; }

        public string CompanyDescription { get; set; }
    }
}
