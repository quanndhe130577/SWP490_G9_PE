using System.ComponentModel.DataAnnotations;

namespace TnR_SS.API.Areas.AccountManagement.Model
{
    public class ChangePasswordModel
    {
        [MinLength(8)]
        public string Password { get; set; }
        [MinLength(8)]
        public string ConfirmPassword { get; set; }
    }
}
