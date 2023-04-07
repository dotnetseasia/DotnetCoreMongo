using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models;
using CompanyAdmin.API.Models.OutputModel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyAdmin.Api.Test.Data
{
    public class BadgeDetailsServiceTest : DatabaseTest
    {
        [Test]
        public async Task CreateBadgeDetailTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            var created =  await MasterService.CreateBadgeAsync(new AddBadgesInputModel { Name = name });
            Assert.That(created, Is.Not.Null);


            //check name save in db 
            var getRecords = await MasterService.GetAllBadgesAsync();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            List<GetBadgesOutputModel> outputModel = new List<GetBadgesOutputModel>();
            outputModel = (List<GetBadgesOutputModel>)getRecords.result;
            Assert.That(outputModel.Where(a => a.BadgeName == name).Select(a => a.BadgeName).FirstOrDefault(), Is.EqualTo(name));
        }

        [Test]
        public async Task DeleteBadgeDetailTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            var created = await MasterService .CreateBadgeAsync(new AddBadgesInputModel { Name = name });
            Assert.That(created, Is.Not.Null);


            //check name save in db 
            var deleteBadge = await MasterService .DeleteBadgeAsync(created.result.ToString());
            Assert.That(deleteBadge.result, Is.Not.Null);
            Assert.That(deleteBadge.result, Is.True );
           
        }
        [Test]
        public async Task UpdateBadgeDetailTest()
        {

            string name = Guid.NewGuid().ToString();
            var created = await MasterService.CreateBadgeAsync(new AddBadgesInputModel { Name = name });
            Assert.That(created, Is.Not.Null);


            //check name save in db 
            var getRecords = await MasterService .GetAllBadgesAsync();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            List<GetBadgesOutputModel> outputModels = new List<GetBadgesOutputModel>();
            outputModels = (List<GetBadgesOutputModel>)getRecords.result;
            Assert.That(outputModels.Where(a => a.BadgeName == name).Select(a => a.BadgeName).FirstOrDefault(), Is.EqualTo(name));

            //update new name
            GetBadgesOutputModel getUpdateUser = outputModels.Where(a => a.BadgeName == name).FirstOrDefault();
            EditBadgesInputModel badgeDetails = new EditBadgesInputModel();
            badgeDetails.Id = getUpdateUser.Id;
            string newName = Guid.NewGuid().ToString();
            badgeDetails.Name = newName;
            var updated = await MasterService .UpdateBadgeAsync(badgeDetails);
            Assert.That(updated, Is.Not.Null);
            Assert.That(updated.result, Is.Not.Null);

            var getById = await MasterService .GetBadgeByIdAsync(badgeDetails.Id);
            Assert.That(getById.result, Is.Not.Null);
            GetBadgesOutputModel getBadgeModel = new GetBadgesOutputModel();
            getBadgeModel = (GetBadgesOutputModel)getById.result;
            Assert.That(getBadgeModel.BadgeName, Is.EqualTo(newName));
        }

        [Test]
        public async Task GetBadgeDetailsTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            var created = await MasterService.CreateBadgeAsync(new AddBadgesInputModel { Name = name });
            Assert.That(created, Is.Not.Null);


            //check name save in db 
            var getRecords = await MasterService.GetAllBadgesAsync();
            Assert.That(getRecords, Is.Not.Null);
            Assert.That(getRecords.result, Is.Not.Null);
            List<GetBadgesOutputModel> outputModel = new List<GetBadgesOutputModel>();
            outputModel = (List<GetBadgesOutputModel>)getRecords.result;
            Assert.That(outputModel.Where(a => a.BadgeName == name).Select(a => a.BadgeName).FirstOrDefault(), Is.EqualTo(name));
        }

        [Test]
        public async Task GetBadgeDetailsByIdTest()
        {
            //first create method
            string name = Guid.NewGuid().ToString();
            var created = await MasterService.CreateBadgeAsync(new AddBadgesInputModel { Name = name });
            Assert.That(created, Is.Not.Null);

            var getById = await MasterService.GetBadgeByIdAsync(created.result.ToString());
            Assert.That(getById.result, Is.Not.Null);
           
           
        }
    }
}
