using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models
{
    public class RoleDetailsEditModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Id { get; set; }
    }
}
