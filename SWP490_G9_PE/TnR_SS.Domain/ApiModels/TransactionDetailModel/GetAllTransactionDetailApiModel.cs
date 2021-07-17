using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.BuyerModel;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.TransactionModel;

namespace TnR_SS.Domain.ApiModels.TransactionDetailModel
{
    public class GetAllTransactionDetailApiModel
    {
        public int ID { get; set; }

        public FishTypeApiModel FishType { get; set; }
        public TransactionResModel Transaction { get; set; }
        public BuyerApiModel Buyer { get; set; }

        public bool IsPaid { get; set; } = false;

        [Range(0, Double.MaxValue, ErrorMessage = "Giá tiền phải lớn hơn 0")]
        public double SellPrice { get; set; }

        [Range(0, Double.MaxValue, ErrorMessage = "Cân nặng phải lớn hơn 0")]
        public double Weight { get; set; } // ko bao gồm cân bì, cân của rổ
    }
}
