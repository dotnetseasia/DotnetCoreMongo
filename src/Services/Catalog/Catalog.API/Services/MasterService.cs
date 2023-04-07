using AutoMapper;
using CompanyAdmin.API.Common;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Entities.CommonEntities;
using CompanyAdmin.API.Models.Channel.Input;
using CompanyAdmin.API.Models.Channel.Output;
using CompanyAdmin.API.Models.Department.Input;
using CompanyAdmin.API.Models.Department.Output;
using CompanyAdmin.API.Models.Designation.Input;
using CompanyAdmin.API.Models.Designation.Output;
using CompanyAdmin.API.Models.TechnicalSkills.Input;
using CompanyAdmin.API.Models.TechnicalSkills.Output;
using CompanyAdmin.API.Models;
using AutoMapper;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models;
using CompanyAdmin.API.Models.OutputModel;
using CompanyAdmin.API.Repositories.Interfaces;
using CompanyAdmin.API.Services.Interfaces;

using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services
{
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository _repository;
        private readonly ILogger<MasterService> _logger;
        private readonly IMapper _mapper;

        public MasterService(IMasterRepository repository, ILogger<MasterService> logger, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }
        #region Department
        public async Task<ServiceResponse> GetAllDepartments()
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                List<DepartmentOutputModel> outputModels = new List<DepartmentOutputModel>();
                var list = await _repository.GetAllDepartments();
                if (list != null)
                {
                    outputModels = _mapper.Map<List<DepartmentOutputModel>>(list);
                }
                objResponse.result = outputModels;
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }
        public async Task<ServiceResponse> GetDepartmentDetails(string id)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {

                var dep = await _repository.GetDepartmentDetails(id);
                if (dep != null)
                {
                    DepartmentOutputModel obj = _mapper.Map<DepartmentOutputModel>(dep);
                    objResponse.result = obj;
                }

            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }
        public async Task<ServiceResponse> DeleteDepartment(string id)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                colDepartment objSelectedDepartment = await _repository.GetDepartmentDetails(id);
                if (objSelectedDepartment == null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Invalid department Id.");
                }
                else
                {
                    if (objSelectedDepartment.IsDeleted == true)
                    {
                        objResponse.isError = true;
                        objResponse.listErrors.Add("Department already deleted.");
                    }
                    else
                    {
                        objSelectedDepartment.IsDeleted = true;
                        objSelectedDepartment.LastModifiedBy = "602d2149e773f2a3990b47f5";
                        objSelectedDepartment.LastModifiedDate = DateTime.UtcNow;
                        objResponse.result = await _repository.DeleteDepartment(objSelectedDepartment);
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }
        public async Task<ServiceResponse> CreateDepartment(AddDepartmentInputModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                //validate duplicate name
                colDepartment checkDuplicateDepartment = await _repository.GetDepartmentByName(inputModel.Name);
                if (checkDuplicateDepartment != null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Department Name already exists.");
                }
                else
                {
                    colDepartment objDepartment = _mapper.Map<colDepartment>(inputModel);
                    objDepartment.CreatedBy = "602d2149e773f2a3990b47f5";
                    objDepartment.LastModifiedBy = "602d2149e773f2a3990b47f5";
                    objResponse.result = await _repository.CreateDepartment(objDepartment);
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }
        public async Task<ServiceResponse> UpdateDepartment(EditDepartmentInputModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                //validate duplicate name
                colDepartment objSelectedDepartment = await _repository.GetDepartmentDetails(inputModel.Id);
                if (objSelectedDepartment == null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Invalid department Id.");
                }
                else
                {
                    colDepartment checkDuplicateDepartmentName = await _repository.ValidateDuplicateDepartmentName(inputModel.Id, inputModel.Name);
                    if (checkDuplicateDepartmentName != null)
                    {
                        objResponse.isError = true;
                        objResponse.listErrors.Add("Another department already exists with this name.");
                    }
                    else
                    {
                        objSelectedDepartment.Name = inputModel.Name;
                        objSelectedDepartment.LastModifiedBy = "602d2149e773f2a3990b47f5";
                        objResponse.result = await _repository.UpdateDepartment(objSelectedDepartment);
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }
        #endregion
        #region Channel
        public async Task<ServiceResponse> GetAllChannelAsync()
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                List<ChannelOutputModel> outputModels = new List<ChannelOutputModel>();
                var list = await _repository.GetAllChannelAsync();
                if (list != null)
                {
                    outputModels = _mapper.Map<List<ChannelOutputModel>>(list);
                }
                objResponse.result = outputModels;
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> CreateChannelAsync(AddChannelInputModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                //validate duplicate name
                colChannel checkDuplicateChannel = await _repository.GetChannelByNameAsync(inputModel.Name);
                if (checkDuplicateChannel != null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Channel Name already exists.");
                }
                else
                {
                    colChannel objChannel = _mapper.Map<colChannel>(inputModel);
                    objChannel.IsDeleted = false;
                    objChannel.CreatedDate = DateTime.Now;
                    objChannel.LastModifiedDate = DateTime.Now;
                    objChannel.CreatedBy = "602d2149e773f2a3990b47f5";
                    objChannel.LastModifiedBy = "602d2149e773f2a3990b47f5";
                    objResponse.result = await _repository.CreateChannelAsync(objChannel);
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }
        public async Task<ServiceResponse> DeleteChannelAsync(string id)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                objResponse.result = await _repository.DeleteChannelAsync(id);
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> UpdateChannelAsync(EditChannelInputModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                //validate duplicate name
                colChannel objSelectedChannel = await _repository.GetChannelDetailsAsync(inputModel.Id);
                if (objSelectedChannel == null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Invalid Channel Id.");
                }
                else
                {
                    colChannel checkDuplicateChannelName = await _repository.ValidateDuplicateChannelNameAsync(inputModel.Id, inputModel.Name);
                    if (checkDuplicateChannelName != null)
                    {
                        objResponse.isError = true;
                        objResponse.listErrors.Add("Another Channel already exists with this name.");
                    }
                    else
                    {
                        objSelectedChannel.Name = inputModel.Name;
                        objSelectedChannel.LastModifiedDate = DateTime.Now;
                        objSelectedChannel.LastModifiedBy = "602d2149e773f2a3990b47f5";
                        objResponse.result = await _repository.UpdateChannelAsync(objSelectedChannel);
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> GetChannelDetailsAsync(string id)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {

                var dep = await _repository.GetChannelDetailsAsync(id);
                if (dep != null)
                {
                    ChannelOutputModel obj = _mapper.Map<ChannelOutputModel>(dep);
                    objResponse.result = obj;
                }

            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }


        #endregion

        #region Badges

        public async Task<ServiceResponse> CreateBadgeAsync(AddBadgesInputModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                //validate duplicate name
                BadgeDetails checkDuplicateBadge = _repository.GetBadgeByName(inputModel.Name);
                if (checkDuplicateBadge != null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Badge Name already exists.");
                }
                else
                {
                    BadgeDetails objBadge = _mapper.Map<BadgeDetails>(inputModel);
                    objBadge.BadgeName = inputModel.Name;
                    objBadge.urlImage = inputModel.urlImage;
                    objBadge.IsDeleted = false;
                    objBadge.CreatedDate = DateTime.Now;
                    objBadge.LastModifiedDate = DateTime.Now;
                    objBadge.CreatedBy = "602d2149e773f2a3990b47f5";
                    objBadge.LastModifiedBy = "602d2149e773f2a3990b47f5";
                    objResponse.result = await _repository.CreateBadgeAsync(objBadge);
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> UpdateBadgeAsync(EditBadgesInputModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                //validate duplicate name
                BadgeDetails objSelectedBadge = await _repository.GetBadgeByIdAsync(inputModel.Id);
                if (objSelectedBadge == null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Badge not exist");
                }
                else
                {
                    BadgeDetails checkDuplicateBadgeName = _repository.ValidateBadgeDetails(inputModel.Id, inputModel.Name);
                    if (checkDuplicateBadgeName != null)
                    {
                        objResponse.isError = true;
                        objResponse.listErrors.Add("Another badge already exists with this name.");
                    }
                    else
                    {
                        objSelectedBadge.BadgeName = inputModel.Name;
                        objSelectedBadge.urlImage = inputModel.urlImage;
                        objSelectedBadge.LastModifiedDate = DateTime.Now;
                        objSelectedBadge.LastModifiedBy = "602d2149e773f2a3990b47f5";
                        objResponse.result = await _repository.UpdateBadgeDetailAsync(objSelectedBadge);
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> DeleteBadgeAsync(string id)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                objResponse.result = await _repository.DeleteBadgeAsync(id);
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Exeception" + ex.InnerException?.Message);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> GetBadgeByIdAsync(string id)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                var result = await _repository.GetBadgeByIdAsync(id);
                if (result != null)
                {
                    objResponse.result = _mapper.Map<GetBadgesOutputModel>(result);
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Exeception" + ex.InnerException?.Message);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> GetAllBadgesAsync()
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                List<GetBadgesOutputModel> outputModels = new List<GetBadgesOutputModel>();
                var list = await _repository.GetAllBadgesAsync();
                if (list != null)
                {
                    outputModels = _mapper.Map<List<GetBadgesOutputModel>>(list);
                }
                objResponse.result = outputModels;
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }
        #endregion Badges

        #region TechnicalSkills

        public async Task<ServiceResponse> AddTechnicalSkillsAsync(AddTechnicalSkillsInputModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                //validate duplicate name
                ColTechnicalSkills checkDuplicateTechSkills = await _repository.GetTechnicalSkillsByNameAsync(inputModel.Name);
                if (checkDuplicateTechSkills != null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Technical Skill Name already exists.");
                }
                else
                {
                    ColTechnicalSkills objTechnicalSkills = _mapper.Map<ColTechnicalSkills>(inputModel);
                    objTechnicalSkills.IsDeleted = false;
                    objTechnicalSkills.CreatedDate = DateTime.Now;
                    objTechnicalSkills.LastModifiedDate = DateTime.Now;
                    objTechnicalSkills.CreatedBy = "602d2149e773f2a3990b47f5";
                    objTechnicalSkills.LastModifiedBy = "602d2149e773f2a3990b47f5";
                    objResponse.result = await _repository.AddTechnicalSkillsAsync(objTechnicalSkills);
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> GetAllTechnicalSkillsAsync()
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                List<TechnicalSkillsOutputModel> outputModels = new List<TechnicalSkillsOutputModel>();
                var list = await _repository.GetAllTechnicalSkillsAsync();
                if (list != null)
                {
                    outputModels = _mapper.Map<List<TechnicalSkillsOutputModel>>(list);
                }
                objResponse.result = outputModels;
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> GetTechnicalSkillsByIdAsync(string id)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {

                var list = await _repository.GetTechnicalSkillsByIdAsync(id);
                if (list != null)
                {
                    TechnicalSkillsOutputModel obj = _mapper.Map<TechnicalSkillsOutputModel>(list);
                    objResponse.result = obj;
                }

            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> DeleteTechnicalSkillsByIdAsync(string id)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                objResponse.result = await _repository.DeleteTechnicalSkillsByIdAsync(id);
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> UpdateTechnicalSkillsAsync(EditTechnicalSkillsInputModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                //validate duplicate name
                ColTechnicalSkills objSelectedTechnicalSkills = await _repository.GetTechnicalSkillsByIdAsync(inputModel.Id);
                if (objSelectedTechnicalSkills == null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Invalid Technical Skill Id.");
                }
                else
                {
                    ColTechnicalSkills checkDuplicateTechnicalSkillName = await _repository.ValidateDuplicateTechnicalSkillNameAsync(inputModel.Id, inputModel.Name);
                    if (checkDuplicateTechnicalSkillName != null)
                    {
                        objResponse.isError = true;
                        objResponse.listErrors.Add("Another technical skill already exists with this name.");
                    }
                    else
                    {
                        objSelectedTechnicalSkills.Name = inputModel.Name;
                        objSelectedTechnicalSkills.LastModifiedDate = DateTime.Now;
                        objSelectedTechnicalSkills.LastModifiedBy = "602d2149e773f2a3990b47f5";
                        objResponse.result = await _repository.UpdateTechnicalSkillsAsync(objSelectedTechnicalSkills);
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }
        #endregion

        #region Designation
        public async Task<ServiceResponse> AddDesignationAsync(AddDesignationInputModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                //validate duplicate name
                ColDesignation checkDuplicateDesignation = await _repository.GetDesignationByNameAsync(inputModel.Name);
                if (checkDuplicateDesignation != null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Designation Name already exists.");
                }
                else
                {
                    ColDesignation objDesignation = _mapper.Map<ColDesignation>(inputModel);
                    objDesignation.IsDeleted = false;
                    objDesignation.CreatedDate = DateTime.Now;
                    objDesignation.LastModifiedDate = DateTime.Now;
                    objDesignation.CreatedBy = "602d2149e773f2a3990b47f5";
                    objDesignation.LastModifiedBy = "602d2149e773f2a3990b47f5";
                    objResponse.result = await _repository.AddDesignationAsync(objDesignation);
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }
        public async Task<ServiceResponse> GetAllDesignationAsync()
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                List<DesignationOutputModel> outputModels = new List<DesignationOutputModel>();
                var list = await _repository.GetAllDesignationAsync();
                if (list != null)
                {
                    outputModels = _mapper.Map<List<DesignationOutputModel>>(list);
                }
                objResponse.result = outputModels;
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }
        public async Task<ServiceResponse> GetDesignationByIdAsync(string id)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {

                var list = await _repository.GetDesignationByIdAsync(id);
                if (list != null)
                {
                    DesignationOutputModel obj = _mapper.Map<DesignationOutputModel>(list);
                    objResponse.result = obj;
                }

            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }
        public async Task<ServiceResponse> DeleteDesignationByIdAsync(string id)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                objResponse.result = await _repository.DeleteDesignationByIdAsync(id);
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }
        public async Task<ServiceResponse> UpdateDesignationAsync(EditDesignationInputModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                //validate duplicate name
                ColDesignation objSelectedDesignation = await _repository.GetDesignationByIdAsync(inputModel.Id);
                if (objSelectedDesignation == null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Invalid Designation Id.");
                }
                else
                {
                    ColDesignation checkDuplicateDesignationName = await _repository.ValidateDuplicateDesignationNameAsync(inputModel.Id, inputModel.Name);
                    if (checkDuplicateDesignationName != null)
                    {
                        objResponse.isError = true;
                        objResponse.listErrors.Add("Another Designation already exists with this name.");
                    }
                    else
                    {
                        objSelectedDesignation.Name = inputModel.Name;
                        objSelectedDesignation.LastModifiedDate = DateTime.Now;
                        objSelectedDesignation.LastModifiedBy = "602d2149e773f2a3990b47f5";
                        objResponse.result = await _repository.UpdateDesignationAsync(objSelectedDesignation);
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }
        #endregion

        #region Achievement

        public async Task<ServiceResponse> GetAllAchievementsAsync()
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                List<AchievementOutputModel> outputModels = new List<AchievementOutputModel>();
                var list = await _repository.GetAllAchievementsAsync();
                if (list != null)
                {
                    outputModels = _mapper.Map<List<AchievementOutputModel>>(list);
                }
                objResponse.result = outputModels;
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }
        public async Task<ServiceResponse> GetAchievementDetailByIdAsync(string id)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                objResponse.result = await _repository.GetAchievementDetailByIdAsync(id);
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Exeception" + ex.InnerException?.Message);
            }
            return objResponse;
        }


        public async Task<ServiceResponse> CreateAchievementAsync(AddAchievementInputModel addAchievement)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                //validate duplicate name
                AchievementDetails checkDuplicateAchievement = await _repository.GetAchievementDetailByNameAsync(addAchievement.AchievementName);
                if (checkDuplicateAchievement != null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Achievement Name already exists.");
                }
                else
                {
                    AchievementDetails objAchievement = _mapper.Map<AchievementDetails>(addAchievement);
                    objAchievement.IsDeleted = false;
                    objAchievement.AchievementName = addAchievement.AchievementName;
                    objAchievement.CreatedDate = DateTime.Now;
                    objAchievement.LastModifiedDate = DateTime.Now;
                    objAchievement.CreatedBy = "602d2149e773f2a3990b47f5";
                    objAchievement.LastModifiedBy = "602d2149e773f2a3990b47f5";
                    objResponse.result = await _repository.CreateAchievementAsync(objAchievement);
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Exeception" + ex.InnerException?.Message);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> UpdateAchievementAsync(EditAchievementInputModel editAchievement)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                //validate duplicate name
                AchievementDetails objSelectedAchievement = await _repository.GetAchievementDetailByIdAsync(editAchievement.Id);
                if (objSelectedAchievement == null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Invalid Achievement Id.");
                }
                else
                {
                    AchievementDetails checkDuplicateAchievementName = await _repository.ValidateAchievementDetailsAsync(editAchievement.Id, editAchievement.AchievementName);
                    if (checkDuplicateAchievementName != null)
                    {
                        objResponse.isError = true;
                        objResponse.listErrors.Add("Another achievement already exists with this name.");
                    }
                    else
                    {
                        objSelectedAchievement.AchievementName = editAchievement.AchievementName;
                        objSelectedAchievement.LastModifiedDate = DateTime.Now;
                        objSelectedAchievement.LastModifiedBy = "602d2149e773f2a3990b47f5";
                        objResponse.result = await _repository.UpdateAchievementAsync(objSelectedAchievement);
                    }
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }
        public async Task<ServiceResponse> DeleteAchievementAsync(string id)
        {
            ServiceResponse objResponse = new ServiceResponse();

            try
            {
                objResponse.result = await _repository.DeleteAchievementAsync(id);
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Exeception" + ex.InnerException?.Message);
            }
            return objResponse;
        }
        #endregion

    }
}
