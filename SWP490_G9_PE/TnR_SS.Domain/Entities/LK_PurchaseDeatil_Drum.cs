using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    public class LK_PurchaseDeatil_Drum
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public int PurchaseDetailID { get; set; }
        public PurchaseDetail PurchaseDetail { get; set; }
        [Required]
        public int DrumID { get; set; }
        public Drum Drum { get; set; }
        /*[Required]
        public double Weight { get; set; }*/

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
