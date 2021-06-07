using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.API.Areas.OTPManagement.Model.RequestModel;
using TnR_SS.API.Areas.OTPManagement.Model.ResponseModel;
using TnR_SS.Entity.Models;

namespace TnR_SS.API.Areas.OTPManagement
{
    public static class TestOTP_Stringee
    {
        public static string GetStringeeToken()
        {
            //var iss = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.StaticConfig["JwtStringee:iss"]));

            var apiKeySecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Startup.StaticConfig["JwtStringee:apiKeySecret"])); //key
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(apiKeySecret, SecurityAlgorithms.HmacSha256Signature);// ma hoa key

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

        public static async Task<int> SendRequestAsync(string phoneNumber)
        {

            string token = GetStringeeToken();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-STRINGEE-AUTH", token);

            // generate OTP
            string OTP = GenerateOTP(phoneNumber);
            if (OTP == "0")
            {
                return 0;
            }

            StringeeReqModel smsModel = new StringeeReqModel();
            SMSContentReqModel sms = new SMSContentReqModel()
            {
                From = "TnR",
                To = ModifyPhoneNumber(phoneNumber),
                Text = "Your OTP is " + OTP
            };


            var response = await client.PostAsync("https://api.stringee.com/v1/sms", new StringContent(JsonConvert.SerializeObject(sms), Encoding.UTF8, "application/json"));
            var rs = await response.Content.ReadAsStringAsync();
            StringeeResModel stringeeResModel = JsonConvert.DeserializeObject<StringeeResModel>(rs);
            // if rs == true => luu OTP vao DB
            if (stringeeResModel.SMSSent == "0") return 0;

            // return OTPID
            return await SaveOTP(phoneNumber, OTP);
        }

        private static string GenerateOTP(string phoneNumber)
        {
            Random rd = new Random();
            string n = rd.Next(1, 999999).ToString("######");
            using (TnR_SSContext db = new TnR_SSContext())
            {
                var rs = db.OTPs.Where(x => x.PhoneNumber == phoneNumber && x.ExpiredDate < DateTime.Now).FirstOrDefault();
                if (rs is null)
                {
                    return n;
                }
                return "0";
            }
        }

        private static async Task<int> SaveOTP(string phoneNumber, string code)
        {
            using (TnR_SSContext db = new TnR_SSContext())
            {
                OTP otp = new OTP()
                {
                    Code = code,
                    PhoneNumber = phoneNumber,
                    ExpiredDate = DateTime.Now.AddMinutes(1),
                    Status = OTPStatus.Waiting.ToString()
                };

                await db.OTPs.AddAsync(otp);
                await db.SaveChangesAsync();

                return otp.ID;
            }
        }

        private static string ModifyPhoneNumber(string phoneNumber)
        {
            return phoneNumber;
        }
    }
}
