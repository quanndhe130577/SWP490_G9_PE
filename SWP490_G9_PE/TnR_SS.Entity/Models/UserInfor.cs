using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TnR_SS.Entity.Models
{
    public partial class UserInfor : IdentityUser<int>
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public override string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string SaltPassword { get; set; }
        public DateTime Dob { get; set; }
        public string IdentifyCode { get; set; }
        public string Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey(nameof(RoleUser))]
        public int RoleId { get; set; }

        public virtual RoleUser Role { get; set; }
    }
}
