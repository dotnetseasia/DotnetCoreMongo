using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models.TechnicalSkills.Input
{
    public class AddTechnicalSkillsInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
