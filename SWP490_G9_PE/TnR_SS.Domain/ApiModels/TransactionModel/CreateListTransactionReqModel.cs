using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.TransactionModel
{
    public class CreateListTransactionReqModel
    {
        public DateTime Date { get; set; }
        public List<int> ListTraderId { get; set; }
    }
}
