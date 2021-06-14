using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class OTPRepository : RepositoryBase<OTP>, IOTPRepository
    {

        public OTPRepository(TnR_SSContext context) : base(context) { }

        public List<OTP> GetByPhoneNumber(string phoneNumber)
        {
            return _context.OTPs.Where(x => x.PhoneNumber == phoneNumber).ToList();
        }

        public async Task UpdateStatusAsync(int otpid)
        {
            var otp = await _context.OTPs.FindAsync(otpid);
            if (otp is not null)
            {
                otp.Status = OTPStatus.Done.ToString();
                otp.ExpiredDate = DateTime.Now.AddMinutes(30);
            }
        }
    }
}
