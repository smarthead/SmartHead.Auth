using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using SmartHead.Auth.JWT.Options;

namespace SmartHead.Auth.JWT
{
    public class JwtTokenProvider
    {
        private readonly IOptions<JwtOptions> _options;

        public JwtTokenProvider(IOptions<JwtOptions> options)
        {
            _options = options;
        }
        
        public string GetToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                _options.Value.Issuer,
                _options.Value.Audience,
                identity.Claims,
                now,
                now.Add(TimeSpan.FromMinutes(_options.Value.Lifetime)),
                new SigningCredentials(_options.Value.JwtKey, SecurityAlgorithms.HmacSha256Signature));
            
            return new JwtSecurityTokenHandler()
                .WriteToken(jwt);
        }

        public int GetTokenLifeTime()
            => _options.Value.Lifetime;
    }
}