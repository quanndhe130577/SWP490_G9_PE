﻿using AutoMapper;
using System;
using TnR_SS.API.Model.AccountModel.RequestModel;
using TnR_SS.API.Model.AccountModel.ResponseModel;
using TnR_SS.API.Model.RoleUserModel.RequestModel;
using TnR_SS.API.Model.RoleUserModel.ResponseModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.API.Model
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            CreateMap<RegisterUserReqModel, UserInfor>().ForMember(destination => destination.UserName, options => options.MapFrom(source => source.PhoneNumber))
                .AfterMap((source, destination) =>
                {
                    /*if (string.IsNullOrEmpty(destination.SecurityStamp))
                    {
                        destination.SecurityStamp = Guid.NewGuid().ToString();
                    }*/
                    if (destination.CreatedDate.Equals(DateTime.MinValue))
                    {
                        destination.CreatedDate = DateTime.Now;
                    }
                });

            CreateMap<UpdateUserReqModel, UserInfor>();

            CreateMap<UserInfor, UserResModel>().ForMember(destination => destination.UserID, options => options.MapFrom(source => source.Id));
            CreateMap<RoleUser, AllRoleResModel>();
            CreateMap<CreateRoleReqModel, RoleUser>();
        }

    }
}