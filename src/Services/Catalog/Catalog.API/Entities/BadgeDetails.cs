using CompanyAdmin.API.Entities.CommonEntities;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Entities
{
    public class BadgeDetails : BaseEntity
    {
        [BsonElement("BadgeName")]
        public string BadgeName { get; set; }
        public string urlImage { get; set; }
    }
}
