using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models
{
    public class RoleDetailDeleteModel
    {
        [Required]
        public string id { get; set; }
    }
}
