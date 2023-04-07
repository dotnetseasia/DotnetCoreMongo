using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services.Interfaces
{
    public class ApplicationConfigurationService : IApplicationConfigurationService
    {
        private ConcurrentDictionary<string, object> _applicationConfigurations;

        public void Set(ConcurrentDictionary<string, object> applicationConfigurations)
        {
            _applicationConfigurations = applicationConfigurations;
        }

        public object GetByKey(string key)
        {
            var result = _applicationConfigurations.TryGetValue(key, out object value);
            if (result)
            {
                return value;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
