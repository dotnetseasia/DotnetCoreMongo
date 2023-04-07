using CompanyAdmin.API.Services.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private ConcurrentDictionary<string, string> _templateIds;

        public void Set(ConcurrentDictionary<string, string> templateIds)
        {
            _templateIds = templateIds;
        }

        public string GetByKey(string key)
        {
            var result = _templateIds.TryGetValue(key, out string value);
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
