using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TnR_SS.API.Areas.OTPManagement.Model.RequestModel
{
    public class OTPReqModel
    {
        [Required]
        public int OTPID { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
