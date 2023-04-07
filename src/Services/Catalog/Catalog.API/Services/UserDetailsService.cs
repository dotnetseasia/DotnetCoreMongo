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
using CompanyAdmin.API.CommonExtension;
using Microsoft.Extensions.Configuration;
using CompanyAdmin.API.Common;
using Microsoft.AspNetCore.Http;
using CompanyAdmin.API.Services.CommonMethods;
using Serilog.Core;
using CompanyAdmin.API.Models.UploadDocument.Input;
using CompanyAdmin.API.Models.Users;

namespace CompanyAdmin.API.Services
{
    public class UserDetailsService : IUserDetailsService
    {
        private readonly IUserDetailsRepository _repository;

        private readonly ILogger<UserDetailsService> _logger;

        private readonly IMapper _mapper;

        private readonly IConfiguration _configuration;

        public UserDetailsService(IUserDetailsRepository repository, ILogger<UserDetailsService> logger, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
            _configuration = configuration;
           
        }

        public async Task<ServiceResponse> Login(LoginInputModel loginInputModel)
        {
            loginInputModel.Password = CommonExtension.CommonExtension.Encrypt(loginInputModel.Password);
            var result = await _repository.Login(loginInputModel);
            ServiceResponse serviceResponse = new ServiceResponse();
            if (result != null)
            {
                UserDetailsViewModel userDetailsViewModel = _mapper.Map<UserDetailsViewModel>(result);
                GenerateToken generateToken = new GenerateToken();
                userDetailsViewModel.Token = generateToken.GenerateJwtToken(userDetailsViewModel, _configuration);
                serviceResponse.result = userDetailsViewModel;
            }
            else
            {
                serviceResponse.isError = true;
                serviceResponse.listErrors.Add("Department Name already exists.");
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse> CreateUserDetailAsync(ColUserAddModel userDetail)
        {


            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                //validate duplicate name
                ColUser colUser = _repository.GetUserDetailByEmployeeId(userDetail.EmployeeInfo.EmployeeId);
                if (colUser != null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Employee Id already exist for another user.");
                }
                else if (_repository.GetUserDetailByEmailId(userDetail.EmployeeInfo.EmailId) != null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("EmailId already exist for another user.");
                }
                else if (_repository.GetUserDetailByUsername(userDetail.UserName) != null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Username already exist for another user.");
                }
                else
                {
                    userDetail.Password = CommonExtension.CommonExtension.Encrypt(Utilities.PasswordGenerator());
                    ColUser userDetails = _mapper.Map<ColUser>(userDetail);
                    objResponse.result = await _repository.CreateUserDetailAsync(userDetails);
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }

        public async Task<ServiceResponse> DeleteUserDetail(string id)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                ColUser objSelectedUser = await _repository.GetUserDetailAsync(id);
                if (objSelectedUser == null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Invalid User Id.");
                }
                else
                {
                    if (objSelectedUser.IsDeleted == true)
                    {
                        objResponse.isError = true;
                        objResponse.listErrors.Add("User already deleted.");
                    }
                    else
                    {
                        objSelectedUser.IsDeleted = true;
                        objSelectedUser.LastModifiedBy = "602d2149e773f2a3990b47f5";
                        objSelectedUser.LastModifiedDate = DateTime.UtcNow;
                        objResponse.result = await _repository.DeleteUserDetail(objSelectedUser);
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

        public async Task<ServiceResponse> GetUserDetailAsync(string id)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                var result = await _repository.GetUserDetailAsync(id);
                UserDetailsViewModel userDetailsViewModel = _mapper.Map<UserDetailsViewModel>(result);
                objResponse.result =  userDetailsViewModel;
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }

        public Task<IEnumerable<ColUser>> GetUserDetailByName(string name)
        {
            return _repository.GetUserDetailByName(name);
        }

        public async Task<ServiceResponse> GetUserDetailsAsync()
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                List<UserDetailsViewModel> outputModels = new List<UserDetailsViewModel>();
                var list = await _repository.GetUserDetailsAsync();
                if (list != null)
                {
                    outputModels = _mapper.Map<List<UserDetailsViewModel>>(list);
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

        public async Task<ServiceResponse> UpdateUserDetailAsync(ColUserEditModel userDetail)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                //validate duplicate with Employee Id
                ColUser colUser = _repository.GetUserDetailByEmployeeId(userDetail.EmployeeInfo.EmployeeId, userDetail.Id);
                if (colUser != null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Employee Id already exist for another user.");
                }
                else if (_repository.GetUserDetailByEmailId(userDetail.EmployeeInfo.EmailId, userDetail.Id) != null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("EmailId already exist for another user.");
                }
                else if (_repository.GetUserDetailByUsername(userDetail.UserName, userDetail.Id) != null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Username already exist for another user.");
                }
                else
                {
                    ColUser userDetails = _mapper.Map<ColUser>(userDetail);
                    objResponse.result = await _repository.UpdateUserDetailAsync(userDetails);
                }
            }
            catch (Exception ex)
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(ex.Message + " Inner Execption" + ex.InnerException?.Message);
            }
            return objResponse;
        }
        public async Task<ServiceResponse> GetAllUsers(GetAllUsersInputModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                List<UserDetailsViewModel> outputModels = new List<UserDetailsViewModel>();
                var list = await _repository.GetAllUsers(inputModel);
                if (list != null)
                {
                    outputModels = _mapper.Map<List<UserDetailsViewModel>>(list);
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
       public async Task<ServiceResponse> SendOTPForForgotPassword(SendOTPInputModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                
                ColUser col = await _repository.SendOTPForForgotPassword(inputModel);
                if (col == null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Invalid Username or Email Id.");
                }
                else
                {
                    col.OTPDetail = new OTPDetail();
                    col.OTPDetail.OTP = Utilities.OTPGenerator(10);
                    col.OTPDetail.OTPCreatedTime = DateTime.Now.ToString();
                    await _repository.SaveOTPDetail(col);
                    objResponse.result = col;
                }
            }
            catch(Exception ex)
            {

            }
            return objResponse;
        }
        public async Task<ServiceResponse> VerifyOTPForForgotPassword(VerifyOTPInputModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                ColUser col = await _repository.VerifyOTPForForgotPassword(inputModel);
                if (col == null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Invalid OTP");
                }
                else
                {
                    DateTime OtpCrtDate = Convert.ToDateTime(col.OTPDetail.OTPCreatedTime.ToString());
                    TimeSpan timeSub = DateTime.Now - OtpCrtDate;
                    if (timeSub.TotalMinutes < 3)
                    {
                        objResponse.isError = false;
                        objResponse.result = ("please Enter your New Password");
                    }
                    else
                    {
                        objResponse.isError = true;
                        objResponse.listErrors.Add("OTP Expired,Please Request for new OTP");
                    }
                }
            }
            catch(Exception ex)
            {
             _logger.LogError(ex.Message);
            }
            return objResponse;
        }
        public async Task<ServiceResponse> ResendOTPForForgotPassword(SendOTPInputModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                ColUser col = await _repository.SendOTPForForgotPassword(inputModel);
                if (col == null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Invalid OTP");
                }
                else
                {
                    col.OTPDetail = new OTPDetail();
                    col.OTPDetail.OTP = Utilities.OTPGenerator(10);
                    col.OTPDetail.OTPCreatedTime = DateTime.Now.ToString();


                    await _repository.SaveOTPDetail(col);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return objResponse;

        }
       public async Task<ServiceResponse> ResetPassword(ResetPasswordInputModel inputModel)
        {
            ServiceResponse objResponse = new ServiceResponse();
            try
            {
                ColUser col = await _repository.ResetPassword(inputModel);
                if (col == null)
                {
                    objResponse.isError = true;
                    objResponse.listErrors.Add("Invalid OTP or Username.");
                }
                else
                {
                    inputModel.Password = CommonExtension.CommonExtension.Encrypt(inputModel.Password);
                    col.Password = inputModel.Password;
                    await _repository.SaveOTPDetail(col);
                }
            }
            catch(Exception ex)
            {
              _logger.LogError(ex.Message);
            }
            return objResponse;
        }

        public ServiceResponse UploadUserDocument(UploadDocumentInputModel inputModel, string path)
        {
            ServiceResponse objResponse = new ServiceResponse();
            string response = Utilities.UploadDocument(inputModel, path);
            if(response == "success")
            {
                objResponse.result = response;
            }
            else
            {
                objResponse.isError = true;
                objResponse.listErrors.Add(response);
            }
            return (objResponse);
        }

        public ServiceResponse ChangePassword(ChangePasswordInputModel ChangePasswordInputModel)
        {
             ChangePasswordInputModel.OldPassword = CommonExtension.CommonExtension.Encrypt(ChangePasswordInputModel.OldPassword);
             var result = _repository.ChangePassword(ChangePasswordInputModel);
             ServiceResponse serviceResponse = new ServiceResponse();
            try
            {
                if (result != null)
                {
                    if (result.Password == ChangePasswordInputModel.NewPassword)
                    {
                        serviceResponse.isError = true;
                        serviceResponse.listErrors.Add("Password matched with previous one , use different");
                    }
                    result.Password = ChangePasswordInputModel.NewPassword;
                    serviceResponse.result = _repository.NewUpdatedPassword(result);
                    ChangePasswordInputModel.NewPassword = CommonExtension.CommonExtension.Encrypt(ChangePasswordInputModel.NewPassword);
                }
                else
                {
                    serviceResponse.isError = true;
                    serviceResponse.listErrors.Add("Incorrect password");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return serviceResponse;
        }
    }
    
}
