using CompanyAdmin.API.Data.Interfaces;
using CompanyAdmin.API.Entities;
using CompanyAdmin.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Repositories
{
    public class ProjectDetailsRepository: IProjectDetailsRepository
    {
        private readonly ICompanyAdminContext _context;

        public ProjectDetailsRepository(ICompanyAdminContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<string> CreateProjectDetailAsync(ColProject colProject)
        {
            await _context.ColProject.InsertOneAsync(colProject);
            return colProject.Id;
        }
    }
}
