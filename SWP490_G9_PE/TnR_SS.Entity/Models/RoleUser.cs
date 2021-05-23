using System;
using System.Collections.Generic;

#nullable disable

namespace TnR_SS.Entity.Models
{
    public partial class RoleUser
    {
        public RoleUser()
        {
            UserInfors = new HashSet<UserInfor>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<UserInfor> UserInfors { get; set; }
    }
}
