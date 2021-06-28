using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.DrumModel
{
    public class DrumApiModel
    {
        public int ID { get; set; }

        public int TruckId { get; set; }

        public string Number { get; set; }

        public string Type { get; set; }
    }
}
