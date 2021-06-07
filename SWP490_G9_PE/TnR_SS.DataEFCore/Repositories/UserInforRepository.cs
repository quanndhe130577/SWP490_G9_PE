using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.IRepositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class UserInforRepository : IUserInforRepository
    {
        private readonly TnR_SSContext _context;
        private readonly UserManager<UserInfor> _userManager;
        private readonly SignInManager<UserInfor> _signInManager;

        public UserInforRepository(TnR_SSContext context, UserManager<UserInfor> userManager, SignInManager<UserInfor> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(UserInfor user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> ChangePasswordAsync(UserInfor user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<bool> CheckPasswordAsync(UserInfor user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateAsync(UserInfor user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> DeleteAsync(UserInfor user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
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
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task SignInAsync(UserInfor user)
        {
            await _signInManager.SignInAsync(user, false);
        }

        public Task<SignInResult> SignInWithPasswordAsync(UserInfor user, string password)
        {
            throw new NotImplementedException();
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> UpdateAsync(UserInfor user)
        {
            return await _userManager.UpdateAsync(user);
        }
    }
}
