using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models.Designation.Input
{
    public class AddDesignationInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
