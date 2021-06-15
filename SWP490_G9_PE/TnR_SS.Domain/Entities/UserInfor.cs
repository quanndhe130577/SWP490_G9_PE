using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace TnR_SS.Domain.Entities
{
    [Table("UserInfor")]
    public partial class UserInfor : IdentityUser<int>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public override string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
        public string IdentifyCode { get; set; }
        public string Avatar { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }

        public List<Purchase> Purchases { get; set; }
        public List<Employee> Employees { get; set; }
        public List<PondOwner> PondOwners { get; set; }
        public List<FishType> FishTypes { get; set; }
        public List<Truck> Trucks { get; set; }
    }
}
