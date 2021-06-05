using System;
using System.ComponentModel.DataAnnotations;

namespace TnR_SS.API.Areas.AccountManagement.Model.ResponseModel
{
    public class LoginResModel
    {
        public string Token { get; set; }
        public UserResModel UserInfor { get; set; }
    }

    public class UserResModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }

        [MaxLength(12)]
        [MinLength(10)]
        public string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
        public string IdentifyCode { get; set; } // chung minh thu nhan dan
        public string Avatar { get; set; }
        public string RoleDisplayName { get; set; }
    }
}
