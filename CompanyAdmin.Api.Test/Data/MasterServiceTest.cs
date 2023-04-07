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
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAdmin.Api.Test.Data
{
    public class MasterServiceTest : DatabaseTest
    {
        #region Department
        [Test]
        public async Task CreateDepartmentTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            string testEmail = "testEmail@yopmali.com";
            string employeeId = "1234";
            var created =  await MasterService.CreateDepartment(new AddDepartmentInputModel { Name = name });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            string id = (string)created.result;

            //check name save in db
            var getRecords = await MasterService.GetAllDepartments();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            var outputModels = (List<DepartmentOutputModel>)getRecords.result;
            Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));
        }
        [Test]
        public async Task DeleteDepartmentTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            string testEmail = "testEmail@yopmali.com";
            string employeeId = "1234";
            var created = await MasterService.CreateDepartment(new AddDepartmentInputModel { Name = name });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            string id = (string)created.result;

            var deleted = await MasterService .DeleteDepartment(created.result.ToString());
            Assert.That(deleted.result, Is.True);
        }
        [Test]
        public async Task UpdateDepartmentTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            string testEmail = "testEmail@yopmali.com";
            string employeeId = "1234";
            var created = await MasterService.CreateDepartment(new AddDepartmentInputModel { Name = name });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            string id = (string)created.result;

        //    var deleted = MasterService.DeleteDepartment(created);
        //    Assert.That(deleted, Is.Not.Null);
        //    Assert.That(deleted, Is.True);
        //    List<DepartmentOutputModel> outputModels = new List<DepartmentOutputModel>();
        //    outputModels = (List<DepartmentOutputModel>)getRecords.result;
        //    Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));
        //}
        //[Test]
        //public async Task UpdateDepartmentTest()
        //{
        //    //first create method
        //    string name = Guid.NewGuid().ToString();
        //    var created = MasterService.CreateDepartment(new AddDepartmentInputModel { Name = name });
        //    Assert.That(created, Is.Not.Null);

            //check name save in db 
            var getRecords = await MasterService .GetAllDepartments();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            List<DepartmentOutputModel> outputModels = new List<DepartmentOutputModel>();
            outputModels = (List<DepartmentOutputModel>)getRecords.result;
            Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));

            //update new name
            DepartmentOutputModel getUpdateDepartment = outputModels.Where(a => a.Name == name).FirstOrDefault();
            EditDepartmentInputModel editDept = new EditDepartmentInputModel();
            editDept.Id = getUpdateDepartment.Id;
            string newName = Guid.NewGuid().ToString();
            editDept.Name = newName;
            var updated = await MasterService .UpdateDepartment(editDept);
            Assert.That(updated, Is.Not.Null);
            Assert.That(updated.result, Is.Not.Null);
            Assert.That(updated.result, Is.EqualTo(editDept.Id));

            var getById = await MasterService.GetDepartmentDetails(getUpdateDepartment.Id);
            DepartmentOutputModel departmentDetail = (DepartmentOutputModel)getById.result;
            Assert.That(departmentDetail, Is.Not.Null);
            Assert.That(departmentDetail.Name, Is.EqualTo(newName));
        }
        
        [Test]
        public async Task GetAllDepartmentsTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            string testEmail = "testEmail@yopmali.com";
            string employeeId = "1234";
            var created = await MasterService.CreateDepartment(new AddDepartmentInputModel { Name = name });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            string id = (string)created.result;

            //check name save in db 
            var getRecords = await MasterService .GetAllDepartments();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            Assert.That(getRecords.result, Has.Count.EqualTo(1));
            List<DepartmentOutputModel> outputModels = new List<DepartmentOutputModel>();
            outputModels = (List<DepartmentOutputModel>)getRecords.result;
            Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));

        }
        [Test] 
        public async Task GetDepartmentDetailsTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            string testEmail = "testEmail@yopmali.com";
            string employeeId = "1234";
            var created = await MasterService.CreateDepartment(new AddDepartmentInputModel { Name = name });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            string id = (string)created.result;

            //ger role by id
            var getById = await MasterService.GetDepartmentDetails(created.result.ToString());
            Assert.That(getById.result, Is.Not.Null);

        }
        #endregion

        #region Technical Skills
        [Test]
        public async Task AddTechnicalSkills()
        {
            //Creating
            string name = Guid.NewGuid().ToString();
            var created = await MasterService.AddTechnicalSkillsAsync(new AddTechnicalSkillsInputModel { Name = name });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            //Getting
            var getRecords = await MasterService.GetAllTechnicalSkillsAsync();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            //Validating
            List<TechnicalSkillsOutputModel> outputModels = (List<TechnicalSkillsOutputModel>)getRecords.result;
            Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));
        }

        [Test]
        public async Task GetTechnicalSkillsById()
        {
            //Creating
            string name = Guid.NewGuid().ToString();
            var created = await MasterService.AddTechnicalSkillsAsync(new AddTechnicalSkillsInputModel { Name = name });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            //Getting
            string Id = (string)created.result;
            var getRecords = await MasterService.GetTechnicalSkillsByIdAsync(Id);
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            //Validating
            TechnicalSkillsOutputModel outputModels = (TechnicalSkillsOutputModel)getRecords.result;
            Assert.That(outputModels.Name == name);
        }

        [Test]
        public async Task GetAllTechnicalSkills()
        {
            //Creating
            string name = Guid.NewGuid().ToString();
            var created = await MasterService.AddTechnicalSkillsAsync(new AddTechnicalSkillsInputModel { Name = name });
            Assert.That(created, Is.Not.Null);
            //Getting 
            var getRecords = await MasterService.GetAllTechnicalSkillsAsync();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            Assert.That(getRecords.result, Has.Count.EqualTo(1));
            //Validating
            List<TechnicalSkillsOutputModel> outputModels = (List<TechnicalSkillsOutputModel>)getRecords.result;
            Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));
        }
        [Test]
        public async Task DeleteTechnicalSkillsTest()
        {
            //Creating
            string name = Guid.NewGuid().ToString();
            var created = await MasterService.AddTechnicalSkillsAsync(new AddTechnicalSkillsInputModel { Name = name });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            string Id = (string)created.result;
            //Getting
            var getRecords = await MasterService.GetTechnicalSkillsByIdAsync(Id);
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            //Deleting
            var deleted = await MasterService.DeleteTechnicalSkillsByIdAsync(Id);
            Assert.That(deleted, Is.Not.Null);
            Assert.That(deleted.result, Is.True);
            //Cross Check
            getRecords = await MasterService.GetTechnicalSkillsByIdAsync(Id);
            Assert.That(getRecords.result, Is.Null);
        }
        [Test]
        public async Task UpdateTechnicalSkillsTest()
        {
            //Creating
            string name = Guid.NewGuid().ToString();
            var created = await MasterService.AddTechnicalSkillsAsync(new AddTechnicalSkillsInputModel { Name = name });
            Assert.That(created, Is.Not.Null);
            //Getting
            var getRecords = await MasterService.GetAllTechnicalSkillsAsync();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            //Validating
            List<TechnicalSkillsOutputModel> outputModels = (List<TechnicalSkillsOutputModel>)getRecords.result;
            Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));
            //Updating

            TechnicalSkillsOutputModel getUpdateTechnicalSkills = outputModels.Where(a => a.Name == name).FirstOrDefault();
            EditTechnicalSkillsInputModel editTechnicalSkills = new EditTechnicalSkillsInputModel();
            editTechnicalSkills.Id = getUpdateTechnicalSkills.Id;
            string newName = Guid.NewGuid().ToString();
            editTechnicalSkills.Name = newName;
            var updated = await MasterService.UpdateTechnicalSkillsAsync(editTechnicalSkills);
            Assert.That(updated, Is.Not.Null);
            Assert.That(updated.result, Is.Not.Null);
            //Checking the update
            string UpdatedId = (string)updated.result;
            var getById = await MasterService.GetTechnicalSkillsByIdAsync(UpdatedId);
            Assert.That(getById, Is.Not.Null);
            Assert.That(getById.result, Is.Not.Null);
            //Validating the update
            TechnicalSkillsOutputModel outputModel = (TechnicalSkillsOutputModel)getById.result;
            Assert.That(outputModel.Name, Is.EqualTo(newName));
        }
        #endregion

        #region Designation
        [Test]
        public async Task AddDesignation()
        {
            //Creating
            string name = Guid.NewGuid().ToString();
            var designationCreated =await MasterService.AddDesignationAsync(new AddDesignationInputModel { Name = name });
            Assert.That(designationCreated, Is.Not.Null);
            Assert.That(designationCreated.result, Is.Not.Null);
            //Getting
            var getDesignationRecords = await MasterService.GetAllDesignationAsync();
            Assert.That(getDesignationRecords, Is.Not.Null);
            Assert.That(getDesignationRecords.result, Is.Not.Null);
            //Validating
            List<DesignationOutputModel> outputModels = (List<DesignationOutputModel>)getDesignationRecords.result;
            Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));
        }
        [Test]
        public async Task GetAllDesignation()
        {
            //Creating
            string name = Guid.NewGuid().ToString();
            var designationCreated = await MasterService.AddDesignationAsync(new AddDesignationInputModel { Name = name });
            Assert.That(designationCreated, Is.Not.Null);
            Assert.That(designationCreated.result, Is.Not.Null);
            //Getting 
            var getDesignationRecords = await MasterService.GetAllDesignationAsync();
            Assert.That(getDesignationRecords, Is.Not.Null);
            Assert.That(getDesignationRecords.result, Is.Not.Null);
            Assert.That(getDesignationRecords.result, Has.Count.EqualTo(1));
            //Validating
            List<DesignationOutputModel> outputModels = (List<DesignationOutputModel>)getDesignationRecords.result;
            Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));
        }
        [Test]
        public async Task GetDesignationById()
        {
            //Creating
            string name = Guid.NewGuid().ToString();
            var designationCreated = await MasterService.AddDesignationAsync(new AddDesignationInputModel { Name = name });
            Assert.That(designationCreated, Is.Not.Null);
            Assert.That(designationCreated.result, Is.Not.Null);
            //Getting
            string Id = (string)designationCreated.result;
            var getDesignationRecords = await MasterService.GetDesignationByIdAsync(Id);
            Assert.That(getDesignationRecords, Is.Not.Null);
            Assert.That(getDesignationRecords.result, Is.Not.Null);
            //Validating
            DesignationOutputModel outputModels = (DesignationOutputModel)getDesignationRecords.result;
            Assert.That(outputModels.Name == name);
        }
        [Test]
        public async Task DeleteDesignationTest()
        {
            //Creating
            string name = Guid.NewGuid().ToString();
            var designationCreated = await MasterService.AddDesignationAsync(new AddDesignationInputModel { Name = name });
            Assert.That(designationCreated, Is.Not.Null);
            Assert.That(designationCreated.result, Is.Not.Null);
            string Id = (string)designationCreated.result;
            //Getting
            var getDesignationRecords = await MasterService.GetDesignationByIdAsync(Id);
            Assert.That(getDesignationRecords, Is.Not.Null);
            Assert.That(getDesignationRecords.result, Is.Not.Null);
            //Deleting
            var DesignationDeleted = await MasterService.DeleteDesignationByIdAsync(Id);
            Assert.That(DesignationDeleted, Is.Not.Null);
            Assert.That(DesignationDeleted.result, Is.True);
            //Cross Check
            getDesignationRecords = await MasterService.GetDesignationByIdAsync(Id);
            Assert.That(getDesignationRecords.result, Is.Null);
        }
        [Test]
        public async Task UpdateDesignationTest()
        {
            //Creating
            string name = Guid.NewGuid().ToString();
            var designationCreated = await MasterService.AddDesignationAsync(new AddDesignationInputModel { Name = name });
            Assert.That(designationCreated, Is.Not.Null);
            Assert.That(designationCreated.result, Is.Not.Null);
            //Getting
            var getDesignationRecords = await MasterService.GetAllDesignationAsync();
            Assert.That(getDesignationRecords, Is.Not.Null);
            Assert.That(getDesignationRecords.result, Is.Not.Null);
            //Validating
            List<DesignationOutputModel> outputModels = (List<DesignationOutputModel>)getDesignationRecords.result;
            Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));
            //Updating
            DesignationOutputModel getUpdateDesignation = outputModels.Where(a => a.Name == name).FirstOrDefault();
            EditDesignationInputModel editDesignation = new EditDesignationInputModel();
            editDesignation.Id = getUpdateDesignation.Id;
            string newName = Guid.NewGuid().ToString();
            editDesignation.Name = newName;
            var updatedDesignation = await MasterService.UpdateDesignationAsync(editDesignation);
            Assert.That(updatedDesignation, Is.Not.Null);
            Assert.That(updatedDesignation.result, Is.Not.Null);
            //Checking the update
            string UpdatedId = (string)updatedDesignation.result;
            var getById = await MasterService.GetDesignationByIdAsync(UpdatedId);
            Assert.That(getById, Is.Not.Null);
            Assert.That(getById.result, Is.Not.Null);
            //Validating the update
            DesignationOutputModel outputModel = (DesignationOutputModel)getById.result;
            Assert.That(outputModel.Name, Is.EqualTo(newName));
        }
        #endregion
        //    //check name save in db 
        //    var getRecords = MasterService.GetAllDepartments();
        //    Assert.That(getRecords, Is.Not.Null);
        //    Assert.That(getRecords.result, Is.Not.Null);
        //    //Assert.That(getRecords.Count(), Is.EqualTo(1));
        //    List<DepartmentOutputModel> outputModels = new List<DepartmentOutputModel>();
        //    outputModels = (List<DepartmentOutputModel>)getRecords.result;
        //    Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));

        //}
        #region Channel

        [Test]
        public async Task GetAllChannelTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            var created = await MasterService.CreateChannelAsync(new AddChannelInputModel { Name = name });
            Assert.That(created, Is.Not.Null);



            //check name save in db 
            var getRecords = await MasterService.GetAllChannelAsync();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            //Assert.That(getRecords.Count(), Is.EqualTo(1));
            List<ChannelOutputModel> outputModels = new List<ChannelOutputModel>();
            outputModels = (List<ChannelOutputModel>)getRecords.result;
            Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));

        }

        [Test]
        public async Task CreateChannelTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            var created = await MasterService .CreateChannelAsync(new AddChannelInputModel { Name = name });
            Assert.That(created, Is.Not.Null);

            //check name save in db
            var getRecords = await MasterService.GetAllChannelAsync();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            var outputModels = (List<ChannelOutputModel>)getRecords.result;
            Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));
        }

        [Test]
        public async Task UpdateChannelTest()
        {
            //Creating
            string name = Guid.NewGuid().ToString();
            var created = await MasterService.CreateChannelAsync(new AddChannelInputModel { Name = name });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            //Getting
            var get = await MasterService.GetAllChannelAsync();
            Assert.That(get, Is.Not.Null);
            //Assert.That(get.result, Is.Not.Null);
            //Validating
            List<ChannelOutputModel> outputModels = (List<ChannelOutputModel>)get.result;
            Assert.That(outputModels.Where(a => a.Name == name).Select(a => a.Name).FirstOrDefault(), Is.EqualTo(name));
            //Updating
            ChannelOutputModel getUpdateChannel = outputModels.Where(a => a.Name == name).FirstOrDefault();
            EditChannelInputModel editChannel = new EditChannelInputModel();
            editChannel.Id = getUpdateChannel.Id;
            string newName = Guid.NewGuid().ToString();
            editChannel.Name = newName;
            var updatedChannel = await MasterService.UpdateChannelAsync(editChannel);
            Assert.That(updatedChannel, Is.Not.Null);
            Assert.That(updatedChannel.result, Is.Not.Null);
            //Checking the update
            string UpdatedId = (string)updatedChannel.result;
            var getById = await MasterService.GetChannelDetailsAsync(UpdatedId);
            Assert.That(getById, Is.Not.Null);
            Assert.That(getById.result, Is.Not.Null);
            //Validating the update
            ChannelOutputModel outputModel = (ChannelOutputModel)getById.result;
            Assert.That(outputModel.Name, Is.EqualTo(newName));


        }

        [Test]
        public async Task DeleteChannelTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            var created = await MasterService.CreateChannelAsync(new AddChannelInputModel { Name = name });
            Assert.That(created, Is.Not.Null);

            var deleted = await MasterService.DeleteChannelAsync(created.result.ToString());
            Assert.That(deleted, Is.Not.Null);
            Assert.That(deleted.result, Is.True);
        }
        #endregion

        #region Achievement
       
            [Test]
            public async Task GetAllAchievementTest()
            {
                //first create method
                string name = Guid.NewGuid().ToString();
                var created =await MasterService.CreateAchievementAsync(new AddAchievementInputModel { AchievementName = name });
                Assert.That(created, Is.Not.Null);

                //check name save in db              
                var getRecords = await MasterService.GetAllAchievementsAsync();
                Assert.That(getRecords, Is.Not.Null);
                Assert.That(getRecords.result, Is.Not.Null);
                Assert.That(getRecords.result, Has.Count.EqualTo(1));
            
                //Validating
                List<AchievementOutputModel> lstAchievement = (List<AchievementOutputModel>)getRecords.result;           
                Assert.That(lstAchievement.Where(a => a.AchievementName == name).Select(a => a.AchievementName).FirstOrDefault(), Is.EqualTo(name));
            }

        [Test]
        public async Task CreateAchievementTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            var created = await MasterService .CreateAchievementAsync(new AddAchievementInputModel { AchievementName = name });
            Assert.That(created, Is.Not.Null);
           
            //check name save in db          
            var getRecords =await MasterService.GetAllAchievementsAsync();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            var outputModels = (List<AchievementOutputModel>)getRecords.result;
            Assert.That(outputModels.Where(a => a.AchievementName == name).Select(a => a.AchievementName).FirstOrDefault(), Is.EqualTo(name));
        }

        [Test]      
        public async Task UpdateAchievementTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();         
            var created = await MasterService.CreateAchievementAsync(new AddAchievementInputModel { AchievementName = name });
            Assert.That(created, Is.Not.Null);
           
            //check name save in db         
            var getRecords =await MasterService.GetAllAchievementsAsync();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);           
            List<AchievementOutputModel> lstAchievement = new List<AchievementOutputModel>();
            lstAchievement = (List<AchievementOutputModel>)getRecords.result;
            Assert.That(lstAchievement.Where(a => a.AchievementName == name).Select(a => a.AchievementName).FirstOrDefault(), Is.EqualTo(name));

            //update new name         
            AchievementOutputModel updateAchievement = lstAchievement.Where(a => a.AchievementName == name).FirstOrDefault();
            EditAchievementInputModel editAchievement = new EditAchievementInputModel();
            editAchievement.Id = updateAchievement.Id;
            string newName = Guid.NewGuid().ToString();         
            editAchievement.AchievementName = newName;
            updateAchievement.AchievementName = newName;           
            var updated =await MasterService.UpdateAchievementAsync(editAchievement);
            Assert.That(updated, Is.Not.Null);
            Assert.That(updated.result, Is.Not.Null);          
            Assert.That(updated.result, Is.EqualTo(editAchievement.Id));
         
            var getById =await MasterService.GetAchievementDetailByIdAsync(editAchievement.Id);
            AchievementDetails getAchievementModel = (AchievementDetails)getById.result;
            Assert.That(getAchievementModel, Is.Not.Null);
            Assert.That(getAchievementModel.AchievementName, Is.EqualTo(newName));
        }

        [Test]
        public async Task DeleteAchievementTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            var created =await MasterService.CreateAchievementAsync(new AddAchievementInputModel { AchievementName = name });
            Assert.That(created, Is.Not.Null);

            var deleted =await MasterService.DeleteAchievementAsync(created.result.ToString());
            Assert.That(deleted.result, Is.True);
        }

        [Test]
        public async Task GetAchievementDetailByIdTest()
        {
            //creating
            string name = Guid.NewGuid().ToString();
            var created = await MasterService .CreateAchievementAsync(new AddAchievementInputModel { AchievementName = name });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);

            //getting
            string Id = (string)created.result;
            var getRecords =await MasterService.GetAchievementDetailByIdAsync(Id);
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);

            //Validating
            AchievementDetails achievementModel = (AchievementDetails)getRecords.result;
            Assert.That(achievementModel.AchievementName == name);
        }
            #endregion
        }
    }


   

