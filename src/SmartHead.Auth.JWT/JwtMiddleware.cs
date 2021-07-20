using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SmartHead.Auth.JWT
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Cookies[".AspNetCore.Application.Id"];

            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers.Remove("Authorization");
                context.Request.Headers.Add("Authorization", "Bearer " + token);
            }

            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Xss-Protection", "1");
            context.Response.Headers.Add("X-Frame-Options", "DENY");

            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}