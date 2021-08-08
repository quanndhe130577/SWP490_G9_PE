using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.TransactionDetailModel
{
    public class FinishPaymentBuyerReqModel
    {
        public DateTime Date { get; set; }
        public int BuyerId { get; set; }
    }
}
