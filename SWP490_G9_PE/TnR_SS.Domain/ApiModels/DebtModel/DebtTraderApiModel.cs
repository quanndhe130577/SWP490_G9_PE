using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.DebtModel
{
    public class DebtTraderApiModel
    {
        public int ID { get; set; }
        public string Trader { get; set; }

        public string Partner { get; set; }
        public string FishName { get; set; }
        public double Weight { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; }

    }
}
