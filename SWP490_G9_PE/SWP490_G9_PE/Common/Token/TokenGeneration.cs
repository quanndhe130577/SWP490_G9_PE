using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SWP490_G9_PE.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SWP490_G9_PE.Common.Token
{
    public static class TokenGeneration
    {

        public static string GetToken(UserInfo _userData)
        {
            //create claims details based on the user information
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, Startup.StaticConfig["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", _userData.UserId.ToString()),
                    new Claim("FirstName", _userData.FirstName),
                    new Claim("LastName", _userData.LastName),
                    new Claim("UserName", _userData.UserName),
                    new Claim("Email", _userData.Email)
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.StaticConfig["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(Startup.StaticConfig["Jwt:Issuer"], Startup.StaticConfig["Jwt:Audience"], claims, expires: DateTime.Now, signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
