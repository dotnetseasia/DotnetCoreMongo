using CompanyAdmin.API.Entities.CommonEntities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyAdmin.API.Entities
{
    public class ColUser : BaseEntity
    {
        [BsonElement("UserName")]
        public string UserName { get; set; }
        public string ImageFile { get; set; }

        public string Token { get; set; }

        public string Password { get; set; }

        public EmployeeDetail EmployeeInfo { get; set; }

        public PersonalInfo PersonalInfo { get; set; }

        public List<Documents> Documents { get; set; }

        public List<PastExperince> PastExperince { get; set; }

        public List<Badges> Badges { get; set; }
        public List<Achivements> Achivements { get; set; }

        public OTPDetail OTPDetail { get; set; }
    }

    public class OTPDetail
    {
        public string OTP { get; set; }
        public string OTPCreatedTime { get; set; }

    }

    public class EmployeeDetail
    {
        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string DepartmentId { get; set; }

        public string ManagerId { get; set; }

        [Required]
        public string RoleId { get; set; }

        [Required]
        public List<TechnicalSkills> SkillSets { get; set; }

        public string SlackId { get; set; }

        public string TeamId { get; set; }

        public string EmailId { get; set; }

        public string BusinessNumber { get; set; }

        [Required]
        public DateTime DateOfJoining { get; set; }

        public int CurrentTeaure { get; set; }

        [Required]
        public string Location { get; set; }

    }

    public class TechnicalSkills
    {
        public string SkillId { get; set; }

        public string Name { get; set; }
    }

    public class PersonalInfo
    {
        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string BloodGroup { get; set; }

        public string MaritalStatus { get; set; }

        public string PersonalEmailId { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfileImagePath { get; set; }
    }

    public class PastExperince
    {
        public string OrganizationName { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        public DateTime WorkedFrom { get; set; }

        [Required]
        public DateTime WorkedTo { get; set; }

        public string WorkType { get; set; }
    }

    public class Documents
    {
        public string DocumentType { get; set; }

        public string FileName { get; set; }
    }

    public class Badges
    {
        public string Id { get; set; }
    }

    public class Achivements
    {
        public string Id { get; set; }
    }
}
