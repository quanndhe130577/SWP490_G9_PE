using System;
using System.ComponentModel.DataAnnotations;

namespace TnR_SS.API.Areas.AccountManagement.Model
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        [MaxLength(12)]
        [MinLength(10)]
        public string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
        public string IdentifyCode { get; set; }
        public string Avatar { get; set; }
        public int RoleId { get; set; }

    }
}
