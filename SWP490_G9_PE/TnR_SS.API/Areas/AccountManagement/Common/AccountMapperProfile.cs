﻿using AutoMapper;
using System;
using TnR_SS.API.Areas.AccountManagement.Model;
using TnR_SS.Entity.Models;

namespace TnR_SS.API.Areas.AccountManagement.Common
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<RegisterReqModel, UserInfor>().ForMember(destination => destination.UserName, options => options.MapFrom(source => source.PhoneNumber))
                .AfterMap((source, destination) =>
                {
                    if (string.IsNullOrEmpty(destination.SecurityStamp))
                    {
                        destination.SecurityStamp = Guid.NewGuid().ToString();
                    }
                    if (destination.CreatedDate.Equals(DateTime.MinValue))
                    {
                        destination.CreatedDate = DateTime.Now;
                    }
                });
            //.ForAllOtherMembers(options => options.UseDestinationValue());
            //.ForMember(destination => destination.SecurityStamp, options => options.UseDestinationValue());
            /*CreateMap<ChangePasswordModel, UserInfor>().AfterMap((source, destination) =>
            {
                var saltPass = RandomSaltHash();
                destination.SaltPassword = saltPass;
                destination.Password = EncryptHandle.EncryptString(source.Password + saltPass);
            });*/
            CreateMap<UserReqModel, UserInfor>().ForMember(destination => destination.UserName, options => options.MapFrom(source => source.PhoneNumber));

            CreateMap<UserInfor, UserResModel>().ForMember(destination => destination.UserID, options => options.MapFrom(source => source.Id))
                .ForMember(destination => destination.RoleName, options => options.MapFrom(source => source.Role.DisplayName));
        }

        private static string RandomSaltHash()
        {
            string rs = "";
            Random rd = new Random();
            for (int i = 0; i < 20; i++)
            {
                rs += Convert.ToString((Char)rd.Next(65, 90));
            }
            return rs;
        }

    }
}