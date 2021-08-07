using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.BuyerModel;

namespace TnR_SS.Domain.ApiModels.TransactionModel
{
    public class PaymentForBuyer
    {
        public BuyerApiModel Buyer { get; set; }
        public double Paid { get; set; }
        public double NotPaid { get; set; }
        public double TotalWeight { get; set; }
        public List<TransactionDetailInformation> TransactionDetails { get; set; }
    }
}
