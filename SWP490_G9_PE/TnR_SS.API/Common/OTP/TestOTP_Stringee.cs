using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.API.Common.OTP
{
    public static class TestOTP_Stringee
    {
        public static string GetStringeeToken()
        {
            var iss = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.StaticConfig["JwtStringee:iss"]));

            var apiKeySecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.StaticConfig["JwtStringee:apiKeySecret"])); //key
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(apiKeySecret, SecurityAlgorithms.HmacSha256Signature);// ma hoa key

            var jti = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.StaticConfig["JwtStringee:apiKeySecret"] + "-" + DateTime.Now));

            /*JwtPayload obj_payload = new JwtPayload
            {
                {"jti", jti },
                {"iss", iss },
                {"exp", DateTime.Now.AddMinutes(2) },
                {"rest_api", true }
            };

            JwtHeader obj_header = new JwtHeader(credentials) {
                {"typ", "JWT" },
                {"alg", "HS256" },
                {"cty", "stringee-api;v=1" }
            };*/
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

        public static async Task<string> SendRequestAsync()
        {

            string token = GetStringeeToken();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-STRINGEE-AUTH", token);

            var r = new object[1];
            r[0] = new
            {
                from = "TnR",
                to = "84966848112",
                text = "Your OTP is 123456"
            };

            var obj = new
            {
                sms = r
            };

            var response = await client.PostAsync("https://api.stringee.com/v1/sms", new StringContent(obj.ToString(), Encoding.UTF8, "application/json"));
            var rs = await response.Content.ReadAsStringAsync();

            return rs;
        }
    }
}
