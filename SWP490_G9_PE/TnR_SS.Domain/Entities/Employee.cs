using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        //public string LastName { get; set; }

        public DateTime DOB { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [Required]
        public int TraderId { get; set; }
        public UserInfor UserInfor { get; set; }
        public List<TimeKeeping> TimeKeepings { get; set; }
    }
}
