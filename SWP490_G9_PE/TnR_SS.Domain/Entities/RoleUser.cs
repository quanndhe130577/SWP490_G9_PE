using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TnR_SS.Domain.Entities
{
    [Table("RoleUser")]
    public partial class RoleUser : IdentityRole<int>
    {
        [Required]
        public string DisplayName { get; set; }

        public RoleUser(string name, string displayName) : base(name)
        {
            this.DisplayName = displayName;
        }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }

        public RoleUser() : base()
        {
        }
    }

    public static class RoleName
    {
        public const string Trader = "Trader";
        public const string Admin = "Admin";
        public const string WeightRecorder = "WeightRecorder";
    }
}
