using CompanyAdmin.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models
{
    public class ColUserAddModel
    {
        public string UserName { get; set; }
        public string ImageFile { get; set; }

        public string Password { get; set; }

        public EmployeeDetail EmployeeInfo { get; set; }

    }
}
