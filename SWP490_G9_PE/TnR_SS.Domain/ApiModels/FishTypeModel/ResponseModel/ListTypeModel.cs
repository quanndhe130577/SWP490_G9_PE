using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.FishTypeModel.ResponseModel
{
    public class ListTypeModel
    {
        public List<FishTypeWithPriceResModel> ListFishType { get; set; }
    }
}
