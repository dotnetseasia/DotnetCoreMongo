using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services.Interfaces
{
    public interface ICurrentUserService
    {
        int GetUserId(); 
        string[] GetRoles();
        string GetRole();  
    }
}
