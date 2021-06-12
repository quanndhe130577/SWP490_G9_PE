using AutoMapper;
using System;
using TnR_SS.Domain.ApiModels.AccountModel.RequestModel;
using TnR_SS.Domain.ApiModels.AccountModel.ResponseModel;
using TnR_SS.Domain.ApiModels.BasketModel.ResponseModel;
using TnR_SS.Domain.ApiModels.PondOwnerModel;
using TnR_SS.Domain.ApiModels.PurchaseModal;
using TnR_SS.Domain.ApiModels.RoleUserModel.RequestModel;
using TnR_SS.Domain.ApiModels.RoleUserModel.ResponseModel;
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

            #region Purchase
            CreateMap<PurchaseApiModel, Purchase>();
            #endregion


            CreateMap<PondOwnerResModel, PondOwner>().ReverseMap();
        }

    }
}
