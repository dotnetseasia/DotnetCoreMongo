using CompanyAdmin.API.Common.DI;
using CompanyAdmin.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Repositories.Interfaces
{
    public interface IProjectDetailsRepository: IAsSelf
    {
        Task<string> CreateProjectDetailAsync(ColProject colProject);
    }
}
