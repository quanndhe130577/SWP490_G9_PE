using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.TransactionDetailModel
{
    public class CreateTransactionDetailReqModelV2
    {
        public int FishTypeId { get; set; }
        public int TraderId { get; set; }
        public int? BuyerId { get; set; }
        public bool IsPaid { get; set; } = false;

        [Range(0, Double.MaxValue, ErrorMessage = "Giá tiền phải lớn hơn 0")]
        public double SellPrice { get; set; }

        [Range(0, Double.MaxValue, ErrorMessage = "Cân nặng phải lớn hơn 0")]
        public double Weight { get; set; } // ko bao gồm cân bì, cân của rổ
        public DateTime Date { get; set; }
    }
}
