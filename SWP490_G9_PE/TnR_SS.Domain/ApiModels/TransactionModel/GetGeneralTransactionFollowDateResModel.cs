using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.UserInforModel;

namespace TnR_SS.Domain.ApiModels.TransactionModel
{
    public class GetGeneralTransactionFollowDateResModel
    {
        public DateTime Date { get; set; }
        public double TotalWeight { get; set; }
        public double TotalMoney { get; set; }
        public double TotalDebt { get; set; }
        public List<UserInformation> ListTrader { get; set; } = new List<UserInformation>();
        public List<UserInformation> ListWeightRecorder { get; set; } = new List<UserInformation>();
    }
}