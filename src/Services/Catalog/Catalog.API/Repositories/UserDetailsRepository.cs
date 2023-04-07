using CompanyAdmin.API.Data.Interfaces;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models;
using CompanyAdmin.API.Models.Users;
using CompanyAdmin.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace CompanyAdmin.API.Repositories
{
    public class UserDetailsRepository : IUserDetailsRepository
    {
        private readonly ICompanyAdminContext _context;

        public UserDetailsRepository(ICompanyAdminContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ColUser> Login(LoginInputModel loginInputModel)
        {
            return await _context
                           .Users
                           .Find(p => p.UserName == loginInputModel.UserName && p.Password == loginInputModel.Password).FirstOrDefaultAsync();
        }

        public ColUser ChangePassword(ChangePasswordInputModel ChangePasswordInputModel)
        {
            return _context
                               .Users
                               .Find(p => p.Id == ChangePasswordInputModel.Id && p.Password == ChangePasswordInputModel.OldPassword).FirstOrDefault();
        }
        public string NewUpdatedPassword(ColUser colUser)
        {
            _context.Users.ReplaceOneAsync(filter: g => g.Id == colUser.Id, replacement: colUser);
            return colUser.Id;
        }

        public async Task<IEnumerable<ColUser>> GetUserDetailsAsync()
        {
            return await _context
                            .Users
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<ColUser> GetUserDetailAsync(string id)
        {
            return await _context
                           .Users
                           .Find(p => p.Id == id).FirstOrDefaultAsync();


        }

        public async Task<IEnumerable<ColUser>> GetUserDetailByName(string name)
        {
            FilterDefinition<ColUser> filter = Builders<ColUser>.Filter.ElemMatch(p => p.UserName, name);

            return await _context
                            .Users
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<string> CreateUserDetailAsync(ColUser userDetail)
        {
            await _context.Users.InsertOneAsync(userDetail);
            return userDetail.Id;
        }

        public async Task<bool> UpdateUserDetailAsync(ColUser userDetail)
        {
            var updateResult = await _context
                                        .Users
                                        .ReplaceOneAsync(filter: g => g.Id == userDetail.Id, replacement: userDetail);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteUserDetail(ColUser userDetail)
        {
            await _context.Users.ReplaceOneAsync(filter: g => g.Id == userDetail.Id, replacement: userDetail);
            return userDetail.IsDeleted == null ? false : Convert.ToBoolean(userDetail.IsDeleted);

        }

        public async Task<IEnumerable<ColUser>> GetAllUsers(GetAllUsersInputModel inputModel)
        {
            return await _context
                            .Users
                            .Find(p => p.IsDeleted == false && 
                            (p.EmployeeInfo.DepartmentId == inputModel.Department || string.IsNullOrEmpty(inputModel.Department)) &&
                            (string.IsNullOrEmpty(inputModel.Search)
                                  || p.EmployeeInfo.FirstName.ToLower().Contains(inputModel.Search)
                                  || p.EmployeeInfo.LastName.ToLower().Contains(inputModel.Search)
                                  || p.EmployeeInfo.EmployeeId.Contains(inputModel.Search)
                                 // || p.PersonalInfo.PhoneNumber.Contains(inputModel.Search)
                            )
                            )
                            .Skip(inputModel.Skip)
                            .Limit(inputModel.Take)
                            .ToListAsync();
          
        }
        /// <summary>
        /// GetUserDetailByEmployeeId
        /// </summary>
        /// <param name="employeeid"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ColUser GetUserDetailByEmployeeId(string employeeId, string userId = null)
        {
            return _context
                            .Users
                            .Find(a => a.EmployeeInfo.EmployeeId == employeeId && (userId == null || a.Id != userId))
                            .FirstOrDefault();
        }

        /// <summary>
        /// GetUserDetailByEmailId
        /// </summary>
        /// <param name="emailId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ColUser GetUserDetailByEmailId(string emailId, string userId = null)
        {
            return _context
                            .Users
                            .Find(a => a.EmployeeInfo.EmailId == emailId && (userId == null || a.Id != userId))
                            .FirstOrDefault();
        }

        /// <summary>
        /// GetUserDetailByUsername
        /// </summary>
        /// <param name="emailId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ColUser GetUserDetailByUsername(string username, string userId = null)
        {
            return _context
                            .Users
                            .Find(a => a.UserName == username && (userId == null || a.Id != userId))
                            .FirstOrDefault();
        }


        public async Task<ColUser> SendOTPForForgotPassword(SendOTPInputModel inputModel)
        {
            return await _context
                           .Users
                           .Find(p => p.UserName == inputModel.Username && p.EmployeeInfo.EmailId == inputModel.EmailId)
                           .FirstOrDefaultAsync();
        }
        public async Task<ColUser> VerifyOTPForForgotPassword(VerifyOTPInputModel inputModel)
        {
            return await _context
                           .Users
                           .Find(p => p.OTPDetail.OTP == inputModel.OTP && p.EmployeeInfo.EmailId==inputModel.EmailID)
                           .FirstOrDefaultAsync();
        }
        public async Task<ColUser> ResetPassword(ResetPasswordInputModel inputModel)
        {
            return await _context
                           .Users
                           .Find(p => p.UserName == inputModel.Username && p.OTPDetail.OTP == inputModel.OTP)
                           .FirstOrDefaultAsync();
        }
        public async Task  SaveOTPDetail(ColUser userDetail)
        {
            var updateResult = await UpdateOTPDetail(userDetail);

        }
        public async Task<string> UpdateOTPDetail(ColUser userDetail)
        {
           await _context.Users.ReplaceOneAsync(filter: g => g.UserName == userDetail.UserName, replacement: userDetail);
            return userDetail.Id;
        }
        public async Task<string> UpdatePasswordDetail(ColUser userDetail)
        {
           await _context.Users.ReplaceOneAsync(filter: g => g.UserName == userDetail.UserName, replacement: userDetail);
            return userDetail.Id;
        }
    }
}
