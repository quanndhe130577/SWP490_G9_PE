using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.UserInforModel;

namespace TnR_SS.Domain.ApiModels.DebtModel
{
    public class GetDebtForWrWithTraderResModel
    {
        public int TransID { get; set; }
        public string Partner { get; set; }
        public DateTime Date { get; set; }
        public double SentMoney { get; set; }
        public double TotalMoney { get; set; }
    }
}
