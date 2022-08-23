using Camp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Camp.BusinessLogicLayer.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration config)
        {
            _configuration = config;
        }

        public JwtSecurityToken CreateAccessToken(string id, string name, string role)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            _ = int.TryParse(_configuration["Jwt:ExpiresInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: SetClaims(id, name, role),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public string WriteAccessToken(JwtSecurityToken token)
        {
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<Claim> SetClaims(string id, string name, string role)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", id),
                //new Claim(ClaimTypes.Name, name),
                //new Claim(ClaimTypes.Role, role)
                new Claim("name", name),
                new Claim("role", role)
            };

            return claims;
        }
    }
}
