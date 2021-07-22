using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.FishTypeModel.ResponseModel
{
    public class ListFishTypeModel
    {
        public int? PurchaseId { get; set; }
        public List<FishTypeApiModel> ListFishType { get; set; }
    }
}
