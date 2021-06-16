using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    [Table("FishType")]
    public class FishType
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public string FishName { get; set; }
        public string Description { get; set; }
        [Required]
        public float MinWeight { get; set; }
        [Required]
        public float MaxWeight { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }

        public DateTime Date { get; set; }
        public double Price { get; set; }

        [Required]
        public int TraderID { get; set; }
        public UserInfor Trader { get; set; }


        public List<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
