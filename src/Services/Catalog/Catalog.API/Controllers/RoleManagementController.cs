using CompanyAdmin.API.Common;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models;
using CompanyAdmin.API.Repositories.Interfaces;
using CompanyAdmin.API.Services.Interfaces;
using DnsClient.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RoleManagementController : ControllerBase
    {
        private readonly IRoleManagementService _roleManagementService;
        private readonly ILogger<RoleManagementController> _logger;

        public RoleManagementController(IRoleManagementService roleManagementService, ILogger<RoleManagementController> logger)
        {
            _roleManagementService = roleManagementService ?? throw new ArgumentNullException(nameof(_roleManagementService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

       
        //[HttpPost]
        //[ProducesResponseType(typeof(colRoleMaster), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<colRoleMaster>> SaveMasterRole([FromBody] colRoleMaster objRoleMaster)
        //{
        //    colRoleMaster objResult = await _roleManagementService.SaveMasterRole(objRoleMaster);
        //    return Ok(objResult);
        //}

        #region Roles
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Roles>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetRoleDetailsAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _roleManagementService.GetRoleDetailsAsync();
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
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateRoleDetailAsync([FromBody] RoleDetailsAddModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _roleManagementService.CreateRoleDetailAsync(inputModel);
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
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> EditRoleAsync([FromBody] RoleDetailsEditModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _roleManagementService.EditRoleAsync(inputModel);
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
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteRoleByIdAsync(string id)
        {
            return Ok(await _roleManagementService.DeleteRoleByIdAsync(id));
        }

        [HttpGet("{id:length(24)}", Name = "GetRoleById")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetRoleByIdAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _roleManagementService.GetRoleByIdAsync(id);
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
