using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.StringeeModel;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {

        public async Task<bool> CheckOTPDoneAsync(int otpId, string phoneNumber)
        {
            //var otpInfor = await _dbContext.OTPs.FindAsync(otpId);
            var otpInfor = await _otpRepository.FindByIdAsync(otpId);
            if (otpInfor is null)
            {
                return false;
            }

            if (otpInfor.PhoneNumber == phoneNumber && otpInfor.Status == OTPStatus.Done.ToString() && otpInfor.ExpiredDate > DateTime.Now)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> CheckOTPRightAsync(int otpId, string otp, string phoneNumber)
        {
            var otpInfor = await _otpRepository.FindByIdAsync(otpId);
            if (otpInfor is null)
            {
                return false;
            }

            if (otpInfor.PhoneNumber == phoneNumber && otpInfor.Code == otp && otpInfor.Status.Equals(OTPStatus.Waiting.ToString()) && otpInfor.ExpiredDate > DateTime.Now)
            {
                await _otpRepository.UpdateStatusAsync(otpId);
                return true;
            }

            return false;
        }

        public bool CheckPhoneOTPExists(string phoneNumber)
        {
            List<OTP> otps = _otpRepository.GetByPhoneNumber(phoneNumber);
            var rs = otps.FirstOrDefault(x => x.ExpiredDate >= DateTime.Now);
            return rs is not null;
        }

        public async Task AddOTPAsync(OTP otp)
        {
            await _otpRepository.AddAsync(otp);
        }

        public async Task<int> SendOTPByStringee(string token, string phoneNumber)
        {
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
                return 0;
            }

            OTP otp = new()
            {
                Code = otpCode,
                PhoneNumber = phoneNumber,
                ExpiredDate = DateTime.Now.AddMinutes(1),
                Status = OTPStatus.Waiting.ToString()
            };

            await AddOTPAsync(otp);

            return otp.ID;
        }
    }
}
