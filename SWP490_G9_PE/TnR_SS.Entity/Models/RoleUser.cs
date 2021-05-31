using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace TnR_SS.Entity.Models
{
    public partial class RoleUser : IdentityRole<int>
    {
        public RoleUser()
        {
            UserInfors = new HashSet<UserInfor>();
        }

        public override int Id { get; set; }
        public string RoleName { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<UserInfor> UserInfors { get; set; }
    }
}
