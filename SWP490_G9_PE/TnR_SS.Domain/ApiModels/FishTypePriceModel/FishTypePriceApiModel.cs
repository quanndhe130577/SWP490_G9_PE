using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.FishTypePriceModel
{
    public class FishTypePriceApiModel
    {
        public int ID { get; set; }
        public int FishTypeID { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
    }
}
