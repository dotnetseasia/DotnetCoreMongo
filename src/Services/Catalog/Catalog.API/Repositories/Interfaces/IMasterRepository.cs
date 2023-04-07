using CompanyAdmin.API.Common.DI;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models;
using CompanyAdmin.API.Entities.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Repositories.Interfaces
{
    public interface IMasterRepository : IAsSelf
    {
        Task<IEnumerable<colDepartment>> GetAllDepartments();
        Task<colDepartment> GetDepartmentDetails(string id);
        Task<bool> DeleteDepartment(colDepartment colDept);
        Task<string> CreateDepartment(colDepartment colDept);
        Task<string> UpdateDepartment(colDepartment colDept);
        Task<colDepartment> GetDepartmentByName(string departmentName);
        Task<colDepartment> ValidateDuplicateDepartmentName(string id,string departmentName);

        #region TechnicalSkills
        Task<string> AddTechnicalSkillsAsync(ColTechnicalSkills ColTechnicalSkills);
        Task<IEnumerable<ColTechnicalSkills>> GetAllTechnicalSkillsAsync();
        Task<ColTechnicalSkills> GetTechnicalSkillsByIdAsync(string id);
        Task<ColTechnicalSkills> GetTechnicalSkillsByNameAsync(string techSkillsName);
        Task<bool> DeleteTechnicalSkillsByIdAsync(string id);
        Task<string> UpdateTechnicalSkillsAsync(ColTechnicalSkills colTechSkills);
        Task<ColTechnicalSkills> ValidateDuplicateTechnicalSkillNameAsync(string id, string TechnicalSkillName);
        #endregion

        #region Designation
        Task<ColDesignation> GetDesignationByNameAsync(string DesignationName);
        Task<string> AddDesignationAsync(ColDesignation ColDesignationss);
        Task<IEnumerable<ColDesignation>> GetAllDesignationAsync();
        Task<ColDesignation> GetDesignationByIdAsync(string id);
        Task<bool> DeleteDesignationByIdAsync(string id);
        Task<string> UpdateDesignationAsync(ColDesignation ColDesignation);
        Task<ColDesignation> ValidateDuplicateDesignationNameAsync(string id, string DesignationName);
        #endregion

        #region Achievement
        Task<IEnumerable<AchievementDetails>> GetAllAchievementsAsync();
        Task<AchievementDetails> GetAchievementDetailByIdAsync(string id);
        Task<string> CreateAchievementAsync(AchievementDetails achievementDetail);
        Task<string> UpdateAchievementAsync(AchievementDetails achievementDetail);
        Task<bool> DeleteAchievementAsync(string id);
        Task<AchievementDetails> GetAchievementDetailByNameAsync(string name);
        Task<AchievementDetails> ValidateAchievementDetailsAsync(string id, string name);
        BadgeDetails ValidateBadgeDetails(string id , string name);
         BadgeDetails GetBadgeByName(string name);
         Task<BadgeDetails> GetBadgeByIdAsync(string id);
         Task<IEnumerable<BadgeDetails>> GetAllBadgesAsync();
         Task<bool> UpdateBadgeDetailAsync(BadgeDetails badgeDetails);
         Task<bool> DeleteBadgeAsync(string id);
         Task<string> CreateBadgeAsync(BadgeDetails badgeDetails);
        #endregion
       

        #region channel
        Task<IEnumerable<colChannel>> GetAllChannelAsync();
        Task<string> CreateChannelAsync(colChannel colChan);
        Task<colChannel> GetChannelByNameAsync(string ChannelName);
        Task<bool> DeleteChannelAsync(string id);

        Task<colChannel> GetChannelDetailsAsync(string id);

        Task<string> UpdateChannelAsync(colChannel colChan);
        Task<colChannel> ValidateDuplicateChannelNameAsync(string id, string ChannelName);
        #endregion
    }
}
