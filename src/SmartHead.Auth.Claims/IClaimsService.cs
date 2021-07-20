using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SmartHead.Auth.Claims
{
    public interface IClaimsService<TUser> 
        where TUser : IdentityUser<long>
    {
        Task<ClaimsIdentity> CreateUserClaimsAsync(TUser user, Func<string, bool> rolesFilter = null);
    }
}