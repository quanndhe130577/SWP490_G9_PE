using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.DrumModel;

namespace TnR_SS.Domain.ApiModels.TruckModel
{
    public class TruckDateModel : TruckApiModel
    {
        public List<DrumWeightModel> ListDrumWeight { get; set; }

        public TruckDateModel()
        {
            ListDrumWeight = new();
        }
    }
}
