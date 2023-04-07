using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models.Users
{
    public class GetAllUsersInputModel
    {
        public string Department { get; set; }
        public string Status { get; set; }
        public string Search { get; set; }
        public int? Take { get; set; }
        public int? Skip { get; set; }
    }
}
