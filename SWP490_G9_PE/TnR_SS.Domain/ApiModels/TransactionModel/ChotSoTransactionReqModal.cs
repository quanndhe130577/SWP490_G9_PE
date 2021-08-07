using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.TransactionModel
{
    public class ChotSoTransactionReqModal
    {
        public double CommissionUnit { get; set; }
        public List<int> listTranId { get; set; }
    }
}
