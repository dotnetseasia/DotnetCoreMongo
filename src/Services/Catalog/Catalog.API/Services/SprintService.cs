using AutoMapper;
using CompanyAdmin.API.Common;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models.Sprint.Input;
using CompanyAdmin.API.Models.Sprint.Output;
using CompanyAdmin.API.Repositories.Interfaces;
using CompanyAdmin.API.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services
{
    public class SprintService : ISprintService
    {
        private readonly ISprintRepository _repository;
        private readonly ILogger<SprintService> _logger;
        private readonly IMapper _mapper;

        public SprintService(ISprintRepository repository, ILogger<SprintService> logger, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        #region Sprint
        public async Task<ServiceResponse> AddSprintAsync(AddSprintInputModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                //validate duplicate name
                colSprint checkDuplicateSprint = await _repository.GetSprintBySprintName(inputModel.SprintName);
                if (checkDuplicateSprint != null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Sprint already exists.");
                }
                else
                {
                    colSprint objSprint = _mapper.Map<colSprint>(inputModel);
                    objSprint.IsDeleted = false;
                    objSprint.CreatedDate = DateTime.Now;
                    objSprint.LastModifiedDate = DateTime.Now;
                    objSprint.CreatedBy = "602d2149e773f2a3990b47f5";
                    objSprint.LastModifiedBy = "602d2149e773f2a3990b47f5";
                    objResponse.result = await _repository.AddSprintAsync(objSprint);
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> GetAllSprintAsync()
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                List<SprintOutputModel> outputModels = new List<SprintOutputModel>();
                var list = await _repository.GetAllSprintAsync();
                if (list != null)
                {
                    outputModels = _mapper.Map<List<SprintOutputModel>>(list);
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
        public async Task<ServiceResponse> GetSprintByIdAsync(string id)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {

                var list = await _repository.GetSprintByIdAsync(id);
                if (list != null)
                {
                    SprintOutputModel obj = _mapper.Map<SprintOutputModel>(list);
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

        public async Task<ServiceResponse> DeleteSprintByIdAsync(string id)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                objResponse.result = await _repository.DeleteSprintByIdAsync(id);
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> UpdateSprintAsync(EditSprintInputModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();

            try
            {
                //validate duplicate name
                colSprint objSelectedSprint = await _repository.GetSprintByIdAsync(inputModel.Id);
                if (objSelectedSprint == null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Invalid Sprint Id.");
                }
                else
                {
                    colSprint objCheckDuplicateSprintName = await _repository.GetSprintBySprintName(inputModel.SprintName);
                    if (objCheckDuplicateSprintName != null)
                    {
                        objResponse.isError = true;
                        objResponse.listErrors.Add("Invalid Sprint Name.");
                    }

                    else
                    {
                        objSelectedSprint.SprintName = inputModel.SprintName;
                        objSelectedSprint.LastModifiedDate = DateTime.Now;
                        objSelectedSprint.LastModifiedBy = "602d2149e773f2a3990b47f5";
                        objResponse.result = await _repository.UpdateSprintAsync(objSelectedSprint);
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
    }
}
