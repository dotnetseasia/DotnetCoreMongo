using CompanyAdmin.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Repositories.Interfaces
{
    public interface IApplicationConfigurationRepository
    {
        ColApplicaitonConfiguration GetApplicationConfigurations();
    }
}
