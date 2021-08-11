using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.FishTypeModel;

namespace TnR_SS.Domain.ApiModels.PurchaseDetailModel
{
    public class SummaryFishTypePurchaseModel
    {
        public int Idx { get; set; }
        public FishTypeApiModel FishType { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
    }
}
