using CompanyAdmin.API.Common;
using CompanyAdmin.API.Models.Sprint.Input;
using CompanyAdmin.API.Models.Sprint.Output;
using CompanyAdmin.API.Services;
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
    public class SprintController : ControllerBase
    {
        private readonly ISprintService _sprintService;
        private readonly ILogger<SprintController> _logger;
        private readonly IConfiguration _configuration;
        public SprintController(ISprintService sprintService, ILogger<SprintController> logger, IConfiguration configuration)
        {
            _sprintService = sprintService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration;
        }
        #region Sprint

        [Route("Sprint/Add")]
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddSprintAsync([FromBody] AddSprintInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _sprintService.AddSprintAsync(inputModel);
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

        [Route("Sprint/GetAll")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SprintOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAllSprintAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _sprintService.GetAllSprintAsync();
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

        [Route("Sprint/Delete")]
        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteSprintByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError("Error", "Id can't be null or empty.");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _sprintService.DeleteSprintByIdAsync(id);
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

        [Route("Sprint/Update")]
        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateSprintAsync([FromBody] EditSprintInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _sprintService.UpdateSprintAsync(inputModel);
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
        #endregion
    }
}
