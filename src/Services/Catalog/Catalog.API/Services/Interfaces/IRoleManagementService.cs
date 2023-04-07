using CompanyAdmin.API.Common;
using CompanyAdmin.API.Common.DI;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services.Interfaces
{
    public interface IRoleManagementService : IAsSelf
    {
        Task<colRoleMaster> SaveMasterRole(colRoleMaster objRoleMaster);

        #region RoleServiceinteface
        Task<ServiceResponse> GetRoleDetailsAsync();
        Task<ServiceResponse> CreateRoleDetailAsync(RoleDetailsAddModel inputModel);
        Task<ServiceResponse> EditRoleAsync(RoleDetailsEditModel inputModel);
        Task<bool> DeleteRoleByIdAsync(string id);
        Task<ServiceResponse> GetRoleByIdAsync(string id);
        #endregion

    }
}
