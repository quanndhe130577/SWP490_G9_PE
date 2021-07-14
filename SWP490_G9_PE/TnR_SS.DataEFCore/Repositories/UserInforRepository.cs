using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class UserInforRepository : RepositoryBase<UserInfor>, IUserInforRepository
    {
        private readonly UserManager<UserInfor> _userManager;
        private readonly SignInManager<UserInfor> _signInManager;

        public UserInforRepository(TnR_SSContext context, UserManager<UserInfor> userManager, SignInManager<UserInfor> signInManager) : base(context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(UserInfor user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> ChangePasswordAsync(UserInfor user, string currentPassword, string newPassword)
        {
            user.UpdatedAt = DateTime.Now;
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<bool> CheckPasswordAsync(UserInfor user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateWithPasswordAsync(UserInfor user, string password)
        {
            user.CreatedAt = DateTime.Now;
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(UserInfor user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IList<string>> GetRolesAsync(UserInfor user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public UserInfor GetUserById(int id)
        {
            return _userManager.Users.FirstOrDefault(x => x.Id == id);
        }

        public UserInfor GetUserByPhoneNumber(string phoneNumber)
        {
            return _userManager.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
        }

        public async Task<SignInResult> PasswordSignInAsync(UserInfor user, string password)
        {
            return await _signInManager.PasswordSignInAsync(user, password, true, false);
        }

        public async Task<IdentityResult> ResetUserPasswordAsync(UserInfor user, string token, string newPassword)
        {
            user.UpdatedAt = DateTime.Now;
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task SignInAsync(UserInfor user)
        {
            await _signInManager.SignInAsync(user, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> UpdateIdentityAsync(UserInfor user)
        {
            user.UpdatedAt = DateTime.Now;
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteIdentityAsync(UserInfor userInfor)
        {
            return await _userManager.DeleteAsync(userInfor);
        }

        public async Task<UserInfor> FindTraderByPhoneAsync(string phoneNumber)
        {
            var rs = await _userManager.GetUsersInRoleAsync(RoleName.Trader);
            return rs.Where(x => x.PhoneNumber == phoneNumber).FirstOrDefault();
        }

        public async Task<List<UserInfor>> GetUserByRoleAsync(string roleName)
        {
            return (List<UserInfor>)await _userManager.GetUsersInRoleAsync(roleName);
        }

        public async Task<List<UserInfor>> FindTradersByPhoneAsync(string phoneNumberStr)
        {
            var listTrader = await _userManager.GetUsersInRoleAsync(RoleName.Trader);
            return listTrader.Where(x => x.PhoneNumber.Contains(phoneNumberStr)).Take(10).ToList();
        }
    }
}
