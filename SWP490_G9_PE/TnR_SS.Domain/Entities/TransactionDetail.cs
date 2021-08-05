using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.Entities
{
    public class TransactionDetail : BaseEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int FishTypeId { get; set; }
        public FishType FishType { get; set; }

        [Required]
        public int TransId { get; set; }
        public Transaction Transaction { get; set; }

        public int? BuyerId { get; set; }
        public Buyer Buyer { get; set; }

        public bool IsPaid { get; set; } = false;

        [Range(0, Double.MaxValue, ErrorMessage = "Giá tiền phải lớn hơn 0")]
        public double SellPrice { get; set; }

        [Range(0, Double.MaxValue, ErrorMessage = "Cân nặng phải lớn hơn 0")]
        public double Weight { get; set; } // ko bao gồm cân bì, cân của rổ

        public CloseTransactionDetail CloseTransactionDetail { get; set; }
    }
}
