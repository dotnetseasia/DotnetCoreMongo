using CompanyAdmin.API.Common;
using CompanyAdmin.API.Models.Sprint.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services.Interfaces
{
    public interface ISprintService
    {
        #region Sprint
        Task<ServiceResponse> AddSprintAsync(AddSprintInputModel inputModel);
        Task<ServiceResponse> GetAllSprintAsync();
        Task<ServiceResponse> GetSprintByIdAsync(string id); 
         Task<ServiceResponse> DeleteSprintByIdAsync(string id);
        Task<ServiceResponse> UpdateSprintAsync(EditSprintInputModel inputModel);
        #endregion
    }
}
