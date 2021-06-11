using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.API.Common.StringeeAPI
{
    public class StringeeAPI
    {
        private static string GetStringeeToken()
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

        public static async Task<string> SendOtpRequestAsync(string phoneNumber)
        {
            string token = GetStringeeToken();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-STRINGEE-AUTH", token);
            Random rd = new Random();
            string otpCode = rd.Next(1, 999999).ToString("D6");
            StringeeReqModel smsModel = new StringeeReqModel();
            SMSContentReqModel sms = new SMSContentReqModel()
            {
                From = "TnR",
                //To = HandleOTP.ModifyPhoneNumber(phoneNumber),
                To = phoneNumber,
                Text = "Your OTP is " + otpCode
            };
            smsModel.SMS = sms;
            var response = await client.PostAsync("https://api.stringee.com/v1/sms", new StringContent(JsonConvert.SerializeObject(smsModel), Encoding.UTF8, "application/json"));
            var rs = await response.Content.ReadAsStringAsync();
            StringeeResModel stringeeResModel = JsonConvert.DeserializeObject<StringeeResModel>(rs);
            if (stringeeResModel.SMSSent == "0")
            {
                return null;
            }

            return otpCode;
        }
    }
}
