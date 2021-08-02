using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TnR_SS.Domain.ApiModels.BuyerModel;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.UserInforModel;

namespace TnR_SS.Domain.ApiModels.TransactionModel
{
    public class TransactionResModel
    {
        public int ID { get; set; }
        public UserInformation Trader { get; set; }
        public UserInformation WeightRecorder { get; set; }
        public DateTime Date { get; set; }
        public List<TransactionDetailInformation> TransactionDetails { get; set; }
    }



    public class TransactionDetailInformation
    {
        public int ID { get; set; }

        public FishTypeApiModel FishType { get; set; }
        public BuyerApiModel Buyer { get; set; }

        public bool IsPaid { get; set; } = false;

        [Range(0, Double.MaxValue, ErrorMessage = "Giá tiền phải lớn hơn 0")]
        public double SellPrice { get; set; }

        [Range(0, Double.MaxValue, ErrorMessage = "Cân nặng phải lớn hơn 0")]
        public double Weight { get; set; } // ko bao gồm cân bì, cân của rổ
    }
}
