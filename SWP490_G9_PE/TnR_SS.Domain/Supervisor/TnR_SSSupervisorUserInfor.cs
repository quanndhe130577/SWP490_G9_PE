using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.AccountModel.RequestModel;
using TnR_SS.Domain.ApiModels.AccountModel.ResponseModel;
using TnR_SS.Domain.ApiModels.RoleUserModel;
using TnR_SS.Domain.ApiModels.UserInforModel;
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
                userResModel.RoleName = await GetRoleNameAsync(user);
                return userResModel;
            }

            return null;
        }

        public async Task SignInAsync(UserInfor user)
        {
            await _unitOfWork.UserInfors.SignInAsync(user);
        }

        public async Task<UserResModel> GetUserByIdAsync(int id)
        {
            var user = _unitOfWork.UserInfors.GetUserById(id);
            var userRes = _mapper.Map<UserInfor, UserResModel>(user);
            userRes.RoleName = await GetRoleNameAsync(user);
            return userRes;
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

        public async Task<List<FindTraderByPhoneApiModel>> SuggestTradersByPhoneAsync(string phoneNumber, int wcId)
        {
            if (phoneNumber != null)
            {
                var rs = await _unitOfWork.UserInfors.FindTradersByPhoneAsync(phoneNumber);
                var listTraderid = _unitOfWork.TraderOfWeightRecorders.GetAll(x => x.WeightRecorderId == wcId).Select(x => x.TraderId);
                return rs.Where(x => !listTraderid.Contains(x.Id)).Take(5).Select(x => _mapper.Map<UserInfor, FindTraderByPhoneApiModel>(x)).ToList();
            }
            else
            {
                /* var listTraderId = _unitOfWork.TraderOfWeightRecorders.GetAll(x => x.WeightRecorderId == wcId).Select(x => x.TraderId).Take(5);
                 var ListTrader = _unitOfWork.UserInfors.GetAll(x => listTraderId.Contains(x.Id));*/
                var ListTrader = await _unitOfWork.UserInfors.GetUserByRoleAsync(RoleName.Trader);
                var listTraderid = _unitOfWork.TraderOfWeightRecorders.GetAll(x => x.WeightRecorderId == wcId).Select(x => x.TraderId);
                return ListTrader.Where(x => !listTraderid.Contains(x.Id)).Take(5).Select(x => _mapper.Map<UserInfor, FindTraderByPhoneApiModel>(x)).ToList();
            }
        }

        public async Task<FindTraderByPhoneApiModel> FindTraderByPhoneAsync(string phoneNumber)
        {
            var rs = await _unitOfWork.UserInfors.FindTraderByPhoneAsync(phoneNumber);
            return _mapper.Map<UserInfor, FindTraderByPhoneApiModel>(rs);
        }

        public List<FindTraderByPhoneApiModel> FindTradersOfWeightRecorder(int weightRecorderId)
        {
            var listTraderId = _unitOfWork.TraderOfWeightRecorders.GetAll(x => x.WeightRecorderId == weightRecorderId).Select(x => x.TraderId).ToList();
            return _unitOfWork.UserInfors.GetAll(x => listTraderId.Contains(x.Id)).Select(x => _mapper.Map<UserInfor, FindTraderByPhoneApiModel>(x)).ToList();
        }

        public async Task WeightRecorderAddTrader(int traderId, int weightRecorderId)
        {
            var traderList = await _unitOfWork.UserInfors.GetUserByRoleAsync(RoleName.Trader);
            if (!traderList.Select(x => x.Id).Contains(traderId))
            {
                throw new Exception("Không tìm thấy thương lái");
            }

            var rs = _unitOfWork.TraderOfWeightRecorders.GetAll(x => x.TraderId == traderId && x.WeightRecorderId == weightRecorderId).ToList();
            if (rs.Count() != 0)
            {
                throw new Exception("Thương lái đã được thêm !!!");
            }

            await _unitOfWork.TraderOfWeightRecorders.CreateAsync(new TraderOfWeightRecorder() { TraderId = traderId, WeightRecorderId = weightRecorderId });
            await _unitOfWork.SaveChangeAsync();
        }
        public async Task<List<WeightRecorderModal>> TraderGetWeightRecorder(int traderId)
        {
            List<WeightRecorderModal> weightRecorderModals = new List<WeightRecorderModal>();
            List<TraderOfWeightRecorder> ids = _unitOfWork.TraderOfWeightRecorders.GetAll(filter: tw => tw.TraderId == traderId).ToList();
            foreach (var traderOfWeightRecorder in ids)
            {
                int count = _unitOfWork.Transactions.GetAll(t => t.TraderId == traderId && t.WeightRecorderId == traderOfWeightRecorder.WeightRecorderId).ToList().Count;
                UserInfor wr = await _unitOfWork.UserInfors.FindAsync(traderOfWeightRecorder.WeightRecorderId);
                WeightRecorderModal weightRecorderModal = new WeightRecorderModal()
                {
                    ID = traderOfWeightRecorder.ID,
                    TraderId = traderId,
                    WrId = traderOfWeightRecorder.WeightRecorderId,
                    Name = wr.FirstName + " " + wr.LastName,
                    PhoneNumber = wr.PhoneNumber,
                    IsAccepted = traderOfWeightRecorder.IsAccepted,
                    CanDelete = count == 0,
                    IsDeleted = false
                };
                weightRecorderModals.Add(weightRecorderModal);
            }
            return weightRecorderModals;
        }

        public async Task TraderUpdateWeightRecorders(WeightRecorderModal weightRecorderModal)
        {
            if (weightRecorderModal.IsDeleted && weightRecorderModal.CanDelete)
            {
                _unitOfWork.TraderOfWeightRecorders.DeleteById(weightRecorderModal.ID);
            }
            else
            {
                var data = await _unitOfWork.TraderOfWeightRecorders.FindAsync(weightRecorderModal.ID);
                data.IsAccepted = weightRecorderModal.IsAccepted;
                _unitOfWork.TraderOfWeightRecorders.Update(data);
            }
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
