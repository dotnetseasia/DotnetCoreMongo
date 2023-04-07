using CompanyAdmin.API.Common;
using CompanyAdmin.API.Common.DI;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Repositories.Interfaces
{
    public interface IRoleManagementRepository : IAsSelf
    {
        Task<colRoleMaster> SaveMasterRole(colRoleMaster objRoleMaster);
        Task<bool> DeleteRoles(string id);
        Task<string> CreateRoleDetailAsync(Roles role);
        Task<IEnumerable<Roles>> GetRoleDetailsAsync();
        Task<string> EditRoleAsync(Roles role);
        Task<Roles> GetRoleByName(string RoleName);
        public Task<Roles> ValidateDuplicateDepartmentName(string id, string Name);
        public Task<Roles> GetRoleByIdAsync(string id);
        Task<bool> DeleteRoleByIdAsync(string id);
    }
}
