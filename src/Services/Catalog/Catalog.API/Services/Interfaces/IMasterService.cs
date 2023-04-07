using CompanyAdmin.API.Common;
using CompanyAdmin.API.Common.DI;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models.Channel.Input;
using CompanyAdmin.API.Models.Department.Input;
using CompanyAdmin.API.Models.Designation.Input;
using CompanyAdmin.API.Models.TechnicalSkills.Input;

using CompanyAdmin.API.Models;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services.Interfaces
{
    public interface IMasterService : IAsSelf
    {
        Task<ServiceResponse> GetAllDepartments();
        Task<ServiceResponse> GetDepartmentDetails(string id);
        Task<ServiceResponse> DeleteDepartment(string id);
        Task<ServiceResponse> CreateDepartment(AddDepartmentInputModel inputModel);
        Task<ServiceResponse> UpdateDepartment(EditDepartmentInputModel inputModel);

        #region TechnicalSkills
        Task<ServiceResponse> AddTechnicalSkillsAsync(AddTechnicalSkillsInputModel inputModel);
        Task<ServiceResponse> GetAllTechnicalSkillsAsync();
        Task<ServiceResponse> GetTechnicalSkillsByIdAsync(string id);
        Task<ServiceResponse> DeleteTechnicalSkillsByIdAsync(string id);
        Task<ServiceResponse> UpdateTechnicalSkillsAsync(EditTechnicalSkillsInputModel inputModel);
        #endregion

        #region Designation
        Task<ServiceResponse> AddDesignationAsync(AddDesignationInputModel inputModel);
        Task<ServiceResponse> GetAllDesignationAsync();
        Task<ServiceResponse> GetDesignationByIdAsync(string id);
        Task<ServiceResponse> DeleteDesignationByIdAsync(string id);
        Task<ServiceResponse> UpdateDesignationAsync(EditDesignationInputModel inputModel);
        #endregion 

        #region Achievement
        Task<ServiceResponse> CreateAchievementAsync(AddAchievementInputModel addAchievement);
        Task<ServiceResponse> UpdateAchievementAsync(EditAchievementInputModel editAchievement);
        Task<ServiceResponse> DeleteAchievementAsync(string id);
        Task<ServiceResponse> GetAchievementDetailByIdAsync(string id);       
        Task <ServiceResponse> GetAllAchievementsAsync();       
        #endregion
        // Task<IEnumerable<BadgeDetails>> GetBadgeDetails();
        // BadgeDetails GetBadgeByName(string name);
        Task<ServiceResponse> GetBadgeByIdAsync(string id);
        Task<ServiceResponse> GetAllBadgesAsync();
        Task<ServiceResponse> CreateBadgeAsync(AddBadgesInputModel inputModel);
        Task<ServiceResponse> UpdateBadgeAsync(EditBadgesInputModel inputModel);
        Task<ServiceResponse> DeleteBadgeAsync(string id);
        #region Channel

      
        Task<ServiceResponse> GetAllChannelAsync();
        Task<ServiceResponse> GetChannelDetailsAsync(string id);

        Task<ServiceResponse> DeleteChannelAsync(string id);
        Task<ServiceResponse> CreateChannelAsync(AddChannelInputModel inputModel);

        Task<ServiceResponse> UpdateChannelAsync(EditChannelInputModel inputModel);
        
        #endregion

    }
}