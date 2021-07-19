using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SmartHead.Auth.Claims
{
    public class ClaimsService<TUser> : IClaimsService<TUser>  
        where TUser : IdentityUser<long>
    {
        private readonly UserManager<TUser> _userManager;
        public ClaimsService(UserManager<TUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ClaimsIdentity> CreateUserClaimsAsync(TUser user, Func<string, bool> rolesFilter = null)
        {
            var roles = await _userManager.GetRolesAsync(user);

            if (rolesFilter != null)
                roles = roles
                    .Where(rolesFilter).ToArray();

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new(ClaimsIdentity.DefaultRoleClaimType, roles.FirstOrDefault() ?? string.Empty)
            };

            var claimsIdentity =
                new ClaimsIdentity(
                    claims,
                    "Token",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}