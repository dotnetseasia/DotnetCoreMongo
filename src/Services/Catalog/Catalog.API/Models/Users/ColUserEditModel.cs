using CompanyAdmin.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models
{
    public class ColUserEditModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string ImageFile { get; set; }

        public string Password { get; set; }

        public EmployeeDetail EmployeeInfo { get; set; }

        public PersonalInfo PersonalInfo { get; set; }

        public List<Documents> Documents { get; set; }

        public List<PastExperince> PastExperince { get; set; }

        public List<Badges> Badges { get; set; }
        public List<Achivements> Achivements { get; set; }
    }
}
