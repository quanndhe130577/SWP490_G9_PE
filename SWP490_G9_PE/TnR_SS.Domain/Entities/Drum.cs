using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    public class Drum
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int TruckID { get; set; }

        public Truck Truck { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public int MaxWeight { get; set; }

        public string Type { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<LK_PurchaseDeatil_Drum> LK_PurchaseDeatil_Drums { get; set; }
    }
}
