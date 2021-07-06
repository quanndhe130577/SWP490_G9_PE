using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.DrumModel;

namespace TnR_SS.Domain.ApiModels.TruckModel
{
    public class TruckDateModel
    {
        public int Id { get; set; }
        public List<DrumWeightModel> ListDrumWeight { get; set; }

        public TruckDateModel()
        {
            ListDrumWeight = new();
        }
    }
}
