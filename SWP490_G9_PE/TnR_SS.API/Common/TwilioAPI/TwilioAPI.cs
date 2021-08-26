using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace TnR_SS.API.Common.TwilioAPI
{
    public class TwilioAPI
    {
        public static string SendOtpRequest(string phoneNumber)
        {
            string accountSid = Startup.StaticConfig["Twilio:accountSid"];
            string authToken = Startup.StaticConfig["Twilio:authToken"];
            string fromPhone = Startup.StaticConfig["Twilio:fromPhone"];

            // Initialize the Twilio client
            TwilioClient.Init(accountSid, authToken);

            Random rd = new Random();
            string otpCode = rd.Next(1, 999999).ToString("D6");

            var message = MessageResource.Create(
                body: "Hi, I'm QuanND from SWP490_G9. Your OTP is " + otpCode,
                from: new Twilio.Types.PhoneNumber(fromPhone),
                to: new Twilio.Types.PhoneNumber(ModifyPhoneNumber(phoneNumber))
            );

            if (message.Status.ToString() == "queued")
            {
                return otpCode;
            }
            else
            {
                throw new Exception(message.ErrorMessage);
            }
        }

        private static string ModifyPhoneNumber(string phoneNumber)
        {
            if (phoneNumber[0] == '0')
            {
                phoneNumber = "+84" + phoneNumber.Substring(1);
            }
            else
            {
                phoneNumber = string.Concat("+", phoneNumber);
            }
            return phoneNumber;
        }
    }
}
