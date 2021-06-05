using System.ComponentModel.DataAnnotations;

namespace TnR_SS.API.Areas.AccountManagement.Model.RequestModel
{
    public class ChangePasswordReqModel
    {
        [MinLength(8)]
        public string CurrentPassword { get; set; }

        [MinLength(8)]
        public string NewPassword { get; set; }

        [MinLength(8)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
