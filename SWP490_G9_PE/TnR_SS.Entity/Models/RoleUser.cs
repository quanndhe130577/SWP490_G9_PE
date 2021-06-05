using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

#nullable disable

namespace TnR_SS.Entity.Models
{
    public partial class RoleUser : IdentityRole<int>
    {
        public string DisplayName { get; set; }
    }
}
