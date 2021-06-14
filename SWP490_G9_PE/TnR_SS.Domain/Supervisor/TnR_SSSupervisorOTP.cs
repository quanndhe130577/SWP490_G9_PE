using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task<bool> CheckOTPDoneAsync(int otpId, string phoneNumber)
        {
            //var otpInfor = await _dbContext.OTPs.FindAsync(otpId);
            var otpInfor = await _unitOfWork.OTPs.FindAsync(otpId);
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
            var otpInfor = await _unitOfWork.OTPs.FindAsync(otpId);
            if (otpInfor is null)
            {
                return false;
            }

            if (otpInfor.PhoneNumber == phoneNumber && otpInfor.Code == otp && otpInfor.Status.Equals(OTPStatus.Waiting.ToString()) && otpInfor.ExpiredDate > DateTime.Now)
            {
                await _unitOfWork.OTPs.UpdateStatusAsync(otpId);
                await _unitOfWork.SaveChangeAsync();
                return true;
            }

            return false;
        }

        public bool CheckPhoneOTPExists(string phoneNumber)
        {
            List<OTP> otps = _unitOfWork.OTPs.GetByPhoneNumber(phoneNumber);
            var rs = otps.FirstOrDefault(x => x.ExpiredDate >= DateTime.Now);
            return rs is not null;
        }

        public async Task<int> AddOTPAsync(string code, string phoneNumber)
        {
            OTP otp = new()
            {
                Code = code,
                PhoneNumber = phoneNumber,
                ExpiredDate = DateTime.Now.AddMinutes(1),
                Status = OTPStatus.Waiting.ToString()
            };

            await _unitOfWork.OTPs.CreateAsync(otp);
            await _unitOfWork.SaveChangeAsync();
            {
                throw new Exception("Lỗi thêm Otp");
            }
            return otp.ID;
        }
    }
}
