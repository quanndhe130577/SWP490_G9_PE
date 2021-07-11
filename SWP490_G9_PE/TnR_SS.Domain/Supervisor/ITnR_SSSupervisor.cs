using Microsoft.AspNetCore.Identity;
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
using TnR_SS.Domain.ApiModels.TimeKeepingModel;
using TnR_SS.Domain.ApiModels.TruckModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.ApiModels.EmployeeModel;
using TnR_SS.Domain.ApiModels.PurchaseDetailModel;
using TnR_SS.Domain.ApiModels.CostIncurredModel;
using TnR_SS.Domain.ApiModels.BuyerModel;

namespace TnR_SS.Domain.Supervisor
{
    public interface ITnR_SSSupervisor
    {

        #region OTP
        Task<bool> CheckOTPDoneAsync(int otpId, string phoneNumber);
        Task<bool> CheckOTPRightAsync(int otpId, string otp, string phoneNumber);
        bool CheckPhoneOTPExists(string phoneNumber);
        Task<int> AddOTPAsync(string code, string phoneNumber);
        #endregion

        #region Role
        Task<bool> RoleExistsAsync(string roleName);
        //Task<string> GetRoleNameAsync(UserInfor user);
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
        Task<UserResModel> GetUserByIdAsync(int id);
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
        List<FishTypeApiModel> GetAllLastFishTypeByPondOwnerId(int traderId, int pondOwnerId);
        List<FishTypeApiModel> GetFishTypesByPondOwnerIdAndDate(int traderId, int poId, DateTime date);
        Task CreateListFishTypeAsync(List<FishTypeApiModel> listType, int traderId);
        Task CreateFishTypeAsync(FishTypeApiModel listType, int traderId);
        Task UpdateFishTypeAsync(FishTypeApiModel fishTypeModel, int traderId);
        Task DeleteFishTypeAsync(int empId, int traderId);
        #endregion

        #region Basket
        List<BasketApiModel> GetAllBasket(int traderId);
        Task CreateBasketAsync(BasketApiModel basketRes, int traderId);
        Task UpdateBasketAsync(BasketApiModel basketModel, int traderId);
        Task DeleteBasketAsync(int basketId, int traderId);

        #endregion

        #region Purchase
        Task<PurchaseResModel> CreatePurchaseAsync(PurchaseReqModel purchaseModel);
        Task<List<PurchaseResModel>> GetAllPurchaseAsync(int traderId);
        Task<PurchaseResModel> GetPurchaseByIdAsync(int purchaseId, int traderId);
        Task UpdatePurchaseAsync(PurchaseApiModel models, int traderId);
        Task DeletePurchaseAsync(int purchaseId, int traderId);
        Task<PurchaseResModel> ChotSoAsync(ChotSoApiModel data, int traderId);
        #endregion

        #region PondOwner
        Task<PondOwner> GetPondOwner(int id);
        List<PondOwnerAPIModel> GetPondOwnerByTraderId(int traderId);
        Task<int> AddPondOwnerAsync(PondOwnerAPIModel pondOwnerModel);
        Task<int> EditPondOwner(PondOwnerAPIModel pondOwnerModel);
        Task<int> DeletePondOwner(PondOwner pondOwner);
        #endregion

        #region Truck
        Task<Truck> GetTruck(int id);
        List<TruckApiModel> GetAllTruckByTraderId(int traderId);
        Task<int> UpdateTruckAsync(TruckApiModel truckModel);
        Task<int> CreateTruckAsync(TruckApiModel truckModel, int traderId);
        Task<int> DeleteTruck(Truck truck);
        Task<List<TruckDateModel>> GetDetailTrucksByDate(int trader, DateTime date);
        #endregion

        #region Drum
        List<DrumApiModel> GetAllDrumByTruckId(int truckId);
        List<DrumApiModel> GetAllDrumByTraderId(int traderId);
        Task<int> CreateDrumAsync(DrumApiModel drumModel, int traderId);
        Task UpdateDrumAsync(DrumApiModel drumModel, int traderId);
        Task DeleteDrumAsync(int drumId, int traderId);
        DrumApiModel GetDetailDrum(int traderId, int drumId);
        #endregion

        #region Employee
        List<EmployeeApiModel> GetAllEmployeeByStatus(string status, int traderId);
        List<EmployeeApiModel> GetAllEmployeeByTraderId(int traderId);
        Task CreateEmployeesAsync(EmployeeApiModel employee, int traderId);
        Task UpdateEmployeeAsync(EmployeeApiModel employee, int traderId);
        Task DeleteEmployeeAsync(int empId, int traderId);
        EmployeeApiModel GetDetailEmployee(int traderId, int empId);
        #endregion

        #region TimeKeeping
        List<TimeKeepingApiModel> GetListTimeKeepingByTraderIdWithDate(int id, DateTime date);
        List<TimeKeepingApiModel> GetListTimeKeepingByTraderIdWithMonth(int id, DateTime date);
        List<TimeKeepingApiModel> GetListTimeKeepingByEmployeeId(int id);
        Task<TimeKeeping> GetTimeKeeping(int id);
        List<TimeKeepingApiModel> GetListTimeKeeping();
        Task<int> AddTimeKeeping(TimeKeepingApiModel timeKeeping);
        Task<int> EditTimeKeeping(TimeKeepingApiModel timeKeeping);
        Task<int> DeleteTimeKeeping(TimeKeeping timeKeeping);
        #endregion

        #region PurchaseDetail
        Task<List<PurchaseDetailResModel>> GetAllPurchaseDetailAsync(int purchaseId);
        Task<int> CreatePurchaseDetailAsync(PurchaseDetailReqModel data, int traderId);
        Task UpdatePurchaseDetailAsync(PurchaseDetailReqModel data);
        Task DeletePurchaseDetailAsync(int traderId, int purchaseDetailId);
        #endregion

        #region CostIncurred
        List<CostIncurredApiModel> GetAllCostIncurredTraderId(int traderId);
        Task CreateCostIncurredAsync(CostIncurredApiModel incurred, int traderId);
        Task UpdateCostIncurredAsync(CostIncurredApiModel incurred, int traderId);
        Task DeleteCostIncurredAsync(int incurredId, int traderId);
        CostIncurredApiModel GetDetailCostIncurred(int traderId, int incurredId);
        #endregion

        #region Buyer
        List<BuyerApiModel> GetAllBuyerByWCId(int wcId);
        Task CreateBuyerAsync(BuyerApiModel buyerModel, int wcId);
        Task UpdateBuyerAsync(BuyerApiModel buyerModel, int wcId);
        Task DeleteBuyerAsync(int buyerId, int wcId);
        Task<BuyerApiModel> GetDetailBuyerAsync(int buyerId, int wcId);
        #endregion



    }
}
