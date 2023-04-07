using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services.Interfaces
{
    public interface IEmailTemplateService
    {
        void Set(ConcurrentDictionary<string, string> templateIds);
        string GetByKey(string key);
    }
}
