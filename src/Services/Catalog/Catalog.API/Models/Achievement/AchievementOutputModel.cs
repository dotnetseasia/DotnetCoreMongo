using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models
{
    public class AchievementOutputModel
    {
        public string Id { get; set; }
        public string AchievementName { get; set; }

        public bool? IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
    }
}
