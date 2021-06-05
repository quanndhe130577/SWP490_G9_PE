using System.ComponentModel.DataAnnotations;

namespace TnR_SS.API.Areas.AccountManagement.Model.RequestModel
{
    public class RegisterUserReqModel : UserModel
    {
        public string Password { get; set; }
        [Required]
        public string RoleNormalizedName { get; set; }
    }
}
