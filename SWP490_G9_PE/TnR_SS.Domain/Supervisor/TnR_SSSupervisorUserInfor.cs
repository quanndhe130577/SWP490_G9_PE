using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.AccountModel.RequestModel;
using TnR_SS.Domain.ApiModels.AccountModel.ResponseModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task<IdentityResult> CreateUserAsync(RegisterUserReqModel userData, string avatarLink)
        {
            var userInfor = _mapper.Map<RegisterUserReqModel, UserInfor>(userData);
            userInfor.Avatar = avatarLink;
            var result = await _unitOfWork.UserInfors.CreateWithPasswordAsync(userInfor, userData.Password);
            if (result.Succeeded)
            {
                //add role to user
                var result_addUserToRole = await _unitOfWork.UserInfors.AddToRoleAsync(userInfor, userData.RoleNormalizedName);
                if (!result_addUserToRole.Succeeded)
                {
                    await _unitOfWork.UserInfors.DeleteIdentityAsync(userInfor);
                }

                return result_addUserToRole;
            }

            return result;
        }

        public async Task<IdentityResult> AddToRoleAsync(UserInfor user, string role)
        {
            return await _unitOfWork.UserInfors.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> DeleteUserAsync(UserInfor user)
        {
            return await _unitOfWork.UserInfors.DeleteIdentityAsync(user);
        }

        public UserInfor GetUserByPhoneNumber(string phoneNumber)
        {
            return _unitOfWork.UserInfors.GetUserByPhoneNumber(phoneNumber);
        }

        public async Task SignOutAsync()
        {
            await _unitOfWork.UserInfors.SignOutAsync();
        }

        public async Task<UserResModel> SignInWithPasswordAsync(UserInfor user, string password)
        {
            await SignOutAsync();
            var userSigninResult = await _unitOfWork.UserInfors.PasswordSignInAsync(user, password);
            if (userSigninResult.Succeeded)
            {
                await _unitOfWork.UserInfors.SignInAsync(user);
                var userResModel = _mapper.Map<UserInfor, UserResModel>(user);
                userResModel.RoleDisplayName = await GetRoleDisplayNameAsync(user);
                return userResModel;
            }

            return null;
        }

        public async Task SignInAsync(UserInfor user)
        {
            await _unitOfWork.UserInfors.SignInAsync(user);
        }

        public UserResModel GetUserById(int id)
        {
            var user = _unitOfWork.UserInfors.GetUserById(id);
            return _mapper.Map<UserInfor, UserResModel>(user);
        }

        public async Task<IdentityResult> UpdateUserAsync(UpdateUserReqModel user, int id, string avatarLink)
        {
            var userInfor = _unitOfWork.UserInfors.GetUserById(id);
            userInfor = _mapper.Map<UpdateUserReqModel, UserInfor>(user, userInfor);
            userInfor.Avatar = avatarLink;
            return await _unitOfWork.UserInfors.UpdateIdentityAsync(userInfor);
        }

        public async Task<UserResModel> GetUserResModelByIdAsync(int id)
        {
            var userInfor = _unitOfWork.UserInfors.GetUserById(id);
            var userResModel = _mapper.Map<UserInfor, UserResModel>(userInfor);
            userResModel.RoleDisplayName = await GetRoleDisplayNameAsync(userInfor);

            return userResModel;
        }

        public async Task<IdentityResult> ChangeUserPasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var userInfor = await _unitOfWork.UserInfors.FindAsync(userId);
            return await _unitOfWork.UserInfors.ChangePasswordAsync(userInfor, currentPassword, newPassword);
        }

        public async Task<IdentityResult> ResetUserPasswordAsync(UserInfor user, string token, string newPassword)
        {
            return await _unitOfWork.UserInfors.ResetUserPasswordAsync(user, token, newPassword);
        }

        public async Task<IList<string>> GetUserRolesAsync(UserInfor user)
        {
            return await _unitOfWork.UserInfors.GetRolesAsync(user);
        }
        public bool CheckUserPhoneExists(string phoneNumber)
        {
            var user = _unitOfWork.UserInfors.GetUserByPhoneNumber(phoneNumber);
            return user is not null;
        }

        public async Task<string> GetPasswordResetTokenAsync(UserInfor user)
        {
            return await _unitOfWork.UserInfors.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<bool> CheckUserPassword(int userId, string password)
        {
            var userInfor = await _unitOfWork.UserInfors.FindAsync(userId);
            var rs = await _unitOfWork.UserInfors.CheckPasswordAsync(userInfor, password);
            return rs;
        }

        public async Task<IdentityResult> UpdatePhoneNumberAsync(int id, string newPhone)
        {
            var user = _unitOfWork.UserInfors.GetUserById(id);
            user.PhoneNumber = newPhone;

            return await _unitOfWork.UserInfors.UpdateIdentityAsync(user);
        }
    }
}
