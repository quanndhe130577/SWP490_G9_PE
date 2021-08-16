using System;
using System.ComponentModel.DataAnnotations;

namespace TnR_SS.Domain.ApiModels.AccountModel
{
    public class UserInforApiModel
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }


        [Required]
        public DateTime Dob { get; set; }

        [Required]
        public string IdentifyCode { get; set; }
        public string Address { get; set; }

    }
}
