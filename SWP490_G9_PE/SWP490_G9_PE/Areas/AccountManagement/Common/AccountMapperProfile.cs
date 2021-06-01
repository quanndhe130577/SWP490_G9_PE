using AutoMapper;
using System;
using TnR_SS.API.Areas.AccountManagement.Model;
using TnR_SS.API.Common.HandleSHA256;
using TnR_SS.Entity.Models;

namespace TnR_SS.API.Areas.AccountManagement.Common
{
    public class AccountMapperProfile : Profile
    {

        public AccountMapperProfile()
        {
            CreateMap<RegisterModel, UserInfor>().ForMember(destination => destination.UserName, options => options.MapFrom(source => source.PhoneNumber))
            //.ForMember(destination => destination.SaltPassword, options => SaltHashHandle.RandomSaltHash())
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
            CreateMap<UserModel, UserInfor>().ForMember(destination => destination.UserName, options => options.MapFrom(source => source.PhoneNumber));
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
