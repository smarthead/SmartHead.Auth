using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SmartHead.Auth.JWT.Options
{
    public class JwtOptions
    {
        [Required]
        public string Secret { get; set; }

        [Required]
        public string Audience { get; set; }
        
        [Required]
        public string Issuer { get; set; }
        
        public int Lifetime { get; set; }
        
        public SymmetricSecurityKey JwtKey => new(Encoding.ASCII.GetBytes(Secret));
        
        public const string CookieName = ".AspNetCore.Application.Id";
    }
}