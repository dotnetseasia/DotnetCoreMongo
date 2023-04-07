using CompanyAdmin.API.Common.DI;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Entities.CommonEntities;
using MongoDB.Driver;

namespace CompanyAdmin.API.Data.Interfaces
{
    public interface ICompanyAdminContext : IAsSelf
    { 
        IMongoCollection<AchievementDetails> AchievementDetails { get; }
    
       // IMongoCollection<UserDetails> UserDetails { get; }
        IMongoCollection<BadgeDetails> BadgeDetails { get; }
        IMongoCollection<ColUser> Users { get; }
        IMongoCollection<Roles> Roles { get; }
        IMongoCollection<colRoleMaster> colRoleMaster { get; }
        IMongoCollection<colDepartment> colDepartment { get; }
        IMongoCollection<ColTechnicalSkills> ColTechnicalSkills { get; }
        IMongoCollection<ColApplicaitonConfiguration> ColApplicaitonConfiguration { get; } 
        IMongoCollection<ColDesignation> ColDesignation { get; }
        IMongoCollection<colSprint> colSprint { get; }

        IMongoCollection<ColProject> ColProject { get; }

        IMongoCollection<colChannel> colChannel { get; }

    }
}
