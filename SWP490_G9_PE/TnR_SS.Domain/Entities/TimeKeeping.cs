using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    public enum TimeKeepingNote { IsPaid, NotPaid }
    [Table("TimeKeeping")]
    public class TimeKeeping
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public DateTime WorkDay { get; set; }

        [Required]
        public double Status { get; set; }

        public double Money { get; set; }

        public TimeKeepingNote Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Required]
        public int EmpId { get; set; }
        public Employee Employee { get; set; }
    }
}
