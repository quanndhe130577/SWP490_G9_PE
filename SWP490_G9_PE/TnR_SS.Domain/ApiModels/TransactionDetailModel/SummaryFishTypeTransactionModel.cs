using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.FishTypeModel;

namespace TnR_SS.Domain.ApiModels.TransactionDetailModel
{
    public class SummaryFishTypeTransactionModel
    {
        public int Idx { get; set; }
        public FishTypeApiModel FishType { get; set; }

        public double SellPrice { get; set; }

        public double Weight { get; set; } // ko bao gồm cân bì, cân của rổ
    }
}
