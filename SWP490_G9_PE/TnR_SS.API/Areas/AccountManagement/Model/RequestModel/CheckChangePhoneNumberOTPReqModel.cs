using System.ComponentModel.DataAnnotations;
using TnR_SS.API.Areas.OTPManagement.Model.RequestModel;

namespace TnR_SS.API.Areas.AccountManagement.Model.RequestModel
{
    public class CheckChangePhoneNumberOTPReqModel
    {
        [Required]
        [MinLength(10)]
        public string NewPhoneNumber { get; set; }
        [Required]
        public int OTPID { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string OTP { get; set; }

    }
}
