using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    public class Transaction
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        
        [Required]
        public int TraderId { get; set; }

        [Required]
        public int BuyerId { get; set; }

        [Required]
        public int WeightRecorderId { get; set; }
        //public double TotalAmount { get; set; }
        public double Weight { get; set; }
        public double PriceUnit { get; set; }
        //public double AmountPaidUnit { get; set; }
        public double CommissionUnit { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentDeadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
