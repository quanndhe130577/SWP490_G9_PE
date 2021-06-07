using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using TnR_SS.API.Areas.AccountManagement.Model.RequestModel;
using TnR_SS.API.Areas.AccountManagement.Model.ResponseModel;
using TnR_SS.Entity.Models;

namespace TnR_SS.API.Areas.AccountManagement.Common
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
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
            CreateMap<ResetPasswordReqModel, OTPReqModel>();

            CreateMap<UserInfor, UserResModel>().ForMember(destination => destination.UserID, options => options.MapFrom(source => source.Id));

        }

    }
}
