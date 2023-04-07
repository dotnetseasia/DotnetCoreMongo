using CompanyAdmin.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CompanyAdmin.API.Services
{
    public class CurrentUserService: ICurrentUserService
    {
        private static IHttpContextAccessor _httpContextAccessor; 
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor; 
        } 

        public string[] GetRoles()
        {
            return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Typ)?.Value.Split("|");
        }

        public string GetRole()
        {
            return GetRoles()?.FirstOrDefault();
        }

        public int GetUserId()
        {  
            var userIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Int32.TryParse(userIdString, out int userId);  
            return userId;
        } 
    }
}
