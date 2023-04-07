using CompanyAdmin.API.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAdmin.Api.Test.Data
{
    public class RoleDetailsServiceTest : DatabaseTest
    {

        [Test]
        public async Task CreateRoleTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            var created = await RoleManagementService.CreateRoleDetailAsync(new RoleDetailsAddModel { Name = name });
            Assert.That(created, Is.Not.Null);
            string id = (string)created.result;

            //check name save in db
            var getRecords = await RoleManagementService.GetRoleByIdAsync(id);
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            GetRoleDetailsModel outputModels = new GetRoleDetailsModel();
            outputModels = (GetRoleDetailsModel)getRecords.result;
            Assert.That(outputModels.Name, Is.EqualTo(name));
        }
        [Test]
        public async Task DeleteRoleTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            var created = await RoleManagementService.CreateRoleDetailAsync(new RoleDetailsAddModel { Name = name });
            Assert.That(created, Is.Not.Null);

            //check delete role in  db
            var deleted = await RoleManagementService.DeleteRoleByIdAsync(created.result.ToString());
            Assert.That(deleted, Is.Not.Null);
            Assert.That(deleted, Is.True);


            //Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));
        }

        [Test]
        public async Task UpdateRoleDetailTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            var created = await RoleManagementService.CreateRoleDetailAsync(new RoleDetailsAddModel { Name = name });
            Assert.That(created, Is.Not.Null);


            //check name save in db 
            var getRecords = await RoleManagementService.GetRoleDetailsAsync();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            List<GetRoleDetailsModel> outputModels = new List<GetRoleDetailsModel>();
            outputModels = (List<GetRoleDetailsModel>)getRecords.result;
            Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));

            //update new name
            GetRoleDetailsModel getUpdateUser = outputModels.Where(a => a.Name == name).FirstOrDefault();
            RoleDetailsEditModel RoleDetails = new RoleDetailsEditModel();
            RoleDetails.Id = getUpdateUser.Id;
            string newName = Guid.NewGuid().ToString();
            RoleDetails.Name = newName;
            var updated = await RoleManagementService.EditRoleAsync(RoleDetails);
            Assert.That(updated, Is.Not.Null);
            Assert.That(updated.result, Is.Not.Null);

            var getById = await RoleManagementService.GetRoleByIdAsync(RoleDetails.Id);
            Assert.That(getById.result, Is.Not.Null);
            GetRoleDetailsModel getRoleDetailsModel = new GetRoleDetailsModel();
            getRoleDetailsModel = (GetRoleDetailsModel)getById.result;
            Assert.That(getRoleDetailsModel.Name, Is.EqualTo(newName));
        }
        [Test]
        public async Task GetRoleDetailsTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            var created = await  RoleManagementService.CreateRoleDetailAsync(new RoleDetailsAddModel { Name = name });
            Assert.That(created, Is.Not.Null);


            //check name save in db 
            var getRecords = await RoleManagementService .GetRoleDetailsAsync();
            Assert.That(getRecords, Is.Not.Null);
            List<GetRoleDetailsModel> outputModels = new List<GetRoleDetailsModel>();
            outputModels = (List<GetRoleDetailsModel>)getRecords.result;
            Assert.That(outputModels.Count(), Is.EqualTo(1));
            Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));
        }
        [Test]
        public async Task GetRoleDetailsByidTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            var created = await RoleManagementService.CreateRoleDetailAsync(new RoleDetailsAddModel { Name = name });
            Assert.That(created, Is.Not.Null);

            //save role in db
            var getRecords = await RoleManagementService.GetRoleDetailsAsync();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            List<GetRoleDetailsModel> outputModels = new List<GetRoleDetailsModel>();
            outputModels = (List<GetRoleDetailsModel>)getRecords.result;
            Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));

            //ger role by id
            GetRoleDetailsModel getupdatedid = outputModels.Where(a => a.Name == name).FirstOrDefault();
            RoleDetailsByIdModel getid = new RoleDetailsByIdModel();
            getid.Id = getupdatedid.Id;
            var getById = await RoleManagementService.GetRoleByIdAsync(getid.Id);
            Assert.That(getById.result, Is.Not.Null);
        }

    }
}
