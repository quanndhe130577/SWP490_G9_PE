using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    public class PurchaseDetail : BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int FishTypeID { get; set; }
        public FishType FishType { get; set; }

        /*[Required]
        public double BuyPrice { get; set; }*/
        [Required]
        public double Weight { get; set; }


        [Required]
        public int BasketId { get; set; }
        public Basket Basket { get; set; }

        [Required]
        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        public List<LK_PurchaseDeatil_Drum> LK_PurchaseDeatil_Drums { get; set; }
        //public ClosePurchaseDetail ClosePurchaseDetail { get; set; }
    }
}