using CompanyAdmin.API.Data.Interfaces;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Entities.CommonEntities;
using CompanyAdmin.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Repositories
{
    public class MasterRepository : IMasterRepository
    {
        private readonly ICompanyAdminContext _context;

        public MasterRepository(ICompanyAdminContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

    #region Badges
        public async Task<string> CreateBadgeAsync(BadgeDetails badgeDetails)
        {
            await _context.BadgeDetails.InsertOneAsync(badgeDetails);
            return badgeDetails.Id;
        }

        public async Task<BadgeDetails> GetBadgeDetailById(string id)
        {
            return await _context
                           .BadgeDetails
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }
       
        public async Task<bool> UpdateBadgeDetailAsync(BadgeDetails badgeDetails)
        {
            var updateResult = await _context
                                        .BadgeDetails
                                        .ReplaceOneAsync(filter: g => g.Id == badgeDetails.Id, replacement: badgeDetails);
             return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public BadgeDetails GetBadgeByName(string name)
        {
            try  {
                FilterDefinition<BadgeDetails> filter = Builders<BadgeDetails>.Filter.Eq(p => p.BadgeName, name);
                return _context
                                .BadgeDetails
                                .Find(filter)
                                .FirstOrDefault();
            }
            catch(Exception ex)  {
                return new BadgeDetails();
            }
        }

        public BadgeDetails ValidateBadgeDetails(string id , string name)
        {
            try {
                FilterDefinition<BadgeDetails> filter = Builders<BadgeDetails>.Filter.Where(p => p.Id !=id && p.BadgeName == name);
                return _context
                                .BadgeDetails
                                .Find(filter)
                                .FirstOrDefault();
            }
            catch (Exception ex) {
                return new BadgeDetails();
            }
        }

     

        public async Task<BadgeDetails> GetBadgeByIdAsync(string id)
        {
            FilterDefinition<BadgeDetails> filter = Builders<BadgeDetails>.Filter.Eq(p => p.Id, id);
              return await _context
                            .BadgeDetails
                            .Find(filter)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BadgeDetails>> GetAllBadgesAsync()
        {
                return await _context
                                .BadgeDetails
                                .Find(p => true)
                                .ToListAsync();
        }

       public async Task<bool> DeleteBadgeAsync(string id)
       {
            FilterDefinition<BadgeDetails> filter = Builders<BadgeDetails>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _context
                                              .BadgeDetails
                                              .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
       }
    #endregion Badges

        
        public async Task<IEnumerable<colDepartment>> GetAllDepartments()
        {
            return await _context
                            .colDepartment
                            .Find(p => p.IsDeleted==false)
                            .ToListAsync();
        }
        public async Task<colDepartment> GetDepartmentDetails(string id)
        {
            FilterDefinition<colDepartment> filter = Builders<colDepartment>.Filter.Eq(p => p.Id, id);

           return await _context.colDepartment
                .Find(filter)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> DeleteDepartment(colDepartment colDept)
        {
            await _context.colDepartment.ReplaceOneAsync(filter: g => g.Id == colDept.Id, replacement: colDept);
            return  colDept.IsDeleted == null ? false : Convert.ToBoolean(colDept.IsDeleted);
        }
        public async Task<string> CreateDepartment(colDepartment colDept)
        {
            await _context.colDepartment.InsertOneAsync(colDept);
            return  colDept.Id;
        }
        public async Task<string> UpdateDepartment(colDepartment colDept)
        {
            await _context.colDepartment.ReplaceOneAsync(filter: g => g.Id == colDept.Id, replacement: colDept);
            return  colDept.Id;
        }

        public async Task<colDepartment> GetDepartmentByName(string departmentName)
        {
                FilterDefinition<colDepartment> filter = Builders<colDepartment>.Filter.Eq(p => p.Name, departmentName);
            return await _context
                            .colDepartment
                            .Find(filter)
                            .FirstOrDefaultAsync();
        }
        public async Task<colDepartment> ValidateDuplicateDepartmentName(string id, string departmentName)
        {
            return await _context
                            .colDepartment
                            .Find(p => p.Name == departmentName && p.Id != id)
                            .FirstOrDefaultAsync();
        }


        #region TechnicalSkills

        public async Task<string> AddTechnicalSkillsAsync(ColTechnicalSkills ColTechnicalSkills)
        {
            await _context.ColTechnicalSkills.InsertOneAsync(ColTechnicalSkills);
            return ColTechnicalSkills.Id;
        }

        public async Task<IEnumerable<ColTechnicalSkills>> GetAllTechnicalSkillsAsync()
        {
            return await _context
                            .ColTechnicalSkills
                            .Find(p => p.IsDeleted == false)
                            .ToListAsync();
        }

        public async Task<ColTechnicalSkills> GetTechnicalSkillsByIdAsync(string id)
        {
            FilterDefinition<ColTechnicalSkills> filter = Builders<ColTechnicalSkills>.Filter.Eq(p => p.Id, id);

            return await _context.ColTechnicalSkills
                 .Find(filter)
                 .FirstOrDefaultAsync();
        }

        public async Task<ColTechnicalSkills> GetTechnicalSkillsByNameAsync(string TechSkillsName)
        {
            FilterDefinition<ColTechnicalSkills> filter = Builders<ColTechnicalSkills>.Filter.Eq(p => p.Name, TechSkillsName);
            return await _context
                            .ColTechnicalSkills
                            .Find(filter)
                            .FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteTechnicalSkillsByIdAsync(string id)
        {
            FilterDefinition<ColTechnicalSkills> filter = Builders<ColTechnicalSkills>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .ColTechnicalSkills
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<ColTechnicalSkills> ValidateDuplicateTechnicalSkillNameAsync(string id, string TechnicalSkillName)
        {
            return await _context
                            .ColTechnicalSkills
                            .Find(p => p.Name == TechnicalSkillName && p.Id != id)
                            .FirstOrDefaultAsync();
        }

        public async Task<string> UpdateTechnicalSkillsAsync(ColTechnicalSkills colTechSkills)
        {
            await _context.ColTechnicalSkills.ReplaceOneAsync(filter: g => g.Id == colTechSkills.Id, replacement: colTechSkills);
            return colTechSkills.Id;
        }
        #endregion

        #region Designation
        public async Task<ColDesignation> GetDesignationByNameAsync(string DesignationName)
        {
            FilterDefinition<ColDesignation> filter = Builders<ColDesignation>.Filter.Eq(p => p.Name, DesignationName);
            return await _context
                            .ColDesignation
                            .Find(filter)
                            .FirstOrDefaultAsync();
        }
        public async Task<string> AddDesignationAsync(ColDesignation ColDesignation)
        {
            await _context.ColDesignation.InsertOneAsync(ColDesignation);
            return ColDesignation.Id;
        }
        public async Task<IEnumerable<ColDesignation>> GetAllDesignationAsync()
        {
            return await _context
                            .ColDesignation
                            .Find(p => p.IsDeleted == false)
                            .ToListAsync();
    }
    public async Task<ColDesignation> GetDesignationByIdAsync(string id)
    {
        FilterDefinition<ColDesignation> filter = Builders<ColDesignation>.Filter.Eq(p => p.Id, id);

        return await _context.ColDesignation
             .Find(filter)
             .FirstOrDefaultAsync();
    }
    public async Task<bool> DeleteDesignationByIdAsync(string id)
    {
        FilterDefinition<ColDesignation> filter = Builders<ColDesignation>.Filter.Eq(p => p.Id, id);

        DeleteResult deleteResult = await _context
                                            .ColDesignation
                                            .DeleteOneAsync(filter);
        return deleteResult.IsAcknowledged
            && deleteResult.DeletedCount > 0;
    }
    public async Task<string> UpdateDesignationAsync(ColDesignation ColDesignation)
    {
        await _context.ColDesignation.ReplaceOneAsync(filter: g => g.Id == ColDesignation.Id, replacement: ColDesignation);
        return ColDesignation.Id;
    }
    public async Task<ColDesignation> ValidateDuplicateDesignationNameAsync(string id, string DesignationName)
    {
        return await _context
                        .ColDesignation
                        .Find(p => p.Name == DesignationName && p.Id != id)
                        .FirstOrDefaultAsync();
    }
    #endregion

        #region Achievement
        public async Task<IEnumerable<AchievementDetails>> GetAllAchievementsAsync()
        {        
            return await _context
                            .AchievementDetails
                            .Find(p => true)
                             .ToListAsync();
        }
      
        public async Task<AchievementDetails> GetAchievementDetailByIdAsync(string id)
        {
            FilterDefinition<AchievementDetails> filter = Builders<AchievementDetails>.Filter.Eq(p => p.Id, id);

            return await _context
                            .AchievementDetails
                            .Find(filter)
                            .FirstOrDefaultAsync();
           
        }

        public async Task<AchievementDetails> GetAchievementDetailByNameAsync(string name)
        {
            FilterDefinition<AchievementDetails> filter = Builders<AchievementDetails>.Filter.Eq(p => p.AchievementName, name);

            return await _context
                            .AchievementDetails
                            .Find(filter)
                            .FirstOrDefaultAsync();
        }

        #region Channel

        public async Task<bool> DeleteChannelAsync(string id)
        {
            FilterDefinition<colChannel> filter = Builders<colChannel>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .colChannel
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<colChannel>> GetAllChannelAsync()
        {
            return await _context
                            .colChannel
                            .Find(p => p.IsDeleted == false)
                            .ToListAsync();
        }
        public async Task<string> CreateChannelAsync(colChannel colChan)
        {
            await _context.colChannel.InsertOneAsync(colChan);
            return colChan.Id;
        }

        public async Task<colChannel> GetChannelByNameAsync(string ChannelName)
        {
            FilterDefinition<colChannel> filter = Builders<colChannel>.Filter.Eq(p => p.Name, ChannelName);

             return await _context
                                                .colChannel
                                                .Find(filter)
                                                .FirstOrDefaultAsync();           
        }

        public async Task<colChannel> ValidateDuplicateChannelNameAsync(string id, string ChannelName)
        {
            return await _context
                            .colChannel
                            .Find(p => p.Name == ChannelName && p.Id != id)
                            .FirstOrDefaultAsync();
        }

        public async Task<colChannel> GetChannelDetailsAsync(string id)
        {
            FilterDefinition<colChannel> filter = Builders<colChannel>.Filter.Eq(p => p.Id, id);

            return await _context
                             .colChannel
                             .Find(filter)
                             .FirstOrDefaultAsync();
        }

        public async Task<string> CreateAchievementAsync(AchievementDetails achievementDetail)
        {
            await _context.AchievementDetails.InsertOneAsync(achievementDetail);
            return achievementDetail.Id;           
        }
       
        public async Task <bool> DeleteAchievementAsync(string id)

    {
        FilterDefinition<AchievementDetails> filter = Builders<AchievementDetails>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                              .AchievementDetails
                                              .DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged
            && deleteResult.DeletedCount > 0;
    }

        public async Task <AchievementDetails> ValidateAchievementDetailsAsync(string id, string name)
        {
            try
            {
                FilterDefinition<AchievementDetails> filter = Builders<AchievementDetails>.Filter.Where(p => p.Id == id && p.AchievementName == name);

                return await _context
                                .AchievementDetails
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                return new AchievementDetails();
            }
        }

        public async Task<string> UpdateAchievementAsync(AchievementDetails achievementDetails)
        {
          await  _context.AchievementDetails.ReplaceOneAsync(filter: g => g.Id == achievementDetails.Id, replacement: achievementDetails);
            return achievementDetails.Id;
        }
        #endregion
        

        public async Task<string> UpdateChannelAsync(colChannel colChan)
        {
           await _context.colChannel.ReplaceOneAsync(filter: g => g.Id == colChan.Id, replacement: colChan);
            return colChan.Id;
        }

       
        #endregion
    }
}
