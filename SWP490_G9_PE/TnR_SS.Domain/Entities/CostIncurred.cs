using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    public class CostIncurred : BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Cost { get; set; }

        public string Note { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public UserInfor User { get; set; }

    }
}
