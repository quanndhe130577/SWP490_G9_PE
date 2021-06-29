using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.OTPModel.RequestModel
{
    public class OTPReqModel
    {
        [Required]
        public int OTPID { get; set; }

        [RegularExpression(@"(84|0[3|5|7|8|9])+([0-9]{8})\b", ErrorMessage = "Phone Number invalid")]
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
