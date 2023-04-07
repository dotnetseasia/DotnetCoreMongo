using CompanyAdmin.API.Common;
using CompanyAdmin.API.Common.DI;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models;
using CompanyAdmin.API.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Repositories.Interfaces
{
    public interface IUserDetailsRepository : IAsSelf
    {
        Task<IEnumerable<ColUser>> GetUserDetailsAsync();
        Task<ColUser> GetUserDetailAsync(string id);
        Task<IEnumerable<ColUser>> GetUserDetailByName(string name);

        ColUser GetUserDetailByEmployeeId(string employeeId, string userId = null);

        Task<string> CreateUserDetailAsync(ColUser userDetail);
        Task<bool> UpdateUserDetailAsync(ColUser userDetail);
        Task<bool> DeleteUserDetail(ColUser userDetail);
        Task<ColUser> Login(LoginInputModel loginInputModel);
        Task<IEnumerable<ColUser>> GetAllUsers(GetAllUsersInputModel inputModel);

        /// <summary>
        /// GetUserDetailByEmailId
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        ColUser GetUserDetailByEmailId(string emailId, string userId = null);

        /// <summary>
        /// GetUserDetailByUsername
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        ColUser GetUserDetailByUsername(string username, string userId = null);
        ColUser ChangePassword(ChangePasswordInputModel ChangePasswordInputModel);
        string NewUpdatedPassword(ColUser colUser);
       Task<ColUser> SendOTPForForgotPassword(SendOTPInputModel inputModel);
        Task<ColUser> VerifyOTPForForgotPassword(VerifyOTPInputModel inputModel);
       
        Task<ColUser> ResetPassword(ResetPasswordInputModel inputModel);
        public Task SaveOTPDetail(ColUser userDetail);
        public Task<string> UpdatePasswordDetail(ColUser userDetail);
    }
}
