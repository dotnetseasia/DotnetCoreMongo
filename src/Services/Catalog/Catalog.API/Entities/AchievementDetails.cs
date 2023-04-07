using CompanyAdmin.API.Entities.CommonEntities;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Entities
{
    public class AchievementDetails : BaseEntity
    {
        [BsonElement("AchievementName")]
        [Required]
        public string AchievementName { get; set; }
        
     
    }
}
