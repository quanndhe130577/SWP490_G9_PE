using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.TruckModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.ApiModels.DrumModel
{
    public class LK_Drum_Truck
    {
        public Drum Drum { get; set; }
        public Truck Truck { get; set; }
    }
}
