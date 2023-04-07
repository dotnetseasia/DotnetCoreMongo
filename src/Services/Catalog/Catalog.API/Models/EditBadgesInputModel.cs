using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models
{
    public class EditBadgesInputModel
    {
        [Required]
        [BsonElement("Name")]
        public string Name { get; set; }
        public string urlImage { get; set; }
        public string Id { get; set; }
    }
}
