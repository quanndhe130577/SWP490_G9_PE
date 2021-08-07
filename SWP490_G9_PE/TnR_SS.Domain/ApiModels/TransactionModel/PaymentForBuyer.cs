using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.BuyerModel;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.UserInforModel;

namespace TnR_SS.Domain.ApiModels.TransactionModel
{
    public class PaymentForBuyer
    {
        public DateTime Date { get; set; }
        public BuyerApiModel Buyer { get; set; }
        public double MoneyPaid { get; set; }
        public double MoneyNotPaid { get; set; }
        public double TotalMoney { get; set; }
        public double TotalWeight { get; set; }
        public List<TransactionDetailPayment> TransactionDetails { get; set; } = new List<TransactionDetailPayment>();
    }

    public class TransactionDetailPayment
    {
        public int ID { get; set; }

        public FishTypeApiModel FishType { get; set; }
        public BuyerApiModel Buyer { get; set; }

        public bool IsPaid { get; set; } = false;

        public double SellPrice { get; set; }

        public double Weight { get; set; } // ko bao gồm cân bì, cân của rổ

        public UserInformation Trader { get; set; }
    }
}
