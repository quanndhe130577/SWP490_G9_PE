using System.ComponentModel.DataAnnotations;

namespace TnR_SS.Domain.ApiModels.AccountModel.RequestModel
{
    public class RegisterUserReqModel : UserInforApiModel
    {
        public string AvatarBase64 { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Mật khẩu phải có ít nhất 8 kí tự")]
        public string Password { get; set; }

        [Required]
        public string RoleNormalizedName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You need OTPID")]
        public int OTPID { get; set; }

        [MaxLength(12)]
        [MinLength(10)]
        public string PhoneNumber { get; set; }
    }
}
