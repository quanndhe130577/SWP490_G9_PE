using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TnR_SS.API.Areas.AccountManagement.Model.RequestModel
{
    public class ResetPasswordReqModel
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(6)]
        public string OTP { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
