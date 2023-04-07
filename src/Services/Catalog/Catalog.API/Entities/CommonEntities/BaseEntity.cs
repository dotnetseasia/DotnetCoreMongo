using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Entities.CommonEntities
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public bool? IsDeleted { get; set; } = false;
        [BsonRepresentation(BsonType.ObjectId)]
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTime.UtcNow;
        [BsonRepresentation(BsonType.ObjectId)]
        public string LastModifiedBy { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
