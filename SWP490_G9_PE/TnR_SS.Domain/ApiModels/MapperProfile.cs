using AutoMapper;
using TnR_SS.Domain.ApiModels.AccountModel.RequestModel;
using TnR_SS.Domain.ApiModels.AccountModel.ResponseModel;
using TnR_SS.Domain.ApiModels.BasketModel.ResponseModel;
using TnR_SS.Domain.ApiModels.DrumModel;
using TnR_SS.Domain.ApiModels.EmployeeModel;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.LK_PurchaseDetail_DrumModel;
using TnR_SS.Domain.ApiModels.PondOwnerModel;
using TnR_SS.Domain.ApiModels.PurchaseDetailModel;
using TnR_SS.Domain.ApiModels.PurchaseModal;
using TnR_SS.Domain.ApiModels.RoleUserModel.RequestModel;
using TnR_SS.Domain.ApiModels.RoleUserModel.ResponseModel;
using TnR_SS.Domain.ApiModels.TimeKeepingModel;
using TnR_SS.Domain.ApiModels.TruckModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.ApiModels
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            #region UserInfor
            CreateMap<RegisterUserReqModel, UserInfor>().ForMember(destination => destination.UserName, options => options.MapFrom(source => source.PhoneNumber));
            /*.AfterMap((source, destination) =>
            {
                destination.CreatedDate = DateTime.Now;
            });*/

            CreateMap<UpdateUserReqModel, UserInfor>();
            CreateMap<UserInfor, UserResModel>().ForMember(destination => destination.UserID, options => options.MapFrom(source => source.Id));
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
            #endregion

            #region Purchase
            CreateMap<PurchaseReqModel, Purchase>().ReverseMap();
            CreateMap<PurchaseResModel, Purchase>().ReverseMap();
            #endregion

            #region PondOwner
            CreateMap<PondOwnerAPIModel, PondOwner>().ReverseMap();
            #endregion

            #region TimeKeeping
            CreateMap<TimeKeepingApiModel, TimeKeeping>().ReverseMap();
            #endregion

            #region Employee
            CreateMap<Employee, EmployeeApiModel>();
            CreateMap<EmployeeApiModel, Employee>();
            #endregion

            #region Truck
            CreateMap<Truck, TruckApiModel>().ReverseMap();
            #endregion

            #region Drum
            CreateMap<Drum, DrumApiModel>().ReverseMap();
            #endregion

            #region PurchaseDetail
            CreateMap<PurchaseDetail, PurchaseDetailReqModel>().ReverseMap();
            CreateMap<PurchaseDetail, PurchaseDetailResModel>().ReverseMap();
            #endregion

            #region LK_PurchaseDeatil_Drum
            CreateMap<LK_PurchaseDeatil_Drum, LK_PurchaseDetail_DrumApiModel>().ReverseMap();
            #endregion
        }

    }
}
