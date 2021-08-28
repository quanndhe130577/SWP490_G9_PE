using System.ComponentModel.DataAnnotations;

namespace TnR_SS.Domain.ApiModels.AccountModel.RequestModel
{
    public class RegisterUserReqModel : UserInforApiModel
    {
        public string AvatarBase64 { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 kí tự")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Hãy chọn Vai trò")]
        public string RoleNormalizedName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "OTP sai")]
        public int OTPID { get; set; }

        [MaxLength(12, ErrorMessage = "Số điện thoại không đúng")]
        [MinLength(10, ErrorMessage = "Số điện thoại không đúng")]
        public string PhoneNumber { get; set; }
    }
}
