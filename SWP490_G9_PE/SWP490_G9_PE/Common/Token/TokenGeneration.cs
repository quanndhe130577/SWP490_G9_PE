using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TnR_SS.API.Common.Token
{
    public static class TokenGeneration
    {

        public static string GetTokenUser(string id)
        {
            //create claims details based on the user information
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, Startup.StaticConfig["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("ID", id),
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.StaticConfig["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(Startup.StaticConfig["Jwt:Issuer"], Startup.StaticConfig["Jwt:Audience"], claims, expires: DateTime.Now, signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
