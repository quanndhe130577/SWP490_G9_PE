using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.Entity.Models;

namespace TnR_SS.API.UserInfors.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
        public string IdentifyCode { get; set; }
        public string Avatar { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public static UserModel changeToModel(UserInfor uInfor)
        {
            return new UserModel()
            {
                Id = uInfor.Id,
                FirstName = uInfor.FirstName,
                Lastname = uInfor.Lastname,
                Dob = uInfor.Dob,
                Avatar = uInfor.Avatar,
                IdentifyCode = uInfor.IdentifyCode,
                PhoneNumber = uInfor.PhoneNumber,
                RoleName = uInfor.Role.DisplayName,
                RoleId = uInfor.RoleId,
            };
        }
    }
}
