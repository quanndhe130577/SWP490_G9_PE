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
using TnR_SS.API.Common.HandleOTP.Model;
using TnR_SS.Entity.Models;

namespace TnR_SS.API.Common.HandleOTP
{
    public class HandleOTP
    {
        private readonly TnR_SSContext _dbContext;

        public HandleOTP(TnR_SSContext context)
        {
            _dbContext = context;
        }

        public async Task<bool> CheckOTPDoneAsync(int otpId, string phoneNumber)
        {
            var otpInfor = await _dbContext.OTPs.FindAsync(otpId);
            if (otpInfor is null)
            {
                return false;
            }

            if (otpInfor.PhoneNumber == phoneNumber && otpInfor.Status == OTPStatus.Done.ToString())
            {
                return true;
            }

            return false;
        }

        public async Task<bool> CheckOTPRightAsync(int otpId, string otp, string phoneNumber)
        {
            var otpInfor = await _dbContext.OTPs.FindAsync(otpId);
            if (otpInfor is null)
            {
                return false;
            }

            if (otpInfor.PhoneNumber == phoneNumber && otpInfor.Code == otp)
            {
                return true;
            }

            return false;
        }

        public async Task<int> SendOTPByStringeeAsync(string phoneNumber)
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
            smsModel.SMS = sms;

            var response = await client.PostAsync("https://api.stringee.com/v1/sms", new StringContent(JsonConvert.SerializeObject(smsModel), Encoding.UTF8, "application/json"));
            var rs = await response.Content.ReadAsStringAsync();
            StringeeResModel stringeeResModel = JsonConvert.DeserializeObject<StringeeResModel>(rs);
            if (stringeeResModel.SMSSent == "0") return 0;

            return await SaveOTPAsync(phoneNumber, OTP);
        }

        private string GenerateOTP(string phoneNumber)
        {
            Random rd = new Random();
            string n = rd.Next(1, 999999).ToString("D6");
            var rs = _dbContext.OTPs.Where(x => x.PhoneNumber == phoneNumber && x.ExpiredDate < DateTime.Now).FirstOrDefault();
            if (rs is null)
            {
                return n;
            }
            return "0";
        }

        private static string GetStringeeToken()
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

        private async Task<int> SaveOTPAsync(string phoneNumber, string code)
        {
            OTP otp = new OTP()
            {
                Code = code,
                PhoneNumber = phoneNumber,
                ExpiredDate = DateTime.Now.AddMinutes(1),
                Status = OTPStatus.Waiting.ToString()
            };

            await _dbContext.OTPs.AddAsync(otp);
            await _dbContext.SaveChangesAsync();

            return otp.ID;
        }

        private static string ModifyPhoneNumber(string phoneNumber)
        {
            return phoneNumber;
        }
    }
}
