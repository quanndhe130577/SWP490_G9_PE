using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

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

            if (otpInfor.PhoneNumber == phoneNumber && otpInfor.Status == OTPStatus.Done.ToString())
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
    }
}
