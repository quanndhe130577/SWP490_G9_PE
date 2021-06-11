using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.AccountModel.RequestModel;
using TnR_SS.Domain.ApiModels.AccountModel.ResponseModel;
using TnR_SS.Domain.ApiModels.RoleUserModel.RequestModel;
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
        Task<int> SendOTPByStringee(string token, string phoneNumber);

        //role
        Task<bool> RoleExistsAsync(string roleName);
        Task<string> GetRoleDisplayNameAsync(UserInfor user);
        List<AllRoleResModel> GetAllResRoles();
        Task<IdentityResult> AddRoleUserAsync(RoleUser role);

        //user
        Task<IdentityResult> CreateAsync(RegisterUserReqModel userData, string imgurClientId);
        Task<IdentityResult> AddToRoleAsync(UserInfor user, string role);
        Task<IdentityResult> DeleteAsync(UserInfor user);
        UserInfor GetUserByPhoneNumber(string phoneNumber);
        Task SignOutAsync();
        Task<UserResModel> SignInWithPasswordAsync(UserInfor user, string password);
        Task SignInAsync(UserInfor user);
        UserInfor GetUserById(int id);
        Task<IdentityResult> UpdateUserAsync(UpdateUserReqModel user, int id, string imgurClientId);
        Task<IdentityResult> UpdatePhoneNumberAsync(int id, string newPhone);
        Task<IdentityResult> ChangeUserPasswordAsync(UserInfor user, string currentPassword, string newPassword);
        Task<IdentityResult> ResetUserPasswordAsync(UserInfor user, string token, string newPassword);
        Task<IList<string>> GetUserRolesAsync(UserInfor user);
        bool CheckUserPhoneExists(string phoneNumber);
        Task<string> GetPasswordResetTokenAsync(UserInfor user);
        Task<bool> CheckUserPassword(UserInfor user, string password);
        Task<UserResModel> GetUserResModelByIdAsync(int id);

        #region Rổ
        bool CheckRoExist(string typeRo);
        Task<bool> CreateRo(string typeRo, int weight);
        Task<bool> UpdateRo(Ro ro, string typeRo, int weight);
        #endregion
    }
}
