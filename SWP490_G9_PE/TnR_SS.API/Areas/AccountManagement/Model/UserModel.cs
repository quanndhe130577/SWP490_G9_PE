using System;
using System.ComponentModel.DataAnnotations;

namespace TnR_SS.API.Areas.AccountManagement.Model
{
    public class UserModel
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; }

        [MaxLength(12)]
        [MinLength(10)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        [Required]
        public string IdentifyCode { get; set; }

        public string Avatar { get; set; }

    }
}
