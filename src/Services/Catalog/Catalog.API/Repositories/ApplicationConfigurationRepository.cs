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
    public class ApplicationConfigurationRepository : IApplicationConfigurationRepository
    {
        private readonly ICompanyAdminContext _context;

        public ApplicationConfigurationRepository(ICompanyAdminContext context)
        {
            _context = context;
        }

        public ColApplicaitonConfiguration GetApplicationConfigurations()
        {
            return _context.ColApplicaitonConfiguration
                .Find(p => true)
                .FirstOrDefault();
        }
    }
}
