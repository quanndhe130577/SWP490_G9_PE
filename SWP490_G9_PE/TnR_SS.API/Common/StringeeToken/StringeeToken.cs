using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TnR_SS.API.Common.HandleOTP
{
    public class StringeeToken
    {
        public static string GetStringeeToken()
        {
            //var iss = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.StaticConfig["JwtStringee:iss"]));

            var apiKeySecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.StaticConfig["JwtStringee:apiKeySecret"])); //key
            var credentials = new SigningCredentials(apiKeySecret, SecurityAlgorithms.HmacSha256Signature);// ma hoa key

            //var jti = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.StaticConfig["JwtStringee:apiKeySecret"] + "-" + DateTime.Now));

            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Jti, Startup.StaticConfig["JwtStringee:iss"] + "_" + DateTime.Now),
                    new Claim(JwtRegisteredClaimNames.Iss, Startup.StaticConfig["JwtStringee:iss"]),
                    new Claim(JwtRegisteredClaimNames.Exp, "120"),
                    new Claim("rest_api", "1"),
                   };
            var identityClaim = new ClaimsIdentity(claims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                AdditionalHeaderClaims = new Dictionary<string, object> { { "cty", "stringee-api;v=1" }, { "typ", "JWT" }, { "alg", "HS256" }, },
                Subject = identityClaim
            };

            //var secToken = new JwtSecurityToken(obj_header, obj_payload);

            return new JwtSecurityTokenHandler().CreateEncodedJwt(tokenDescriptor);
        }
    }
}
