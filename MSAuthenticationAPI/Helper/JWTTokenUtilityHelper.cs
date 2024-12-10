using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MSAuthenticationAPI.Model;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MSAuthenticationAPI.Helper
{
    public static class JWTTokenUtilityHelper
    {

        public static string GenerateJwtToken(ApplicationUser user, JWTSettings jWTSettings)
        {
            var claims = new List<Claim>
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jWTtoken = new JwtSecurityToken
            (
                issuer: jWTSettings.Issuer,
                audience: jWTSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(jWTSettings.DurationInMinutes),
                signingCredentials: creds
            );  

            return new JwtSecurityTokenHandler().WriteToken(jWTtoken);

        }
    }
}
