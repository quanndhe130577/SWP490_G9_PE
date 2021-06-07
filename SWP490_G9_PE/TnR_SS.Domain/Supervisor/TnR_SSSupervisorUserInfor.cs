using Microsoft.AspNetCore.Identity;
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
        public async Task<IdentityResult> CreateAsync(UserInfor user, string password)
        {
            return await _userInforRepository.CreateAsync(user, password);
        }
        public async Task<IdentityResult> AddToRoleAsync(UserInfor user, string role)
        {
            return await _userInforRepository.AddToRoleAsync(user, role);
        }
        public async Task<IdentityResult> DeleteAsync(UserInfor user)
        {
            return await _userInforRepository.DeleteAsync(user);
        }

        public UserInfor GetUserByPhoneNumber(string phoneNumber)
        {
            return _userInforRepository.GetUserByPhoneNumber(phoneNumber);
        }

        public async Task SignOutAsync()
        {
            await _userInforRepository.SignOutAsync();
        }

        public async Task<SignInResult> SignInWithPasswordAsync(UserInfor user, string password)
        {
            return await _userInforRepository.SignInWithPasswordAsync(user, password);
        }
        public async Task SignInAsync(UserInfor user)
        {
            await _userInforRepository.SignInAsync(user);
        }

        public UserInfor GetUserById(int id)
        {
            return _userInforRepository.GetUserById(id);
        }

        public async Task<IdentityResult> UpdateUserAsync(UserInfor user)
        {
            return await _userInforRepository.UpdateAsync(user);
        }
        public async Task<IdentityResult> ChangeUserPasswordAsync(UserInfor user, string currentPassword, string newPassword)
        {
            return await _userInforRepository.ChangePasswordAsync(user, currentPassword, newPassword);
        }
        public async Task<IdentityResult> ResetUserPasswordAsync(UserInfor user, string token, string newPassword)
        {
            return await _userInforRepository.ResetUserPasswordAsync(user, token, newPassword);
        }
        public async Task<IList<string>> GetUserRolesAsync(UserInfor user)
        {
            return await _userInforRepository.GetRolesAsync(user);
        }
        public bool CheckUserPhoneExists(string phoneNumber)
        {
            var user = _userInforRepository.GetUserByPhoneNumber(phoneNumber);
            return user is not null;
        }

        public async Task<string> GetPasswordResetTokenAsync(UserInfor user)
        {
            return await _userInforRepository.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<bool> CheckUserPassword(UserInfor user, string password)
        {
            var rs = await _userInforRepository.CheckPasswordAsync(user, password);
            return rs;
        }
    }
}
