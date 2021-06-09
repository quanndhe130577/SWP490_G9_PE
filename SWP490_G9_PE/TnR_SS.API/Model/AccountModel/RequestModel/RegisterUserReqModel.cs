using System.ComponentModel.DataAnnotations;

namespace TnR_SS.API.Model.AccountModel.RequestModel
{
    public class RegisterUserReqModel : UserModel
    {
        public string AvatarBase64 { get; set; }

        [Required]
        [MinLength(8)]
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
