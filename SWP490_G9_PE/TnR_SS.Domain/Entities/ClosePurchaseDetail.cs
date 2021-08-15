using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.DrumModel;

namespace TnR_SS.Domain.Entities
{
    public class ClosePurchaseDetail : BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }

        public int BasketId { get; set; }
        public string BasketType { get; set; }
        public double BasketWeight { get; set; }

        public int FishTypeId { get; set; }
        public string FishName { get; set; }
        public string FishTypeDescription { get; set; }
        public float FishTypeMinWeight { get; set; }
        public float FishTypeMaxWeight { get; set; }
        public double FishTypePrice { get; set; }
        public double FishTypeTransactionPrice { get; set; }

        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        public List<LK_PurchaseDeatil_Drum> LK_PurchaseDeatil_Drums { get; set; }
    }
}
