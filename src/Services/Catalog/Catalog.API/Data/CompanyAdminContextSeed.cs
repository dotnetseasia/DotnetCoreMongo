using CompanyAdmin.API.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace CompanyAdmin.API.Data
{
    public class CompanyAdminContextSeed
    {
        public static void SeedData(IMongoCollection<ColUser> userCollection)
        {
            bool existProduct = userCollection.Find(p => true).Any();
            if (!existProduct)
            {
                userCollection.InsertManyAsync(GetPreconfiguredUsers());
            }
        }
        public static void ClearRolesData(IMongoCollection<Roles> rolesCollection)
        {
            var rolesList = rolesCollection.Find(p => true)
                           .ToList();
            if (rolesList.Count() > 0)
            {
                // Get the _id values of the found documents
                var ids = rolesList.Select(d => d.Id);
                // Create an $in filter for those ids
                var idsFilter = Builders<Roles>.Filter.In(d => d.Id, ids);
                rolesCollection.DeleteManyAsync(idsFilter);
            }
        }

        public static void ClearUserDetailsData(IMongoCollection<ColUser> userCollection)
        {
            var users = userCollection.Find(p => true)
                           .ToList();
            if (users.Count > 0)
            {
                // Get the _id values of the found documents
                var ids = users.Select(d => d.Id);
                // Create an $in filter for those ids
                var idsFilter = Builders<ColUser>.Filter.In(d => d.Id, ids);
                userCollection.DeleteManyAsync(idsFilter);
            }
        }
        #region Department
        public static void ClearDepartmentData(IMongoCollection<colDepartment> DepartmentCollection)
        {
            var DepartmentlList = DepartmentCollection.Find(p => true)
                           .ToList();
            if (DepartmentlList.Count() > 0)
            {
                // Get the _id values of the found documents
                var ids = DepartmentlList.Select(d => d.Id);
                // Create an $in filter for those ids
                var idsFilter = Builders<colDepartment>.Filter.In(d => d.Id, ids);
                DepartmentCollection.DeleteManyAsync(idsFilter);
            }
        }
        #endregion
        #region Technical Skills
        public static void ClearTechnicalSkillsData(IMongoCollection<ColTechnicalSkills> TechnicalSkillsCollection)
        {
            var TechnicalSkillList = TechnicalSkillsCollection.Find(p => true)
                           .ToList();
            if (TechnicalSkillList.Count() > 0)
            {
                // Get the _id values of the found documents
                var ids = TechnicalSkillList.Select(d => d.Id);
                // Create an $in filter for those ids
                var idsFilter = Builders<ColTechnicalSkills>.Filter.In(d => d.Id, ids);
                TechnicalSkillsCollection.DeleteManyAsync(idsFilter);
            }
        }
        #endregion
        #region Designation
        public static void ClearDesignationData(IMongoCollection<ColDesignation> DesignationCollection)
        {
            var DesignationList = DesignationCollection.Find(p => true)
                           .ToList();
            if (DesignationList.Count() > 0)
            {
                // Get the _id values of the found documents
                var ids = DesignationList.Select(d => d.Id);
                // Create an $in filter for those ids
                var idsFilter = Builders<ColDesignation>.Filter.In(d => d.Id, ids);
                DesignationCollection.DeleteManyAsync(idsFilter);
            }
        }
        #endregion

        #region Achievement
        public static void ClearAchievementData(IMongoCollection<AchievementDetails> AchievementCollection)
        {
            var AchievementList = AchievementCollection.Find(p => true)
                           .ToList();
            if (AchievementList.Count() > 0)
            {
                // Get the _id values of the found documents
                var ids = AchievementList.Select(d => d.Id);
                // Create an $in filter for those ids
                var idsFilter = Builders<AchievementDetails>.Filter.In(d => d.Id, ids);
                AchievementCollection.DeleteManyAsync(idsFilter);
            }
        }
        #endregion

        #region Sprint
        public static void ClearSprintData(IMongoCollection<colSprint> SprintCollection)
        {
            var SprintList = SprintCollection.Find(p => true)
                           .ToList();
            if (SprintList.Count() > 0)
            {
                // Get the _id values of the found documents
                var ids = SprintList.Select(d => d.Id);
                // Create an $in filter for those ids
                var idsFilter = Builders<colSprint>.Filter.In(d => d.Id, ids);
                SprintCollection.DeleteManyAsync(idsFilter);
            }
        }
        #endregion



        private static IEnumerable<ColUser> GetPreconfiguredUsers()
        {
            return new List<ColUser>()
            {
                new ColUser()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    UserName = "Staff A",                   
                    ImageFile = "product-1.png"
                },
                new ColUser()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    UserName = "Staff B",
                    ImageFile = "product-2.png"
                },
                new ColUser()
                {
                    Id = "602d2149e773f2a3990b47f7",
                    UserName = "User C",
                    ImageFile = "product-3.png"
                }
            };
        }
    }
}
