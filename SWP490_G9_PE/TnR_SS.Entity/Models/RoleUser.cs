using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace TnR_SS.Entity.Models
{
    public partial class RoleUser : IdentityRole<int>
    {
        [Required]
        public string DisplayName { get; set; }

        public RoleUser(string name, string displayName) : base(name)
        {
            this.DisplayName = displayName;
        }

        public RoleUser() : base()
        {
        }
    }
}
