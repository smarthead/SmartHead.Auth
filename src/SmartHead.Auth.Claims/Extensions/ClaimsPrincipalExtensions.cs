using System;
using System.Security.Claims;

namespace SmartHead.Auth.Claims.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static long GetUserId(this ClaimsPrincipal user)
        {
            var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
                
            return long.TryParse(userIdString, out var userId)
                ? userId
                : throw new InvalidOperationException(nameof(userId));
        }
    }
}