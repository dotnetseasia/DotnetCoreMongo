using CompanyAdmin.API.Entities.CommonEntities;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Entities
{
    public class ColProject : BaseEntity
    {
        [BsonElement("ProjectName")]
        public string ProjectName { get; set; }

        public ProjectDetails ProjectDetails { get; set; }

        public string ClientId { get; set; }

        public bool IsExsitingClient { get; set; }

        public PointOfContact PointOfContact { get; set; }

        public string ProjectOwnerId { get; set; }

        public ProjectDepartment ProjectDepartment { get; set; }

        public PaymentDetails PaymentDetails { get; set; }

        public List<TechnicalSkills> SkillSets { get; set; }

    }

    public class ProjectDepartment
    {
        public string DepartmentId { get; set; }
    }

    public class ProjectDetails
    {
        public string Pin { get; set; }

        public string KeyWords { get; set; }

        public string Channel { get; set; }

        public string Costing { get; set; }

        public string PinEfforts { get; set; }

        public string PinAmount { get; set; }

        public string ProjectDescription { get; set; }
    }

    public class PointOfContact
    {
        public string Email { get; set; }

        public string SkypeId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string MobileNumber { get; set; }

        public string Code { get; set; }

        public string AlterNateNumber { get; set; }
    }

    public class PaymentDetails
    {
        public string CostingType { get; set; }

        public string UpfrontPayment { get; set; }

        public string Currency { get; set; }

        public string BillingType { get; set; }

        public string RatePerHour { get; set; }

        public List<DepartmentRatePerHour> DepartmentRatePerHours { get; set; }
    }

    public class DepartmentRatePerHour
    {
        public string DepartmentId { get; set; }

        public string RatePerHour { get; set; }
    }
}
