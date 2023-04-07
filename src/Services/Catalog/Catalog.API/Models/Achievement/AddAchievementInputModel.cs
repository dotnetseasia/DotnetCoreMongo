using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models
{
    public class AddAchievementInputModel
    {
       
        [Required]
        [BsonElement("AchievementName")]
        public string AchievementName { get; set; }
    }
}
