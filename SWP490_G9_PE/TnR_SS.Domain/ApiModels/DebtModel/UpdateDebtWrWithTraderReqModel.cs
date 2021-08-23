using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TnR_SS.Domain.ApiModels.DebtModel
{
    public class UpdateDebtWrWithTraderReqModel
    {
        public int TransId { get; set; }
        public double Amount { get; set; }
    }
}
