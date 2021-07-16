using AutoMapper;
using TnR_SS.Domain.ApiModels.AccountModel.RequestModel;
using TnR_SS.Domain.ApiModels.AccountModel.ResponseModel;
using TnR_SS.Domain.ApiModels.BasketModel.ResponseModel;
using TnR_SS.Domain.ApiModels.BuyerModel;
using TnR_SS.Domain.ApiModels.CostIncurredModel;
using TnR_SS.Domain.ApiModels.DrumModel;
using TnR_SS.Domain.ApiModels.EmployeeModel;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.FishTypeModel.ResponseModel;
using TnR_SS.Domain.ApiModels.LK_PurchaseDetail_DrumModel;
using TnR_SS.Domain.ApiModels.PondOwnerModel;
using TnR_SS.Domain.ApiModels.PurchaseDetailModel;
using TnR_SS.Domain.ApiModels.PurchaseModal;
using TnR_SS.Domain.ApiModels.RoleUserModel.RequestModel;
using TnR_SS.Domain.ApiModels.RoleUserModel.ResponseModel;
using TnR_SS.Domain.ApiModels.TimeKeepingModel;
using TnR_SS.Domain.ApiModels.TransactionDetailModel;
using TnR_SS.Domain.ApiModels.TransactionModel;
using TnR_SS.Domain.ApiModels.TruckModel;
using TnR_SS.Domain.ApiModels.UserInforModel;
using TnR_SS.Domain.ApiModels.EmployeeDebtModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.ApiModels
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            #region UserInfor
            CreateMap<RegisterUserReqModel, UserInfor>().ForMember(destination => destination.UserName, options => options.MapFrom(source => source.PhoneNumber)).ReverseMap();
            /*.AfterMap((source, destination) =>
            {
                destination.CreatedDate = DateTime.Now;
            });*/

            CreateMap<UpdateUserReqModel, UserInfor>().ReverseMap();
            CreateMap<FindTraderByPhoneApiModel, UserInfor>().ReverseMap();
            CreateMap<TraderInformation, UserInfor>().ReverseMap();
            CreateMap<UserInfor, UserResModel>().ForMember(destination => destination.UserID, options => options.MapFrom(source => source.Id)).ReverseMap();
            #endregion

            #region Role
            CreateMap<RoleUser, AllRoleResModel>();
            CreateMap<CreateRoleReqModel, RoleUser>();
            #endregion

            #region Basket
            CreateMap<Basket, BasketApiModel>();
            CreateMap<BasketApiModel, Basket>();
            #endregion

            #region FishType
            CreateMap<FishType, FishTypeApiModel>().ReverseMap();
            CreateMap<FishType, FishTypeResModel>().ReverseMap();
            #endregion

            #region Purchase
            CreateMap<PurchaseCreateReqModel, Purchase>().ReverseMap();
            CreateMap<PurchaseResModel, Purchase>().ReverseMap();
            CreateMap<PurchaseApiModel, Purchase>().ReverseMap();
            #endregion

            #region PondOwner
            CreateMap<PondOwnerApiModel, PondOwner>().ReverseMap();
            #endregion

            #region TimeKeeping
            CreateMap<TimeKeepingApiModel, TimeKeeping>().ReverseMap();
            #endregion

            #region Employee
            CreateMap<Employee, EmployeeApiModel>().ReverseMap();
            #endregion

            #region Truck
            CreateMap<Truck, TruckApiModel>().ReverseMap();
            CreateMap<Truck, TruckDateModel>().ReverseMap();
            #endregion

            #region Drum
            CreateMap<Drum, DrumApiModel>().ReverseMap();
            CreateMap<Drum, DrumWeightModel>().ReverseMap();
            #endregion

            #region PurchaseDetail
            CreateMap<PurchaseDetail, PurchaseDetailReqModel>().ReverseMap();
            CreateMap<PurchaseDetail, PurchaseDetailResModel>().ReverseMap();
            #endregion

            #region LK_PurchaseDeatil_Drum
            CreateMap<LK_PurchaseDeatil_Drum, LK_PurchaseDetail_DrumApiModel>().ReverseMap();
            #endregion

            #region CostIncurred
            CreateMap<CostIncurred, CostIncurredApiModel>().ReverseMap();
            #endregion

            #region Buyer
            CreateMap<Buyer, BuyerApiModel>().ReverseMap();
            #endregion

            #region Transaction Detail
            CreateMap<TransactionDetail, CreateTransactionDetailApiModel>().ReverseMap();
            #endregion

            #region Employee Debt
            CreateMap<EmployeeDebt, EmployeeDebtApiModel>().ReverseMap();
            #endregion
        }

    }
}
