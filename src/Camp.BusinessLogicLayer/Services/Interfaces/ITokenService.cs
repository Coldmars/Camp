using System.IdentityModel.Tokens.Jwt;

namespace Camp.BusinessLogicLayer.Services.Interfaces
{
    public interface ITokenService
    {
        public JwtSecurityToken CreateAccessToken(string id, string name, string role);

        public string WriteAccessToken(JwtSecurityToken token);
    }
}
