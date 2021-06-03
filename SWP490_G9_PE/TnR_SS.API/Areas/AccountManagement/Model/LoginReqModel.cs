using System.ComponentModel.DataAnnotations;

namespace TnR_SS.API.Areas.AccountManagement.Model
{
    public class LoginReqModel
    {
        [MinLength(10)]
        [MaxLength(12)]
        public string PhoneNumber { get; set; }
        [MinLength(8)]
        public string Password { get; set; }
    }

    public class LoginResModel
    {
        public string Token { get; set; }
        public int UserID { get; set; }
    }
}
