using System.ComponentModel.DataAnnotations;

namespace TnR_SS.Domain.ApiModels.AccountModel.RequestModel
{
    public class LoginReqModel
    {
        [RegularExpression(@"(84|0[3|5|7|8|9])+([0-9]{8})\b", ErrorMessage = "Phone Number invalid")]
        [MinLength(10)]
        public string PhoneNumber { get; set; }

        [MinLength(8)]
        public string Password { get; set; }
    }

}
