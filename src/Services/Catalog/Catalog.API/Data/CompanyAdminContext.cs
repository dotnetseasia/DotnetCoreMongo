using CompanyAdmin.API.Data.Interfaces;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Entities.CommonEntities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace CompanyAdmin.API.Data
{
    public class CompanyAdminContext : ICompanyAdminContext
    {
        public CompanyAdminContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            var isTestDb = configuration.GetValue<string>("DatabaseSettings:IsTest");


            Users = database.GetCollection<ColUser>("colUsers");
            Roles = database.GetCollection<Roles>("colRoles");
            colRoleMaster = database.GetCollection<colRoleMaster>("colRoleMaster");
            colDepartment = database.GetCollection<colDepartment>("colDepartment");
            colChannel = database.GetCollection<colChannel>("colChannel");
            ColApplicaitonConfiguration = database.GetCollection<ColApplicaitonConfiguration>("ColApplicaitonConfiguration");
            AchievementDetails = database.GetCollection<AchievementDetails>("AchievementDetails"); 
            BadgeDetails = database.GetCollection<BadgeDetails>("BadgeDetails");

            ColTechnicalSkills = database.GetCollection<ColTechnicalSkills>("ColTechnicalSkills");
            ColDesignation = database.GetCollection<ColDesignation>("ColDesignation");
            ColProject = database.GetCollection<ColProject>("ColProject");
            colSprint = database.GetCollection<colSprint>("colSprint");

            if (isTestDb != null && isTestDb == "true")
            {
                //Every table to clear data we need to add this method
                CompanyAdminContextSeed.ClearUserDetailsData(Users);
                CompanyAdminContextSeed.ClearRolesData(Roles);
                CompanyAdminContextSeed.ClearTechnicalSkillsData(ColTechnicalSkills);
                CompanyAdminContextSeed.ClearDesignationData(ColDesignation);
                CompanyAdminContextSeed.ClearAchievementData(AchievementDetails);
                CompanyAdminContextSeed.ClearDepartmentData(colDepartment);
                CompanyAdminContextSeed.ClearSprintData(colSprint);
            }
            else
            {
                CompanyAdminContextSeed.SeedData(Users);
            }
        }

        public IMongoCollection<ColUser> Users { get; }
        public IMongoCollection<Roles> Roles { get; }
        public IMongoCollection<colRoleMaster> colRoleMaster { get; }
        public IMongoCollection<colDepartment> colDepartment { get; }
        public IMongoCollection<ColTechnicalSkills> ColTechnicalSkills { get; }
        public IMongoCollection<ColApplicaitonConfiguration> ColApplicaitonConfiguration { get; }
        public IMongoCollection<ColDesignation> ColDesignation { get; }
        public IMongoCollection<AchievementDetails> AchievementDetails { get; }

        public IMongoCollection<BadgeDetails> BadgeDetails { get; }

        public IMongoCollection<colChannel> colChannel { get; }

        public IMongoCollection<ColProject> ColProject { get; }
        public IMongoCollection<colSprint> colSprint { get; }
    }
}
