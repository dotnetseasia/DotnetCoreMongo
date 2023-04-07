using CompanyAdmin.API.Models.Sprint.Input;
using CompanyAdmin.API.Models.Sprint.Output;
using CompanyAdmin.API.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAdmin.Api.Test.Data
{
    public class SprintServiceTest : DatabaseTest
    {
        #region Sprint
        [Test]
        public async Task AddSprintAsync()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            var created = await SprintService.AddSprintAsync(new AddSprintInputModel { SprintName = name });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            string id = (string)created.result;
            //Getting
            var getRecords = await SprintService.GetAllSprintAsync();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            List<SprintOutputModel> outputModels = new List<SprintOutputModel>();
            outputModels = (List<SprintOutputModel>)getRecords.result;
            Assert.That(outputModels.Where(a => a.SprintName == name).Select(a => a.SprintName).FirstOrDefault(), Is.EqualTo(name));

            //Validating
            List<SprintOutputModel> outputModel = (List<SprintOutputModel>)getRecords.result;
            Assert.That(outputModel.Where(a => a.SprintName == name).Select(a => a.SprintName).FirstOrDefault(), Is.EqualTo(name));
        }


        [Test]
        public async Task GetAllSprintAsync()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            string testEmail = "testEmail@yopmali.com";
            string employeeId = "1234";
            var created = await SprintService.AddSprintAsync(new AddSprintInputModel { SprintName = name });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            string id = (string)created.result;

            //check name save in db 
            var getRecords = await SprintService.GetAllSprintAsync();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            Assert.That(getRecords.result, Has.Count.EqualTo(1));
            List<SprintOutputModel> outputModels = new List<SprintOutputModel>();
            outputModels = (List<SprintOutputModel>)getRecords.result;
            Assert.That(outputModels.Where(a => a.SprintName == name).Select(a => a.SprintName).FirstOrDefault(), Is.EqualTo(name));
        }

        [Test]
        public async Task DeleteSprintByIdAsync()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            string testEmail = "testEmail@yopmali.com";
            string employeeId = "1234";
            var created = await SprintService.AddSprintAsync(new AddSprintInputModel { SprintName = name });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            string id = (string)created.result;

            var deleted = await SprintService.DeleteSprintByIdAsync(created.result.ToString());
            Assert.That(deleted.result, Is.True);
        }

        [Test]
        public async Task UpdateSprintAsync()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            string testEmail = "testEmail@yopmali.com";
            string employeeId = "1234";
            var created = await SprintService.AddSprintAsync(new AddSprintInputModel { SprintName = name });
            Assert.That(created, Is.Not.Null);
            Assert.That(created.result, Is.Not.Null);
            string id = (string)created.result;
            //Getting
            var getRecords = await SprintService.GetAllSprintAsync();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            List<SprintOutputModel> outputModels = new List<SprintOutputModel>();
            outputModels = (List<SprintOutputModel>)getRecords.result;
            Assert.That(outputModels.Where(a => a.SprintName == name).Select(a => a.SprintName).FirstOrDefault(), Is.EqualTo(name));

            //Validating
            List<SprintOutputModel> outputModel = (List<SprintOutputModel>)getRecords.result;
            Assert.That(outputModel.Where(a => a.SprintName == name).Select(a => a.SprintName).FirstOrDefault(), Is.EqualTo(name));

            //Updating
            SprintOutputModel getUpdateSprint = outputModels.Where(a => a.SprintName == name).FirstOrDefault();
            EditSprintInputModel editSprint = new EditSprintInputModel();
            editSprint.Id = getUpdateSprint.Id;
            string newName = Guid.NewGuid().ToString();
            editSprint.SprintName = newName;
            var updated = await SprintService.UpdateSprintAsync(editSprint);
            Assert.That(updated, Is.Not.Null);
            Assert.That(updated.result, Is.Not.Null);

            //Checking the update
            string UpdatedId = (string)updated.result;
            var getById = await SprintService.GetSprintByIdAsync(UpdatedId);
            Assert.That(getById, Is.Not.Null);
            Assert.That(getById.result, Is.Not.Null);

            //Validating the update
            SprintOutputModel output = (SprintOutputModel)getById.result;
            Assert.That(output.SprintName, Is.EqualTo(newName));
        }
        #endregion
    }
}
