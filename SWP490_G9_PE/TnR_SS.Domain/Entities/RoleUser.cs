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
        public DateTime CreatedAt { get; set; } = new DateTime(2021, 01, 01);
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public override string NormalizedName { get => base.NormalizedName; }
        public override string Name { get => base.Name; set { base.Name = value; base.NormalizedName = value.ToUpperInvariant(); } }

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
