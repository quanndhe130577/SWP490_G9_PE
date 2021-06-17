﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.AccountModel.RequestModel;
using TnR_SS.Domain.ApiModels.AccountModel.ResponseModel;
using TnR_SS.Domain.ApiModels.BasketModel.ResponseModel;
using TnR_SS.Domain.ApiModels.DrumModel;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.PondOwnerModel;
using TnR_SS.Domain.ApiModels.PurchaseModal;
using TnR_SS.Domain.ApiModels.RoleUserModel.RequestModel;
using TnR_SS.Domain.ApiModels.TruckModel;
using TnR_SS.Domain.ApiModels.TimeKeepingModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public interface ITnR_SSSupervisor
    {
        void Test();

        #region OTP
        Task<bool> CheckOTPDoneAsync(int otpId, string phoneNumber);
        Task<bool> CheckOTPRightAsync(int otpId, string otp, string phoneNumber);
        bool CheckPhoneOTPExists(string phoneNumber);
        Task<int> AddOTPAsync(string code, string phoneNumber);
        #endregion

        #region Role
        Task<bool> RoleExistsAsync(string roleName);
        Task<string> GetRoleDisplayNameAsync(UserInfor user);
        List<AllRoleResModel> GetAllResRoles();
        Task<IdentityResult> AddRoleUserAsync(RoleUser role);
        #endregion

        #region UserInfor
        Task<IdentityResult> CreateUserAsync(RegisterUserReqModel userData, string avatarLink);
        Task<IdentityResult> AddToRoleAsync(UserInfor user, string role);
        Task<IdentityResult> DeleteUserAsync(UserInfor user);
        UserInfor GetUserByPhoneNumber(string phoneNumber);
        Task SignOutAsync();
        Task<UserResModel> SignInWithPasswordAsync(UserInfor user, string password);
        Task SignInAsync(UserInfor user);
        UserResModel GetUserById(int id);
        Task<IdentityResult> UpdateUserAsync(UpdateUserReqModel user, int id, string avatarLink);
        Task<IdentityResult> UpdatePhoneNumberAsync(int id, string newPhone);
        Task<IdentityResult> ChangeUserPasswordAsync(int userId, string currentPassword, string newPassword);
        Task<IdentityResult> ResetUserPasswordAsync(UserInfor user, string token, string newPassword);
        Task<IList<string>> GetUserRolesAsync(UserInfor user);
        bool CheckUserPhoneExists(string phoneNumber);
        Task<string> GetPasswordResetTokenAsync(UserInfor user);
        Task<bool> CheckUserPassword(int userId, string password);
        Task<UserResModel> GetUserResModelByIdAsync(int id);
        #endregion

        #region Fishtype
        List<FishTypeApiModel> GetAllLastFishTypeByTraderId(int traderId);
        Task CreateFishTypeAsync(List<FishTypeApiModel> listType, int traderId);
        Task UpdateFishTypeAsync(FishTypeApiModel fishTypeModel);
        #endregion

        #region Basket
        List<BasketApiModel> GetAllBasket();
        Task CreateBasketAsync(BasketApiModel basketRes);
        Task UpdateBasketAsync(BasketApiModel basketRes);

        #endregion

        #region Purchase
        Task<PurchaseResModel> CreatePurchaseAsync(PurchaseReqModel purchaseModel);
        #endregion

        #region PondOwner
        Task<PondOwner> GetPondOwner(Guid id);
        List<PondOwnerAPIModel> GetPondOwnerByTraderId(int traderId);
        Task<int> AddPondOwner(PondOwnerAPIModel pondOwnerModel);
        Task<int> EditPondOwner(PondOwnerAPIModel pondOwnerModel);
        Task<int> DeletePondOwner(PondOwner pondOwner);
        #endregion

        #region Truck
        List<TruckApiModel> GetAllTruckByTraderId(int traderId);
        Task<int> CreateTruckAsync(TruckApiModel truckModel, int traderId);
        #endregion

        #region Drum
        List<DrumApiModel> GetAllDrumByTruckId(int truckId);
        List<DrumApiModel> GetAllDrumByTraderId(int traderId);
        Task<int> CreateDrumAsync(DrumApiModel drumModel);
        #endregion
        
        #region Employee
        List<EmployeeApiModel> GetAllEmployeeByTraderId(int traderId);
        Task CreateEmployeesAsync(EmployeeApiModel employee, int traderId);
        Task UpdateEmployeeAsync(EmployeeApiModel employee, int traderId);
        Task DeleteEmployeeAsync(int empId, int traderId);
        EmployeeApiModel GetDetailEmployee(int traderId, int empId);
........#endregion
        
        #region TimeKeeping
        List<TimeKeepingApiModel> GetListTimeKeepingByTraderId(int id);
        List<TimeKeepingApiModel> GetListTimeKeepingByEmployeeId(int id);
        Task<TimeKeeping> GetTimeKeeping(int id);
        List<TimeKeepingApiModel> GetListTimeKeeping();
        Task<int> AddTimeKeeping(TimeKeepingApiModel timeKeeping);
        Task<int> EditTimeKeeping(TimeKeepingApiModel timeKeeping);
        Task<int> DeleteTimeKeeping(TimeKeeping timeKeeping);
        #endregion
    }
}
