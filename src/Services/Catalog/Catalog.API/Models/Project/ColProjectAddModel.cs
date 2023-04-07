using CompanyAdmin.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models.Project
{
    public class ColProjectAddModel
    {
        public string ProjectName { get; set; }

        public ProjectDetails ProjectDetails { get; set; }

        public string ClientId { get; set; }

        public bool IsExsitingClient { get; set; }

        public PointOfContact PointOfContact { get; set; }

        public string ProjectOwnerId { get; set; }

        public ProjectDepartment ProjectDepartment { get; set; }

        public PaymentDetails PaymentDetails { get; set; }

        public List<CompanyAdmin.API.Entities.TechnicalSkills> SkillSets { get; set; }
    }
}
