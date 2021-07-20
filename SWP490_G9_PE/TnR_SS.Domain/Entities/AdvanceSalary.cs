using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    [Table("AdvanceSalary")]
    public class AdvanceSalary : BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public double Debt { get; set; }

        [Required]
        public int EmpId { get; set; }

        public Employee Employee { get; set; }

        public bool Paid { get; set; }
    }
}
