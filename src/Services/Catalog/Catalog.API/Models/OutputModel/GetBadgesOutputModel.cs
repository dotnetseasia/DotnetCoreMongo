using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Models.OutputModel
{
    public class GetBadgesOutputModel
    {
        public string Id { get; set; }
        public string BadgeName { get; set; }
        public string urlImage { get; set; }
        public bool? IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
    }
}
