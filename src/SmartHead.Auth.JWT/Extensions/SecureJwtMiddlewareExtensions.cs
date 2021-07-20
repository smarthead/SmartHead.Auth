using Microsoft.AspNetCore.Builder;

namespace SmartHead.Auth.JWT.Extensions
{
    public static class SecureJwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecureJwt(this IApplicationBuilder builder) 
            => builder.UseMiddleware<JwtMiddleware>();
    }
}