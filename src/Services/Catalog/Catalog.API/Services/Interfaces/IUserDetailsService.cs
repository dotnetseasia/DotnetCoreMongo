using CompanyAdmin.API.Common;
using CompanyAdmin.API.Common.DI;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models;
using CompanyAdmin.API.Models.UploadDocument.Input;
using Microsoft.AspNetCore.Http;
using CompanyAdmin.API.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services.Interfaces
{
    public interface IUserDetailsService : IAsSelf
    {
        Task<ServiceResponse> GetUserDetailsAsync();
        Task<ServiceResponse> GetAllUsers(GetAllUsersInputModel inputModel);
        Task<ServiceResponse> GetUserDetailAsync(string id);
        Task<IEnumerable<ColUser>> GetUserDetailByName(string name);
        Task<ServiceResponse> CreateUserDetailAsync(ColUserAddModel userDetail);
        Task<ServiceResponse> UpdateUserDetailAsync(ColUserEditModel userDetail);
        Task<ServiceResponse> DeleteUserDetail(string id);
        Task<ServiceResponse> Login(LoginInputModel loginInputModel);
        Task<ServiceResponse> SendOTPForForgotPassword(SendOTPInputModel inputModel);
        Task<ServiceResponse> VerifyOTPForForgotPassword(VerifyOTPInputModel inputModel);
        Task<ServiceResponse> ResendOTPForForgotPassword(SendOTPInputModel inputModel);
        Task<ServiceResponse> ResetPassword(ResetPasswordInputModel inputModel);
        ServiceResponse UploadUserDocument(UploadDocumentInputModel inputModel, string path);
        ServiceResponse ChangePassword(ChangePasswordInputModel ChangePasswordInputModel);
    }
}
