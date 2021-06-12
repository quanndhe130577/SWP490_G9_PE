using System.ComponentModel.DataAnnotations;

namespace TnR_SS.Domain.ApiModels.AccountModel.RequestModel
{
    public class LoginReqModel
    {
        [MinLength(10)]
        public string PhoneNumber { get; set; }

        [MinLength(8)]
        public string Password { get; set; }
    }

}
