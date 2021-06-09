using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.AccountModel.RequestModel;
using TnR_SS.Domain.ApiModels.AccountModel.ResponseModel;
using TnR_SS.Domain.Common;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task<IdentityResult> CreateAsync(RegisterUserReqModel userData, string imgurClientId)
        {
            var userInfor = _mapper.Map<RegisterUserReqModel, UserInfor>(userData);
            userInfor.Avatar = HandleImgurAPI.UploadImgurAsync(userData.AvatarBase64, imgurClientId);
            var result = await _userInforRepository.CreateAsync(userInfor, userData.Password);
            if (result.Succeeded)
            {
                //add role to user
                var result_addUserToRole = await _userInforRepository.AddToRoleAsync(userInfor, userData.RoleNormalizedName);
                if (!result_addUserToRole.Succeeded)
                {
                    await _userInforRepository.DeleteAsync(userInfor);
                }

                return result_addUserToRole;
            }

            return result;
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

        public async Task<UserResModel> SignInWithPasswordAsync(UserInfor user, string password)
        {
            await SignOutAsync();
            var userSigninResult = await _userInforRepository.PasswordSignInAsync(user, password);
            if (userSigninResult.Succeeded)
            {
                await _userInforRepository.SignInAsync(user);
                var userResModel = _mapper.Map<UserInfor, UserResModel>(user);
                userResModel.RoleDisplayName = await GetRoleDisplayNameAsync(user);
                return userResModel;
            }

            return null;
        }

        public async Task SignInAsync(UserInfor user)
        {
            await _userInforRepository.SignInAsync(user);
        }

        public UserInfor GetUserById(int id)
        {
            return _userInforRepository.GetUserById(id);
        }

        public async Task<IdentityResult> UpdateUserAsync(UpdateUserReqModel user, int id, string imgurClientId)
        {
            var userInfor = _userInforRepository.GetUserById(id);
            userInfor = _mapper.Map<UpdateUserReqModel, UserInfor>(user, userInfor);
            userInfor.Avatar = HandleImgurAPI.UploadImgurAsync(user.AvatarBase64, imgurClientId);
            return await _userInforRepository.UpdateAsync(userInfor);
        }

        public async Task<UserResModel> GetUserResModelByIdAsync(int id)
        {
            var userInfor = _userInforRepository.GetUserById(id);
            var userResModel = _mapper.Map<UserInfor, UserResModel>(userInfor);
            userResModel.RoleDisplayName = await GetRoleDisplayNameAsync(userInfor);

            return userResModel;
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

        public async Task<IdentityResult> UpdatePhoneNumberAsync(int id, string newPhone)
        {
            var user = _userInforRepository.GetUserById(id);
            user.PhoneNumber = newPhone;

            return await _userInforRepository.UpdateAsync(user);
        }
    }
}
