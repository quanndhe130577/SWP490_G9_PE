using System.ComponentModel.DataAnnotations;

namespace TnR_SS.Domain.ApiModels.AccountModel.RequestModel
{
    public class ChangePhoneNumberOTPReqModel
    {
        [Required]
        [MinLength(10)]
        [MaxLength(11)]
        public string NewPhoneNumber { get; set; }
        [Required]
        public int OTPID { get; set; }
        [Required]
        public string Code { get; set; }

    }
}
