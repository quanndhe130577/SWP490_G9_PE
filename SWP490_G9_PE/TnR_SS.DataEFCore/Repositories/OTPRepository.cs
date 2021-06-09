using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.IRepositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class OTPRepository : IOTPRepository
    {
        private readonly TnR_SSContext _context;

        public OTPRepository(TnR_SSContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OTP otp)
        {
            await _context.OTPs.AddAsync(otp);
            await _context.SaveChangesAsync();
        }

        public void Dispose() => _context.Dispose();

        public async Task<OTP> FindByIdAsync(int otpId)
        {
            return await _context.OTPs.FindAsync(otpId);
        }

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
                await _context.SaveChangesAsync();
            }

        }
    }
}
