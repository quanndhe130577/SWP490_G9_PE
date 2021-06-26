using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.BasketModel.ResponseModel;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.ApiModels.PurchaseDetailModel
{
    public class PurchaseDetailResModel
    {
        public int ID { get; set; }
        public BasketApiModel Basket { get; set; }
        public FishTypeApiModel FishType { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }
    }
}
