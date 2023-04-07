using AutoMapper;
using CompanyAdmin.API.Common;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models.Project;
using CompanyAdmin.API.Repositories.Interfaces;
using CompanyAdmin.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services
{
    public class ProjectDetailsService : IProjectDetailsService
    {
        private readonly IProjectDetailsRepository _repository;

        private readonly ILogger<ProjectDetailsService> _logger;

        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration;

        public ProjectDetailsService(IProjectDetailsRepository repository, ILogger<ProjectDetailsService> logger, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
            _configuration = configuration;

        }

        public async Task<ServiceResponse> CreateProjectDetailAsync(ColProjectAddModel colProject)
        {


            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                //validate duplicate name
                // ToDo Validation code
                ColProject project = _mapper.Map<ColProject>(colProject);
                objResponse.result = await _repository.CreateProjectDetailAsync(project);

            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }
    }
}
