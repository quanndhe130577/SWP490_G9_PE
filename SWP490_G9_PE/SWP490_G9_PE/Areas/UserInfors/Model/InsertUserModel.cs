using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.HandleSHA256;
using TnR_SS.API.UserInfors.Common;
using TnR_SS.Entity.Models;

namespace TnR_SS.API.UserInfors.Model
{
    public class InsertUserModel
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public DateTime Dob { get; set; }
        public string IdentifyCode { get; set; }
        public string Avatar { get; set; }
        public int RoleId { get; set; }

        public UserInfor changeToUserInfor()
        {
            UserInfor uInfor = new UserInfor();
            uInfor.FirstName = this.FirstName;
            uInfor.Lastname = this.Lastname;
            uInfor.Dob = this.Dob;
            uInfor.Avatar = this.Avatar;
            uInfor.IdentifyCode = this.IdentifyCode;
            uInfor.PhoneNumber = this.PhoneNumber;
            uInfor.RoleId = this.RoleId;
            return uInfor;
        }

        public void updateUserInfor(in UserInfor uInfor)
        {
            uInfor.FirstName = this.FirstName;
            uInfor.Lastname = this.Lastname;
            uInfor.Dob = this.Dob;
            uInfor.Avatar = this.Avatar;
            uInfor.IdentifyCode = this.IdentifyCode;
            uInfor.PhoneNumber = this.PhoneNumber;
            uInfor.RoleId = this.RoleId;

            uInfor.SaltPassword = SaltHashHandle.RandomSaltHash();
            uInfor.Password = HandleSHA256.EncryptString(this.Password + uInfor.SaltPassword);
            uInfor.CreatedDate = DateTime.Now;
        }
    }
}
