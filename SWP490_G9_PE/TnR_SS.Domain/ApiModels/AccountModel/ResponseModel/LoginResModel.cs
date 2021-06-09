using System;
using System.ComponentModel.DataAnnotations;

namespace TnR_SS.Domain.ApiModels.AccountModel.ResponseModel
{
    public class LoginResModel
    {
        public string Token { get; set; }
        public UserResModel User { get; set; }
    }

   
}
