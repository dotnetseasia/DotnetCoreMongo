using CompanyAdmin.API.Common;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models;
using CompanyAdmin.API.Models.UploadDocument.Input;
using CompanyAdmin.API.Models.Users;
using CompanyAdmin.API.Repositories.Interfaces;
using CompanyAdmin.API.Services.Interfaces;
using DnsClient.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class UserManagementController : ControllerBase
    {
        private readonly IUserDetailsService _userDetailService;
        private readonly ILogger<UserManagementController> _logger;
        private readonly IConfiguration _configuration;

        public UserManagementController(IUserDetailsService userDetailService, ILogger<UserManagementController> logger
                                        ,IConfiguration configuration)
        {
            _userDetailService = userDetailService ?? throw new ArgumentNullException(nameof(_userDetailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration;
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<UserDetailsViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserDetailsViewModel>>> Login(LoginInputModel loginInputModel)
        {
            var userDetails = await _userDetailService.Login(loginInputModel);
            return Ok(userDetails);
        }

        //[HttpPost]
        //[Route("[action]/{changePassword}", Name = "ChangePassword")]
        //[ProducesResponseType(typeof(IEnumerable<UserDetailsViewModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<UserDetailsViewModel>>> ChangePassword(ChangePasswordInputModel ChangePasswordInputModel)
        //{
        //    var userDetails =  _userDetailService.ChangePassword(ChangePasswordInputModel);
        //    return Ok(userDetails);
        //}

        [HttpPost]
        [Route("[action]", Name = "ChangePassword")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public ActionResult ChangePassword([FromBody] ChangePasswordInputModel ChangePasswordInputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = _userDetailService.ChangePassword(ChangePasswordInputModel);
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
        [ProducesResponseType(typeof(IEnumerable<ColUser>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetAllUsers(GetAllUsersInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult =  await _userDetailService.GetAllUsers(inputModel);
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
        [ProducesResponseType(typeof(IEnumerable<ColUser>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetUserDetailsAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _userDetailService.GetUserDetailsAsync();
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
         
        [HttpGet("{id:length(24)}", Name = "GetUserDetail")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ColUser), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserDetailsViewModel>> GetUserDetailById(string id)
        {
            //GenerateToken generateToken = new GenerateToken();
            //userDetail.Token = generateToken.GenerateJwtToken(userDetail, _configuration);
            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _userDetailService.GetUserDetailAsync(id);
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

        [Route("[action]/{name}", Name = "GetUserDetailByName")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<ColUser>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ColUser>>> GetUserDetailByName(string name)
        {
            var items = await _userDetailService.GetUserDetailByName(name);
            if (items == null)
            {
                _logger.LogError($"UserDetails with name: {name} not found.");
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateUserDetailAsync([FromBody] ColUserAddModel userDetail)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _userDetailService.CreateUserDetailAsync(userDetail);
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
        [ProducesResponseType(typeof(ColUserAddModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateUserDetailAsync([FromBody] ColUserEditModel userDetail)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _userDetailService.UpdateUserDetailAsync(userDetail);
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

        [HttpDelete("{id:length(24)}", Name = "DeleteUserDetail")]
        [ProducesResponseType(typeof(ColUser), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteUserDetailById(string id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _userDetailService.DeleteUserDetail(id);
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
        public async Task<ActionResult> UploadUserDocument([FromForm] UploadDocumentInputModel inputModel)
        {
            var path = _configuration.GetSection("DocumentConfiguration:targetPath").Value;
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = _userDetailService.UploadUserDocument(inputModel,path);
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
        #region Send and Velidate OTP
        [HttpPost]
        //[Route("SendOTPForForgotPassword")]
        public async Task<ActionResult> SendOTPForForgotPassword([FromBody] SendOTPInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult =await _userDetailService.SendOTPForForgotPassword(inputModel);
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
                return Ok("Otp Sent Successfully.");
            }
        }
        [HttpPost]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> VerifyOTPForForgotPassword([FromBody] VerifyOTPInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _userDetailService.VerifyOTPForForgotPassword(inputModel);
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
        public async Task<ActionResult> ResendOTPForForgotPassword([FromBody] SendOTPInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult =await  _userDetailService.ResendOTPForForgotPassword(inputModel);
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
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Invalid Input Request");
                return BadRequest(ModelState);
            }
            ServiceResponse objResult = await _userDetailService.ResetPassword(inputModel);
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
