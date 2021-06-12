using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.IRepositories
{
    public interface IOTPRepository : IRepositoryBase<OTP>
    {
        //Task<OTP> FindByIdAsync(int otpId);
        List<OTP> GetByPhoneNumber(string phoneNumber);
        //Task AddAsync(OTP otp);
        Task UpdateStatusAsync(int otpid);
    }
}
