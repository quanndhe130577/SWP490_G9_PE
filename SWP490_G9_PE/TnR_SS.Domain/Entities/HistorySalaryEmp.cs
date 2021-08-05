using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    [Table("HistorySalaryEmp")]
    public class HistorySalaryEmp : BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public DateTime DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        // [Required]
        // public int Month { get; set; }

        // [Required]
        // public int Year { get; set; }

        [Required]
        public double Salary { get; set; }
        public double Bonus { get; set; }
        public double Punish { get; set; }
        [Required]
        public int EmpId { get; set; }
        public Employee Employee { get; set; }
    }
}
