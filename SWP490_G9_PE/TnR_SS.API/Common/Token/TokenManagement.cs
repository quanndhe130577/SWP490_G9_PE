using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace TnR_SS.API.Common.Token
{
    public static class TokenManagement
    {

        public static string GetTokenUser(int id)
        {
            //create claims details based on the user information
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, Startup.StaticConfig["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("ID", id.ToString()),
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.StaticConfig["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(Startup.StaticConfig["Jwt:Issuer"], Startup.StaticConfig["Jwt:Audience"], claims, expires: DateTime.Now.AddHours(1), signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public static bool ValidateToken(HttpContext _context, int id)
        {
            var currentUser = _context.User;

            if (currentUser.HasClaim(c => c.Type == "ID"))
            {
                int claimdId = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "ID").Value);
                if (claimdId == id)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CheckUserIdFromToken(HttpContext _context, int userId)
        {
            var currentUser = _context.User;

            if (currentUser.HasClaim(c => c.Type == "ID"))
            {
                int claimdId = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "ID").Value);
                if (userId == claimdId) return true; 
            }

            return false;
        }
    }
}
