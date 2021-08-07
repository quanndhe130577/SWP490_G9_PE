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
        public DateTime Date { get; set; }
        public BuyerApiModel Buyer { get; set; }
        public double MoneyPaid { get; set; }
        public double MoneyNotPaid { get; set; }
        public double TotalWeight { get; set; }
        public List<TransactionDetailInformation> TransactionDetails { get; set; }
    }
}
