using CompanyAdmin.API.Common;
using CompanyAdmin.API.Common.DI;
using CompanyAdmin.API.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services.Interfaces
{
    public interface IProjectDetailsService: IAsSelf
    {
        Task<ServiceResponse> CreateProjectDetailAsync(ColProjectAddModel colProject);
    }
}
