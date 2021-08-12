using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.CostIncurredModel;
using TnR_SS.Domain.ApiModels.PondOwnerModel;
using TnR_SS.Domain.ApiModels.PurchaseDetailModel;
using TnR_SS.Domain.ApiModels.TransactionDetailModel;
using TnR_SS.Domain.ApiModels.UserInforModel;

namespace TnR_SS.Domain.ApiModels.ReportModel
{
    public class ReportMonthApiModel
    {
        /*public ReportPurchaseModal PurchaseTotal { get; set; }
        public ReportTransactionModal TransactionTotal { get; set; }
        public List<CostIncurredApiModel> ListCostIncurred { get; set; } = new List<CostIncurredApiModel>();*/
        public double TongChi { get; set; }
        public double TongThu { get; set; }
        public DateTime Date { get; set; }
    }

    /*public class ReportPurchaseModal
    {
        public List<SummaryPurchaseModal> ListSummaryPurchaseDetail { get; set; } = new List<SummaryPurchaseModal>();
        public double SummaryWeight { get; set; }
        public double SummaryMoney { get; set; }
    }

    public class SummaryPurchaseModal
    {
        public PondOwnerApiModel PondOwner { get; set; }
        public double TotalWeight { get; set; }
        public double TotalMoney { get; set; }
        public List<SummaryFishTypePurchaseModel> PurchaseDetails { get; set; } = new List<SummaryFishTypePurchaseModel>();
    }

    public class ReportTransactionModal
    {
        public List<SummaryTransactionModal> ListSummaryTransactionDetail { get; set; } = new List<SummaryTransactionModal>();
        public double SummaryWeight { get; set; }
        public double SummaryMoney { get; set; }
        public double SummaryCommission { get; set; }
    }

    public class SummaryTransactionModal
    {
        public UserInformation WeightRecorder { get; set; }
        public UserInformation Trader { get; set; }
        public double TotalWeight { get; set; }
        public double TotalMoney { get; set; }
        public double TotalCommission { get; set; }
        public List<SummaryFishTypeTransactionModel> TransactionDetails { get; set; } = new List<SummaryFishTypeTransactionModel>();
    }*/
}



