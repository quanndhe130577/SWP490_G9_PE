using AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using TnR_SS.API.Areas.AccountManagement.Common;
using TnR_SS.API.Common.HandleSHA256;
using TnR_SS.Entity.Models;

namespace TnR_SS.Areas.API.AccountManagement.Model
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
