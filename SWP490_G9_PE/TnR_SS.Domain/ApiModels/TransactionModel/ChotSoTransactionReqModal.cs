using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.TransactionModel
{
    public class ChotSoTransactionReqModal
    {
        public DateTime Date { get; set; }
        public double CommissionUnit { get; set; }
        public int TranId { get; set; }
        public List<RemainFish> ListRemainFish { get; set; } = new List<RemainFish>();
    }

    public class RemainFish
    {
        public int ID { get; set; }
        public double Weight { get; set; }
    }
}
