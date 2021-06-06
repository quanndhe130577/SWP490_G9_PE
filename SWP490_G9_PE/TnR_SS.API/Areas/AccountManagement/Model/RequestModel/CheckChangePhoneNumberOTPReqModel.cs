using System.ComponentModel.DataAnnotations;

namespace TnR_SS.API.Areas.AccountManagement.Model.RequestModel
{
    public class CheckChangePhoneNumberOTPReqModel : OTPReqModel
    {
        [Required]
        [MinLength(10)]
        public string NewPhoneNumber { get; set; }

    }
}
