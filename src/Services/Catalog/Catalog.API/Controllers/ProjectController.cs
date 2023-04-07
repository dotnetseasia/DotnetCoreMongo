using CompanyAdmin.API.Common;
using CompanyAdmin.API.Models.Project;
using CompanyAdmin.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProjectController : ControllerBase
    {

        private readonly IProjectDetailsService _projectDetailService;
        private readonly ILogger<ProjectController> _logger;
        private readonly IConfiguration _configuration;

        public ProjectController(IProjectDetailsService projectDetailService, ILogger<ProjectController> logger
                                       , IConfiguration configuration)
        {
            _projectDetailService = projectDetailService ?? throw new ArgumentNullException(nameof(_projectDetailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateProjectDetailAsync([FromBody] ColProjectAddModel colProject)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _projectDetailService.CreateProjectDetailAsync(colProject);
            if (objResult.isError == true)
            {
                foreach (var error in objResult.listErrors)
                {
                    ModelState.AddModelError("Error", error);
                }
                return BadRequest(ModelState);
            }
            else
            {
                return Ok(objResult.result);
            }
        }
    }
}
