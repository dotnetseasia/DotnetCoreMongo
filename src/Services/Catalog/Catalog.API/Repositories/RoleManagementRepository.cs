using CompanyAdmin.API.Data.Interfaces;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models;
using CompanyAdmin.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Repositories
{
    public class RoleManagementRepository : IRoleManagementRepository
    {
        private readonly ICompanyAdminContext _context;

        public RoleManagementRepository(ICompanyAdminContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<colRoleMaster> SaveMasterRole(colRoleMaster objRoleMaster)
        {
            await _context.colRoleMaster.InsertOneAsync(objRoleMaster);
            return objRoleMaster;
        }

      

        public async Task<string> CreateRoleDetailAsync(Roles role)
        {
           await _context.Roles.InsertOneAsync(role);
            return role.Id;
        }
        public async Task<bool> DeleteRoles(string id)
        {
            FilterDefinition<Roles> filter = Builders<Roles>.Filter.Where(x => x.Id == id && x.IsDeleted == false);

            DeleteResult deleteResult = await _context
                                                .Roles
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
        public async Task<Roles> GetRoleByName(string RoleName)
        {
            try
            {
                FilterDefinition<Roles> filter = Builders<Roles>.Filter.Eq(p => p.Name, RoleName);
                return await _context
                                .Roles
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                return new Roles();
            }
        }
        public async Task<Roles> ValidateDuplicateDepartmentName(string id ,string Name)
        {
           try
            {
                return await _context
                               .Roles
                               .Find(p => p.Name == Name && p.Id != id)
                               .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                return new Roles();
            }
        }

        public async Task<Roles> GetRoleByIdAsync(string id)
        {
            FilterDefinition<Roles> filter = Builders<Roles>.Filter.Eq(p => p.Id, id);

            return await _context.Roles
                            .Find(filter)
                            .FirstOrDefaultAsync();
        }
        public async Task<string> EditRoleAsync(Roles role)
        {
           await _context.Roles.ReplaceOneAsync(filter: g => g.Id == role.Id, replacement: role);
            return role.Id;
        }
        public async Task<bool> DeleteRoleByIdAsync(string id)
        {
            FilterDefinition<Roles> filter = Builders<Roles>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Roles
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
        public async Task<IEnumerable<Roles>> GetRoleDetailsAsync()
        {
            return await _context
                             .Roles
                             .Find(p => true)
                             .ToListAsync();
        }
    }
}
