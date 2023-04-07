using AutoMapper;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Entities.CommonEntities;
using CompanyAdmin.API.Models;
using CompanyAdmin.API.Models.Channel.Input;
using CompanyAdmin.API.Models.Channel.Output;
using CompanyAdmin.API.Models.Department.Input;
using CompanyAdmin.API.Models.Department.Output;
using CompanyAdmin.API.Models.Designation.Input;
using CompanyAdmin.API.Models.Designation.Output;
using CompanyAdmin.API.Models.TechnicalSkills.Input;
using CompanyAdmin.API.Models.TechnicalSkills.Output;
using CompanyAdmin.API.Models.OutputModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyAdmin.API.Models.Project;
using CompanyAdmin.API.Models.Sprint.Input;
using CompanyAdmin.API.Models.Sprint.Output;

namespace CompanyAdmin.API.Data
{
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// All our mappings will go here.
        /// </summary>
        /// <remarks>
        /// The cool new way to map stuff in a queryable is to use .ProjectTo().
        /// return await GetAllAsQueryable()
        ///         .Where(p =&gt; p.Id == id)
        ///         .ProjectTo&lt;SettingDto&gt;(_mapper.ConfigurationProvider)
        ///         .FirstOrDefaultAsync();
        /// </remarks>
        public AutoMapperProfile()
        {
            MapUserDetails();
            MapRoleDetails();
            MapDepartments();
            MapAchievementDetails();
            MapTechnicalSkills();
            MapDesignation();
            MapBadgeDetails();
            MapChannel();
            MapProjectDetails();
            MapSprint();
        }
        private void MapUserDetails()
        {
            CreateMap<ColUserAddModel, ColUser>()
                .ForMember(p => p.PersonalInfo, opt => opt.Ignore())
                .ForMember(p => p.Documents, opt => opt.Ignore())
                .ForMember(p => p.PastExperince, opt => opt.Ignore())
                .ForMember(p => p.Badges, opt => opt.Ignore())
                .ForMember(p => p.Achivements, opt => opt.Ignore())
                .ForMember(p => p.OTPDetail, opt => opt.Ignore())
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.Token, opt => opt.Ignore())
                .ForMember(p => p.IsDeleted, opt => opt.Ignore())
                .ForMember(p => p.CreatedBy, opt => opt.Ignore())
                .ForMember(p => p.CreatedDate, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedBy, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedDate, opt => opt.Ignore())
                .ForMember(p => p.PersonalInfo, opt => opt.Ignore())
                .ForMember(p => p.Badges, opt => opt.Ignore())
                .ForMember(p => p.Achivements, opt => opt.Ignore())
                  .ForMember(p => p.OTPDetail, opt => opt.Ignore())
                    .ForMember(p => p.PastExperince, opt => opt.Ignore())
                .ForMember(p => p.Documents, opt => opt.Ignore());
            CreateMap<ColUserEditModel, ColUser>()
                 .ForMember(p => p.Token, opt => opt.Ignore())
                 .ForMember(p => p.IsDeleted, opt => opt.Ignore())
                 .ForMember(p => p.CreatedBy, opt => opt.Ignore())
                 .ForMember(p => p.CreatedDate, opt => opt.Ignore())
                 .ForMember(p => p.LastModifiedBy, opt => opt.Ignore())
                 .ForMember(p => p.LastModifiedDate, opt => opt.Ignore())
                 .ForMember(p => p.OTPDetail, opt => opt.Ignore());
            CreateMap<ColUser, UserDetailsViewModel>();

        }
        private void MapRoleDetails()
        {
            CreateMap<Roles, RoleDetailsViewModel>();
            CreateMap<Roles, GetRoleDetailsModel>();
            CreateMap<RoleDetailsAddModel, Roles>()
                 .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.IsDeleted, opt => opt.Ignore())
                .ForMember(p => p.CreatedBy, opt => opt.Ignore())
                .ForMember(p => p.CreatedDate, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedBy, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedDate, opt => opt.Ignore());
            ;
        }
        private void MapAchievementDetails()
        {

            CreateMap<AchievementDetails, AchievementOutputModel>();
            CreateMap<AddAchievementInputModel, AchievementDetails>()
            .ForMember(p => p.Id, opt => opt.Ignore())
            .ForMember(p => p.AchievementName, opt => opt.Ignore())
            .ForMember(p => p.IsDeleted, opt => opt.Ignore())
            .ForMember(p => p.CreatedBy, opt => opt.Ignore())
            .ForMember(p => p.CreatedDate, opt => opt.Ignore())
            .ForMember(p => p.LastModifiedBy, opt => opt.Ignore())
            .ForMember(p => p.LastModifiedDate, opt => opt.Ignore());

        }

        private void MapDepartments()
        {
            CreateMap<colDepartment, DepartmentOutputModel>();
            CreateMap<AddDepartmentInputModel, colDepartment>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.IsDeleted, opt => opt.Ignore())
                .ForMember(p => p.CreatedBy, opt => opt.Ignore())
                .ForMember(p => p.CreatedDate, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedBy, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedDate, opt => opt.Ignore());
        }
        private void MapTechnicalSkills()
        {
            CreateMap<ColTechnicalSkills, TechnicalSkillsOutputModel>();
            CreateMap<AddTechnicalSkillsInputModel, ColTechnicalSkills>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.IsDeleted, opt => opt.Ignore())
                .ForMember(p => p.CreatedBy, opt => opt.Ignore())
                .ForMember(p => p.CreatedDate, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedBy, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedDate, opt => opt.Ignore());
        }
        private void MapDesignation()
        {
            CreateMap<ColDesignation, DesignationOutputModel>();
            CreateMap<AddDesignationInputModel, ColDesignation>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.IsDeleted, opt => opt.Ignore())
                .ForMember(p => p.CreatedBy, opt => opt.Ignore())
                .ForMember(p => p.CreatedDate, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedBy, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedDate, opt => opt.Ignore());
        }

        private void MapBadgeDetails()
        {
            CreateMap<BadgeDetails, GetBadgesOutputModel>();
            CreateMap<AddBadgesInputModel, BadgeDetails>()
                .ForMember(p => p.BadgeName, opt => opt.Ignore())
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.IsDeleted, opt => opt.Ignore())
                .ForMember(p => p.CreatedBy, opt => opt.Ignore())
                .ForMember(p => p.CreatedDate, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedBy, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedDate, opt => opt.Ignore());
        }


        private void MapChannel()
        {
            CreateMap<colChannel, ChannelOutputModel>();
            CreateMap<AddChannelInputModel, colChannel>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.IsDeleted, opt => opt.Ignore())
                .ForMember(p => p.CreatedBy, opt => opt.Ignore())
                .ForMember(p => p.CreatedDate, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedBy, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedDate, opt => opt.Ignore());
        }

        private void MapProjectDetails()
        {
            CreateMap<ColProjectAddModel, ColProject>()
                .ForMember(p => p.IsDeleted, opt => opt.Ignore())
                .ForMember(p => p.CreatedBy, opt => opt.Ignore())
                .ForMember(p => p.CreatedDate, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedBy, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedDate, opt => opt.Ignore())
                .ForMember(p => p.Id, opt => opt.Ignore())
                ;
        }
        private void MapSprint()
        {
            CreateMap<colSprint, SprintOutputModel>()
                .ForMember(p => p.Goal, opt => opt.Ignore())
            .ForMember(p => p.Duration, opt => opt.Ignore())
            .ForMember(p => p.StartDate, opt => opt.Ignore())
            .ForMember(p => p.EndDate, opt => opt.Ignore());
            CreateMap<AddSprintInputModel, colSprint>()
                 .ForMember(p => p.IsDeleted, opt => opt.Ignore())
                .ForMember(p => p.CreatedBy, opt => opt.Ignore())
                .ForMember(p => p.CreatedDate, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedBy, opt => opt.Ignore())
                .ForMember(p => p.LastModifiedDate, opt => opt.Ignore())
                .ForMember(p => p.Id, opt => opt.Ignore())
            ;

        }
    }
}
