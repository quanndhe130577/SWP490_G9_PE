﻿using System.ComponentModel.DataAnnotations;

namespace TnR_SS.API.Areas.AccountManagement.Model.RequestModel
{
    public class LoginReqModel
    {
        [MinLength(10)]
        public string PhoneNumber { get; set; }

        [MinLength(8)]
        public string Password { get; set; }
    }

}
