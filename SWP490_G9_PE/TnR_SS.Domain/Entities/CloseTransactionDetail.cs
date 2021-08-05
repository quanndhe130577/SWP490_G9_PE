using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    public class CloseTransactionDetail : BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int FishTypeId { get; set; }
        public string FishName { get; set; }
        public string FishTypeDescription { get; set; }
        public float FishTypeMinWeight { get; set; }
        public float FishTypeMaxWeight { get; set; }

        public int? BuyerId { get; set; }
        public string BuyerName { get; set; } = "";
        public string BuyerAddress { get; set; } = "";
        public string BuyerPhoneNumber { get; set; } = "";
        public double SellPrice { get; set; }
        public double Weight { get; set; }
        public int TransactionDetailId { get; set; }
        public TransactionDetail TransactionDetail { get; set; }
    }
}
