using Autofac;
using Autofac.Core;
using CompanyAdmin.API.Data.Interfaces;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models;
using CompanyAdmin.API.Models.Users;
using CompanyAdmin.API.Services;
using CompanyAdmin.API.Services.CommonMethods;
using CompanyAdmin.API.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyAdmin.Api.Test.Data
{
    public class UserDetailsServiceTests : DatabaseTest
    {

        [Test]
        public async Task CreateUserDetailTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            string testEmail = "testEmail@yopmali.com";
            string employeeId = "1234";
            var created = await UserDetailsService.CreateUserDetailAsync(new ColUserAddModel { UserName = name , EmployeeInfo = new EmployeeDetail() {EmployeeId=employeeId , EmailId=testEmail } });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            string id = (string)created.result;

            //check name save in db 
            var userDetailById = await UserDetailsService.GetUserDetailAsync(id);
            Assert.That(userDetailById, Is.Not.Null);
            Assert.That(userDetailById.result, Is.Not.Null);
            UserDetailsViewModel userDetailsViewModels = (UserDetailsViewModel)userDetailById.result;
            Assert.That(userDetailsViewModels.UserName, Is.EqualTo(name));
            Assert.That(userDetailsViewModels.EmployeeInfo, Is.Not.Null);
            Assert.That(userDetailsViewModels.EmployeeInfo.EmailId, Is.EqualTo(testEmail));

            name = Guid.NewGuid().ToString();
            var createdWithSameEmail = await UserDetailsService.CreateUserDetailAsync(new ColUserAddModel { UserName = name, EmployeeInfo = new EmployeeDetail() { EmployeeId = employeeId, EmailId = testEmail } });
            Assert.That(createdWithSameEmail, Is.Not.Null);
            Assert.That(createdWithSameEmail.isError, Is.EqualTo(true));
            Assert.That(createdWithSameEmail.listErrors.Any(a=>a.Contains("Employee Id already exist for another user.")), Is.EqualTo(true));
        }

        [Test]
        public async Task DeleteUserDetailTest()
        {
             //first create method
            string name = Guid.NewGuid().ToString();
            string testEmail = "testEmail@yopmali.com";
            string employeeId = "1234";
            var created = await UserDetailsService.CreateUserDetailAsync(new ColUserAddModel { UserName = name, EmployeeInfo = new EmployeeDetail() { EmployeeId = employeeId, EmailId = testEmail } });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            string id = (string)created.result;

            var deleted = await UserDetailsService.DeleteUserDetail(created.result.ToString());
            Assert.That(deleted.result, Is.True);

        }

        [Test]
        public async Task UpdateUserDetailTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            string testEmail = "testEmail@yopmali.com";
            string employeeId = "1234";
            var created = await UserDetailsService.CreateUserDetailAsync(new ColUserAddModel { UserName = name, EmployeeInfo = new EmployeeDetail() { EmployeeId = employeeId, EmailId = testEmail } });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            string id = (string)created.result;

            //check name save in db 
            var userDetailById = await UserDetailsService.GetUserDetailAsync(id);
            Assert.That(userDetailById, Is.Not.Null);
            Assert.That(userDetailById.result, Is.Not.Null);
            UserDetailsViewModel userDetailsViewModels = (UserDetailsViewModel)userDetailById.result;
            Assert.That(userDetailsViewModels.UserName, Is.EqualTo(name));
            Assert.That(userDetailsViewModels.EmployeeInfo, Is.Not.Null);
            Assert.That(userDetailsViewModels.EmployeeInfo.EmailId, Is.EqualTo(testEmail));

            //update new name
            ColUserEditModel userDetails = new ColUserEditModel();
            userDetails.Id = id;
            string newName = Guid.NewGuid().ToString();
            userDetails.UserName = newName;
            userDetails.EmployeeInfo = new EmployeeDetail() { EmailId = testEmail, EmployeeId = employeeId };
            var updated =  await UserDetailsService .UpdateUserDetailAsync(userDetails);
            Assert.That(updated.result, Is.EqualTo(true));

            var updatedUserDetailById =  await UserDetailsService.GetUserDetailAsync(id);
            Assert.That(updatedUserDetailById, Is.Not.Null);
            Assert.That(updatedUserDetailById.result, Is.Not.Null);
            UserDetailsViewModel updatedUserDetailsViewModels = (UserDetailsViewModel)updatedUserDetailById.result;
            Assert.That(updatedUserDetailsViewModels.UserName, Is.EqualTo(newName));
        }

        [Test]
        public async Task GetUserDetailsTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            string testEmail = "testEmail@yopmali.com";
            string employeeId = "1234";
            var created = await UserDetailsService.CreateUserDetailAsync(new ColUserAddModel { UserName = name, EmployeeInfo = new EmployeeDetail() { EmployeeId = employeeId, EmailId = testEmail } });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            string id = (string)created.result;

            var getRecords = await UserDetailsService.GetUserDetailsAsync();
            Assert.That(getRecords, Is.Not.Null);
            List<UserDetailsViewModel> outputModel = new List<UserDetailsViewModel>();
            outputModel = (List<UserDetailsViewModel>)getRecords.result;
            Assert.That(outputModel.Where(a => a.UserName == name).Select(a => a.UserName).FirstOrDefault(), Is.EqualTo(name));

        }
        [Test]
        public async Task GetAllUsersTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            string testEmail = "testEmail@yopmali.com";
            string employeeId = "1234";
            var created = await UserDetailsService.CreateUserDetailAsync(new ColUserAddModel { UserName = name, EmployeeInfo = new EmployeeDetail() { EmployeeId = employeeId, EmailId = testEmail } });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            string id = (string)created.result;

            //check name save in db 
            GetAllUsersInputModel allUserDetailsInputModel = new GetAllUsersInputModel();
            allUserDetailsInputModel.Skip = 0;
            allUserDetailsInputModel.Take = 1;
            allUserDetailsInputModel.Search = "1234";

            var userDetails= await UserDetailsService.GetAllUsers(allUserDetailsInputModel);
            Assert.That(userDetails, Is.Not.Null);
            Assert.That(userDetails.result, Is.Not.Null);
            List<UserDetailsViewModel> userDetailsList = (List<UserDetailsViewModel>)userDetails.result;
            Assert.That(userDetailsList, Is.Not.Null);
            Assert.That(userDetailsList.Count, Is.GreaterThanOrEqualTo(1));
        }
        [Test]
        public async Task GetUserDetailById()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            string testEmail = "testEmail@yopmali.com";
            string employeeId = "1234";
            var created = await UserDetailsService.CreateUserDetailAsync(new ColUserAddModel { UserName = name, EmployeeInfo = new EmployeeDetail() { EmployeeId = employeeId, EmailId = testEmail } });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);


            //Getting
            string id = (string)created.result;
            var userDetailById = await UserDetailsService.GetUserDetailAsync(id);
            Assert.That(userDetailById, Is.Not.Null);
            Assert.That(userDetailById.result, Is.Not.Null);
            UserDetailsViewModel userDetailsViewModels = (UserDetailsViewModel)userDetailById.result;
            Assert.That(userDetailsViewModels.UserName, Is.EqualTo(name));
            Assert.That(userDetailsViewModels.EmployeeInfo, Is.Not.Null);
            Assert.That(userDetailsViewModels.EmployeeInfo.EmailId, Is.EqualTo(testEmail));

            //Validating
            UserDetailsViewModel outputModels = (UserDetailsViewModel)userDetailById.result;
            Assert.That(outputModels.UserName == name);
        }

        #region TestOtp
        [Test]
        public async Task SendOTPTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            string Emailid = Guid.NewGuid().ToString();
            var created = await UserDetailsService.CreateUserDetailAsync(new ColUserAddModel { UserName = name, EmployeeInfo = new EmployeeDetail() { EmailId = Emailid } });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            //Send OTP and check in DB
            var created1 = await UserDetailsService.SendOTPForForgotPassword(new SendOTPInputModel { Username = name, EmailId = Emailid });
            Assert.That(created1, Is.Not.Null);
            Assert.That(created1.result, Is.Not.Null);
            //verify OTP
            ColUser result = (ColUser)created1.result;
            VerifyOTPInputModel otpdetails = new VerifyOTPInputModel();
            otpdetails.OTP = result.OTPDetail.OTP;
            otpdetails.EmailID = result.EmployeeInfo.EmailId;
            var verifyotp = await UserDetailsService.VerifyOTPForForgotPassword(otpdetails);
            Assert.That(verifyotp.isError, Is.EqualTo(false));
        }
        #endregion
    }
}
