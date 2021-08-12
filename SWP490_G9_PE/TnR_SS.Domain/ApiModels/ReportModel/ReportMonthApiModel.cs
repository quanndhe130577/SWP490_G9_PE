using System;
using System.Collections.Generic;
using TnR_SS.Domain.ApiModels.CostIncurredModel;

namespace TnR_SS.Domain.ApiModels.ReportModel
{
    public class ReportMonthApiModel
    {
        public DailyData DailyData { get; set; }
        public List<CostIncurredApiModel> ListCostIncurred { get; set; } = new List<CostIncurredApiModel>();
        public double SummaryDailyCost { get; set; }
        public double SummaryIncome { get; set; }
        public double SummaryOutcome { get; set; }
        public double SummaryDebt { get; set; }
    }

    public class DailyData
    {
        // Trader
        public List<TraderDailyData> ListTraderData { get; set; } = new List<TraderDailyData>();
        // Weight Recorder
        public List<WeightRecorderDailyData> ListWRData { get; set; } = new List<WeightRecorderDailyData>();
    }

    public class TraderDailyData
    {
        public DateTime Date { get; set; }
        public double TotalIncome { get; set; }
        public double TotalOutcome { get; set; }
    }


    public class WeightRecorderDailyData
    {
        public DateTime Date { get; set; }
        public double TotalIncome { get; set; }
    }
}



