using CompanyAdmin.API.Data.Interfaces;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Repositories
{
    public class SprintRepository : ISprintRepository
    {
        private readonly ICompanyAdminContext _context;

        public SprintRepository(ICompanyAdminContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #region Sprint

        public async Task<string> AddSprintAsync(colSprint ColSprint)
        {
            await _context.colSprint.InsertOneAsync(ColSprint);
            return ColSprint.Id;
        }

        public async Task<IEnumerable<colSprint>> GetAllSprintAsync()
        {
            return await _context
                            .colSprint
                            .Find(p => p.IsDeleted == false)
                            .ToListAsync();
        }


        public async Task<bool> DeleteSprintByIdAsync(string id)
        {
            FilterDefinition<colSprint> filter = Builders<colSprint>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .colSprint
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }


        public async Task<string> UpdateSprintAsync(colSprint colSprint)
        {
            await _context.colSprint.ReplaceOneAsync(filter: g => g.Id == colSprint.Id, replacement: colSprint);
            return colSprint.Id;
        }
        public async Task<colSprint> GetSprintBySprintName(string SprintName)
        {
            FilterDefinition<colSprint> filter = Builders<colSprint>.Filter.Eq(p => p.SprintName, SprintName);
            return await _context
                            .colSprint
                            .Find(filter)
                            .FirstOrDefaultAsync();
        }

        public async Task<colSprint> GetSprintByIdAsync(string Id)
        {
            FilterDefinition<colSprint> filter = Builders<colSprint>.Filter.Eq(p => p.Id, Id);

            return await _context
                      .colSprint
                .Find(filter)
                 .FirstOrDefaultAsync();
        }
        

        #endregion

    }
}
