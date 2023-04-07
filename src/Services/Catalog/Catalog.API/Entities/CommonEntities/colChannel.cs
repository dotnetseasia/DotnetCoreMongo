using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Entities.CommonEntities
{
    public class colChannel  : BaseEntity
    {
        [BsonElement("Name")]
        public string Name { get; set; }
    }
}
