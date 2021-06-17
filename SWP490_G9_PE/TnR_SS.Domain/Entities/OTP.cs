using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TnR_SS.Domain.Entities
{
    [Table("OTP")]
    public class OTP
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public DateTime ExpiredDate { get; set; }

        [Required]
        [StringLength(6)]
        public string Code { get; set; }

        [Required]
        [MaxLength(12)]
        [MinLength(10)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Status { get; set; }
    }

    public enum OTPStatus
    {
        Waiting,
        Done
    }
}
