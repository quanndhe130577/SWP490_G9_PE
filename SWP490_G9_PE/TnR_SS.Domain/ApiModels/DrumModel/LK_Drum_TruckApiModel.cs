using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.TruckModel;

namespace TnR_SS.Domain.ApiModels.DrumModel
{
    public class LK_Drum_TruckApiModel
    {
        public DrumApiModel Drum { get; set; }
        public TruckApiModel Truck { get; set; }
    }
}
