﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.IRepositories
{
    public interface IUserInforRepository : IDisposable
    {
        Task<IdentityResult> CreateAsync(UserInfor user, string password);
        Task<IdentityResult> AddToRoleAsync(UserInfor user, string role);
        Task<IdentityResult> DeleteAsync(UserInfor user);
        UserInfor GetUserByPhoneNumber(string phoneNumber);
        Task SignOutAsync();
        Task<SignInResult> PasswordSignInAsync(UserInfor user, string password);
        Task SignInAsync(UserInfor user);
        UserInfor GetUserById(int id);
        Task<IdentityResult> UpdateAsync(UserInfor user);
        Task<IdentityResult> ChangePasswordAsync(UserInfor user, string currentPassword, string newPassword);
        Task<IdentityResult> ResetUserPasswordAsync(UserInfor user, string token, string newPassword);
        Task<IList<string>> GetRolesAsync(UserInfor user);
        Task<string> GeneratePasswordResetTokenAsync(UserInfor user);
        Task<bool> CheckPasswordAsync(UserInfor user, string password);
    }
}
