using CompanyAdmin.API.Common.DI;
using CompanyAdmin.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Repositories.Interfaces
{
    public interface ISprintRepository : IAsSelf
    {
        #region Sprint
        Task<string> AddSprintAsync(colSprint ColSprint);
        Task<IEnumerable<colSprint>> GetAllSprintAsync();
        Task<bool> DeleteSprintByIdAsync(string id);
        Task<string> UpdateSprintAsync(colSprint ColSprint);
        Task<colSprint> GetSprintByIdAsync(string Id); 
        Task<colSprint> GetSprintBySprintName(string SprintName);
       
        #endregion
    }
}
