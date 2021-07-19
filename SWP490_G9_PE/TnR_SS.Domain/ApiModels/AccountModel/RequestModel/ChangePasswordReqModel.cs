using System.ComponentModel.DataAnnotations;

namespace TnR_SS.Domain.ApiModels.AccountModel.RequestModel
{
    public class ChangePasswordReqModel
    {
        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 kí tự")]
        public string CurrentPassword { get; set; }

        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 kí tự")]
        public string NewPassword { get; set; }

        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 kí tự")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu không nhập lại không đúng ")]
        public string ConfirmPassword { get; set; }
    }
}
