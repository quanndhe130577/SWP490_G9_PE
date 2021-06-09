using System;
using System.ComponentModel.DataAnnotations;

namespace TnR_SS.API.Model.AccountModel
{
    public class UserModel
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; }


        [Required]
        public DateTime Dob { get; set; }

        [Required]
        public string IdentifyCode { get; set; }

    }
}
