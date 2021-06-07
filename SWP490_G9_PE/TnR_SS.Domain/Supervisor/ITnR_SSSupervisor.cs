using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public interface ITnR_SSSupervisor
    {
        // OTP
        Task<bool> CheckOTPDoneAsync(int otpId, string phoneNumber);
        Task<bool> CheckOTPRightAsync(int otpId, string otp, string phoneNumber);
        bool CheckPhoneOTPExists(string phoneNumber);
        Task AddOTPAsync(OTP otp);

        //role
        Task<bool> RoleExistsAsync(string roleName);
        Task<string> GetRoleDisplayName(UserInfor user);
        List<RoleUser> GetAllRoleUser();
        Task<IdentityResult> AddRoleUserAsync(RoleUser role);
        //user
        Task<IdentityResult> CreateAsync(UserInfor user, string password);
        Task<IdentityResult> AddToRoleAsync(UserInfor user, string role);
        Task<IdentityResult> DeleteAsync(UserInfor user);
        UserInfor GetUserByPhoneNumber(string phoneNumber);
        Task SignOutAsync();
        Task<SignInResult> SignInWithPasswordAsync(UserInfor user, string password);
        Task SignInAsync(UserInfor user);
        UserInfor GetUserById(int id);
        Task<IdentityResult> UpdateUserAsync(UserInfor user);
        Task<IdentityResult> ChangeUserPasswordAsync(UserInfor user, string currentPassword, string newPassword);
        Task<IdentityResult> ResetUserPasswordAsync(UserInfor user, string token, string newPassword);
        Task<IList<string>> GetUserRolesAsync(UserInfor user);
        bool CheckUserPhoneExists(string phoneNumber);
        Task<string> GetPasswordResetTokenAsync(UserInfor user);
        Task<bool> CheckUserPassword(UserInfor user, string password);
    }
}
