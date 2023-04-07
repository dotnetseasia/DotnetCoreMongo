using CompanyAdmin.API.Common;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models;
using CompanyAdmin.API.Models.Channel.Input;
using CompanyAdmin.API.Models.Channel.Output;
using CompanyAdmin.API.Models.Department.Input;
using CompanyAdmin.API.Models.Department.Output;
using CompanyAdmin.API.Models.Designation.Input;
using CompanyAdmin.API.Models.Designation.Output;
using CompanyAdmin.API.Models.TechnicalSkills.Input;
using CompanyAdmin.API.Models.TechnicalSkills.Output;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models;
using CompanyAdmin.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
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
    [Route("api/v1/[controller]")]

    public class MasterController : ControllerBase
    {
        private readonly IMasterService _masterService;
        private readonly ILogger<MasterController> _logger;
        private readonly IConfiguration _configuration;

        public MasterController(IMasterService masterService, ILogger<MasterController> logger, IConfiguration configuration)
        {
            _masterService = masterService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration;
        }

        [HttpGet]
        [Route("Department/Search")]
        [ProducesResponseType(typeof(IEnumerable<DepartmentOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAllDepartments()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.GetAllDepartments();
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

        [HttpGet]
        [Route("Department/SearchbyId")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DepartmentOutputModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetDepartmentDetails(string id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.GetDepartmentDetails(id);
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

        [HttpDelete]
        [Route("Department/Delete")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteDepartment(string id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.DeleteDepartment(id);
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

        [HttpPost]
        [Route("Department/Add")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateDepartment([FromBody] AddDepartmentInputModel inputModel)
        {
            if (!ModelState.IsValid)

            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.CreateDepartment(inputModel);
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

        [HttpPut]
        [Route("Department/Edit")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateDepartment([FromBody] EditDepartmentInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.UpdateDepartment(inputModel);
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
        #region Achievements
        [HttpGet]
        [Route("Achievement/GetAll")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAllAchievementsAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.GetAllAchievementsAsync();
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

        [HttpGet]
        [Route("Achievement/GetbyId")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DepartmentOutputModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAchievementDetailByIdAsync(string id)          
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult =await _masterService.GetAchievementDetailByIdAsync(id);
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
     
        [Route("Achievement/Add")]
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateAchievementAsync([FromBody] AddAchievementInputModel addAchievement)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult =await _masterService.CreateAchievementAsync(addAchievement);
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

        [HttpPut]
        [Route("Achievement/Update")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task <ActionResult> UpdateAchievementAsync([FromBody] EditAchievementInputModel editAchievement)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult =await _masterService.UpdateAchievementAsync(editAchievement);
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
   
        [Route("Achievement/DeleteById")]
        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]     
        public async Task<ActionResult> DeleteAchievementAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult =await _masterService.DeleteAchievementAsync(id);
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

    #region Technical Skills

    [Route("TechnicalSkills/Add")]
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddTechnicalSkillsAsync([FromBody] AddTechnicalSkillsInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.AddTechnicalSkillsAsync(inputModel);
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

        [Route("TechnicalSkills/GetAll")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TechnicalSkillsOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAllTechnicalSkillsAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.GetAllTechnicalSkillsAsync();
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

        [Route("TechnicalSkills/GetById")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DepartmentOutputModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetTechnicalSkillsByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError("Error", "Id can't be null or empty.");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.GetTechnicalSkillsByIdAsync(id);
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

        [Route("TechnicalSkills/DeleteById")]
        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteTechnicalSkillsByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError("Error", "Id can't be null or empty.");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.DeleteTechnicalSkillsByIdAsync(id); 
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

        [Route("TechnicalSkills/Update")]
        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateTechnicalSkillsAsync([FromBody] EditTechnicalSkillsInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.UpdateTechnicalSkillsAsync(inputModel);
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

        #region  Designation
        [Route("Designation/Add")]
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> AddDesignationAsync([FromBody] AddDesignationInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.AddDesignationAsync(inputModel);
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

        [Route("Designation/GetAll")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DesignationOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAllDesignationAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.GetAllDesignationAsync();
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

        [Route("Designation/GetById")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DesignationOutputModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetDesignationByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError("Error", "Id can't be null or empty.");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.GetDesignationByIdAsync(id);
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

        [Route("Designation/DeleteById")]
        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteDesignationByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError("Error", "Id can't be null or empty.");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.DeleteDesignationByIdAsync(id);
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
        [Route("Designation/Update")]
        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateDesignationAsync([FromBody] EditDesignationInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.UpdateDesignationAsync(inputModel);
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


        #region Channel 

        [Route("[action]", Name = "GetAllChannel")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ChannelOutputModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAllChannelAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.GetAllChannelAsync();
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

        [Route("[action]", Name = "CreateChannel")]
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateChannel([FromBody] AddChannelInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.CreateChannelAsync(inputModel);
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

        [Route("[action]", Name = "DeleteChannel")]
        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteChannelAsync(string id)
        {
            return Ok(await _masterService.DeleteChannelAsync(id));
        }

        [Route("[action]", Name = "UpdateChannel")]
        [HttpPut]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateChannelAsync([FromBody] EditChannelInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.UpdateChannelAsync(inputModel);
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

        [Route("[action]", Name = "GetChannelDetailsById")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DepartmentOutputModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetChannelDetails(string id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.GetChannelDetailsAsync(id);
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



        #region Badge
        [HttpGet]
        [Route("[action]/{id}", Name = "GetBadgeById")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetBadgeByIdAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.GetBadgeByIdAsync(id);
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

        [HttpGet]
        [Route("[action]", Name = "GetAllBadges")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAllBadgesAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.GetAllBadgesAsync();
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

        [HttpPost]
        [Route("[action]", Name = "CreateBadge")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateBadgeAsync([FromBody] AddBadgesInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.CreateBadgeAsync(inputModel);
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

        [HttpPut]
        [Route("[action]", Name = "UpdateBadge")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateBadgeAsync([FromBody] EditBadgesInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _masterService.UpdateBadgeAsync(inputModel);
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

        [HttpDelete]
        [Route("[action]", Name = "DeleteBadge")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteBadgeAsync(string id)
        {
            ServiceResponse objResult = await _masterService.DeleteBadgeAsync(id);
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
        #endregion Badge
    }
}

