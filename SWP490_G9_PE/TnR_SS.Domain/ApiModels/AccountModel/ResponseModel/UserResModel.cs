using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.AccountModel.ResponseModel
{
    public class UserResModel : UserModel
    {
        public int UserID { get; set; }

        [MaxLength(12)]
        [MinLength(10)]
        public string PhoneNumber { get; set; }
        public string Avatar { get; set; }
        public string RoleDisplayName { get; set; }
    }
}
