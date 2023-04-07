using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services.Interfaces
{
    public interface IApplicationConfigurationService
    {
        void Set(ConcurrentDictionary<string, object> applicationConfigurations);
        object GetByKey(string key);
    }
}
