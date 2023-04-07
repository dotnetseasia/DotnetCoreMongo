using AutoMapper;
using CompanyAdmin.API.Common;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models;
using CompanyAdmin.API.Repositories.Interfaces;
using CompanyAdmin.API.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services
{
    public class RoleManagementService : IRoleManagementService
    {
        private readonly IRoleManagementRepository _repository;

        private readonly ILogger<RoleManagementService> _logger;

        private readonly IMapper _mapper;

        public RoleManagementService(IRoleManagementRepository repository, ILogger<RoleManagementService> logger, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public Task<colRoleMaster> SaveMasterRole(colRoleMaster objRoleMaster)
        {
            return _repository.SaveMasterRole(objRoleMaster);
        }

       public async Task<ServiceResponse> GetRoleDetailsAsync()
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                List<GetRoleDetailsModel> outputModels = new List<GetRoleDetailsModel>();
                var list = await _repository.GetRoleDetailsAsync();
                if (list != null)
                {
                    outputModels = _mapper.Map<List<GetRoleDetailsModel>>(list);
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

        public async Task<ServiceResponse> CreateRoleDetailAsync(RoleDetailsAddModel inputModel)
        {
            {
                ServiceResponse objResponse = new ServiceResponse();
                try
                {
                    //validate duplicate name
                    Roles checkDuplicateRole = await _repository.GetRoleByName(inputModel.Name);
                    if (checkDuplicateRole != null)
                    {
                        objResponse.isError = true;
                        objResponse.listErrors.Add("Department Name already exists.");
                    }
                    else
                    {
                        Roles objRole =  _mapper.Map<Roles>(inputModel);
                        objRole.IsDeleted = false;
                        objRole.CreatedDate = DateTime.Now;
                        objRole.LastModifiedDate = DateTime.Now;
                        objRole.CreatedBy = "602d2149e773f2a3990b47f5";
                        objRole.LastModifiedBy = "602d2149e773f2a3990b47f5";
                        objResponse.result = await _repository.CreateRoleDetailAsync(objRole);
                    }
                }
                catch (Exception ex)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
                }
                return objResponse;
            }
        }
        public async Task<ServiceResponse> EditRoleAsync(RoleDetailsEditModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                //validate duplicate name
                Roles objSelectedRole = await _repository.GetRoleByIdAsync(inputModel.Id);
                if (objSelectedRole == null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Invalid Role Id.");
                }
                else
                {
                    Roles checkDuplicateRoleName = await _repository.ValidateDuplicateDepartmentName(inputModel.Id, inputModel.Name);
                    if (checkDuplicateRoleName != null)
                    {
                        objResponse.isError = true;
                        objResponse.listErrors.Add("Another Role already exists with this Rolename.");
                    }
                    else
                    {
                        objSelectedRole.Name = inputModel.Name;
                        objSelectedRole.LastModifiedDate = DateTime.Now;
                        objSelectedRole.LastModifiedBy = "602d2149e773f2a3990b47f5";
                        objResponse.result =await _repository.EditRoleAsync(objSelectedRole);
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

        public  Task<bool> DeleteRoleByIdAsync(string id)
        {
            return _repository.DeleteRoleByIdAsync(id);
        }
       public async Task<ServiceResponse> GetRoleByIdAsync(string id)
        {
            ServiceResponse objResponse = new ServiceResponse();
            
            try
            {
                var result = await _repository.GetRoleByIdAsync(id);
                if(result != null)
                {
                    objResponse.result =   _mapper.Map<GetRoleDetailsModel>(result);
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Exeception" + ex.InnerException?.Message);
            }
            return objResponse;
        }
    }
}
