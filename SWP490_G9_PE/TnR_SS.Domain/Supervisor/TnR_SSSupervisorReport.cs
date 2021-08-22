using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.CostIncurredModel;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.PondOwnerModel;
using TnR_SS.Domain.ApiModels.PurchaseDetailModel;
using TnR_SS.Domain.ApiModels.ReportModel;
using TnR_SS.Domain.ApiModels.TransactionDetailModel;
using TnR_SS.Domain.ApiModels.UserInforModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task<ReportDayApiModel> GetReportForDayAsync(DateTime date, int userId)
        {
            ReportDayApiModel reportApiModel = new ReportDayApiModel();
            DateTime closestDate = date;
            var role = await _unitOfWork.UserInfors.GetRolesAsync(userId);
            bool isTrader = true;
            if (role.Contains(RoleName.Trader))
            {
                isTrader = true;
            }
            else
            {
                isTrader = false;
            }

            // Purchase
            if (isTrader)
            {
                var listPurchase = _unitOfWork.Purchases.GetAll(x => x.Date.Date == date.Date && x.TraderID == userId);
                if (listPurchase == null || listPurchase.Count() == 0)
                {
                    closestDate = _unitOfWork.Purchases.GetAll(x => x.Date.Date <= date.Date).Select(x => x.Date.Date).OrderByDescending(x => x.Date).FirstOrDefault();
                    if (closestDate != DateTime.MinValue)
                    {
                        listPurchase = _unitOfWork.Purchases.GetAll(x => x.Date.Date == closestDate.Date && x.TraderID == userId);
                    }
                }

                if (listPurchase == null || listPurchase.Count() == 0)
                {
                    closestDate = _unitOfWork.Purchases.GetAll(x => x.Date.Date >= date.Date).Select(x => x.Date.Date).OrderBy(x => x.Date).FirstOrDefault();
                    if (closestDate != DateTime.MinValue)
                    {
                        listPurchase = _unitOfWork.Purchases.GetAll(x => x.Date.Date == closestDate.Date && x.TraderID == userId);
                    }
                }

                if (listPurchase == null || listPurchase.Count() == 0)
                {
                    throw new Exception("Ngày được chọn không có dữ liệu !!");
                }

                reportApiModel.PurchaseTotal = new ReportPurchaseModal();
                foreach (var purchase in listPurchase.Where(x => x.isCompleted != PurchaseStatus.Remain))
                {
                    SummaryPurchaseModal summary = new SummaryPurchaseModal();
                    summary.PondOwner = _mapper.Map<PondOwner, PondOwnerApiModel>(await _unitOfWork.PondOwners.FindAsync(purchase.PondOwnerID));
                    if (purchase.isCompleted == PurchaseStatus.Completed)
                    {
                        var listCPD = _unitOfWork.ClosePurchaseDetails.GetAllByPurchase(purchase);
                        var listFishtype = listCPD.Select(x => new
                        {
                            x.FishName,
                            x.FishTypeDescription,
                            x.FishTypeId,
                            x.FishTypeMaxWeight,
                            x.FishTypeMinWeight,
                            x.FishTypePrice,
                            x.FishTypeTransactionPrice
                        }).Distinct();
                        int count = 0;
                        foreach (var fishtype in listFishtype)
                        {
                            SummaryFishTypePurchaseModel pdM = new SummaryFishTypePurchaseModel();
                            pdM.Idx = count++;
                            pdM.FishType = new FishTypeApiModel()
                            {
                                ID = fishtype.FishTypeId,
                                FishName = fishtype.FishName,
                                Description = fishtype.FishTypeDescription,
                                MinWeight = fishtype.FishTypeMinWeight,
                                MaxWeight = fishtype.FishTypeMaxWeight,
                                Price = fishtype.FishTypePrice,
                                TransactionPrice = fishtype.FishTypeTransactionPrice
                            };

                            pdM.Price = listCPD.Where(x => x.FishName == fishtype.FishName).Sum(x => x.FishTypePrice * (x.Weight - x.BasketWeight));
                            pdM.Weight = listCPD.Where(x => x.FishName == fishtype.FishName).Sum(x => x.Weight - x.BasketWeight);

                            summary.TotalWeight += pdM.Weight;
                            summary.TotalMoney += pdM.Price;

                            summary.PurchaseDetails.Add(pdM);
                        }
                    }
                    else if (purchase.isCompleted == PurchaseStatus.Pending)
                    {
                        var listPD = _unitOfWork.PurchaseDetails.GetAll(x => x.PurchaseId == purchase.ID);
                        var listFishTypeId = listPD.Select(x => x.FishTypeID).Distinct();
                        int count = 0;
                        foreach (var fishTypeId in listFishTypeId)
                        {
                            var fishType = await _unitOfWork.FishTypes.FindAsync(fishTypeId);
                            SummaryFishTypePurchaseModel pdM = new SummaryFishTypePurchaseModel();
                            pdM.Idx = count++;
                            pdM.FishType = _mapper.Map<FishType, FishTypeApiModel>(fishType);
                            pdM.Weight = _unitOfWork.FishTypes.GetTotalWeightOfFishType(fishTypeId);
                            pdM.Price = fishType.Price * pdM.Weight;

                            summary.TotalWeight += pdM.Weight;
                            summary.TotalMoney += pdM.Price;

                            summary.PurchaseDetails.Add(pdM);
                        }
                    }

                    reportApiModel.PurchaseTotal.ListSummaryPurchaseDetail.Add(summary);
                }

                // Cá cũ
                foreach (var item in listPurchase.Where(x => x.isCompleted == PurchaseStatus.Remain))
                {
                    SummaryPurchaseModal summary = new SummaryPurchaseModal();
                    summary.PondOwner = new PondOwnerApiModel()
                    {
                        Name = "Cá cũ",
                        TraderID = item.TraderID,
                        PhoneNumber = "",
                        Address = ""
                    };

                    var listPD = _unitOfWork.PurchaseDetails.GetAll(x => x.PurchaseId == item.ID);
                    var listFishTypeId = listPD.Select(x => x.FishTypeID).Distinct();
                    int count = 0;
                    foreach (var fishTypeId in listFishTypeId)
                    {
                        var fishType = await _unitOfWork.FishTypes.FindAsync(fishTypeId);
                        SummaryFishTypePurchaseModel pdM = new SummaryFishTypePurchaseModel();
                        pdM.Idx = count++;
                        pdM.FishType = _mapper.Map<FishType, FishTypeApiModel>(fishType);
                        pdM.Weight = _unitOfWork.FishTypes.GetTotalWeightOfFishType(fishTypeId);
                        pdM.Price = 0 /*fishType.Price * pdM.Weight*/;

                        summary.TotalWeight += pdM.Weight;
                        summary.TotalMoney += pdM.Price;

                        summary.PurchaseDetails.Add(pdM);
                    }

                    reportApiModel.PurchaseTotal.ListSummaryPurchaseDetail.Add(summary);
                }

                reportApiModel.PurchaseTotal.SummaryWeight = reportApiModel.PurchaseTotal.ListSummaryPurchaseDetail.Sum(x => x.TotalWeight);
                reportApiModel.PurchaseTotal.SummaryMoney = reportApiModel.PurchaseTotal.ListSummaryPurchaseDetail.Sum(x => x.TotalMoney);
            }

            // Transaction
            var listTransaction = _unitOfWork.Transactions.GetAllTransactionsByDate(userId, closestDate);
            reportApiModel.TransactionTotal = new ReportTransactionModal();
            foreach (var transaction in listTransaction)
            {
                SummaryTransactionModal summary = new SummaryTransactionModal();
                summary.WeightRecorder = _mapper.Map<UserInfor, UserInformation>(await _unitOfWork.UserInfors.FindAsync(transaction.WeightRecorderId));
                summary.Trader = _mapper.Map<UserInfor, UserInformation>(await _unitOfWork.UserInfors.FindAsync(transaction.TraderId));
                if (transaction.isCompleted == TransactionStatus.Completed)
                {
                    var listCTD = _unitOfWork.CloseTransactionDetails.GetAll(x => x.TransactionId == transaction.ID);
                    // tính tổng nợ cho wr
                    reportApiModel.TongNo += listCTD.Where(x => !x.IsPaid).Sum(x => x.SellPrice * x.Weight);
                    var listFishtype = listCTD.Select(x => new
                    {
                        x.FishName,
                        x.FishTypeDescription,
                        x.FishTypeId,
                        x.FishTypeMaxWeight,
                        x.FishTypeMinWeight,
                        x.FishTypePrice
                    }).Distinct();
                    int count = 0;
                    foreach (var fishType in listFishtype)
                    {
                        SummaryFishTypeTransactionModel tdM = new SummaryFishTypeTransactionModel();
                        var listFish = listCTD.Where(x => x.FishTypeId == fishType.FishTypeId);
                        tdM.FishType = new FishTypeApiModel()
                        {
                            ID = fishType.FishTypeId,
                            FishName = fishType.FishName,
                            Description = fishType.FishTypeDescription,
                            MinWeight = fishType.FishTypeMinWeight,
                            MaxWeight = fishType.FishTypeMaxWeight,
                            Price = fishType.FishTypePrice,
                            TransactionPrice = listFish.Sum(x => x.SellPrice) / listFish.Count()
                        };
                        tdM.Idx = count++;
                        tdM.Weight = listFish.Sum(x => x.Weight);
                        tdM.SellPrice = listFish.Sum(x => x.Weight * x.SellPrice);

                        summary.TotalWeight += tdM.Weight;
                        summary.TotalMoney += tdM.SellPrice;
                        summary.TotalCommission += transaction.CommissionUnit * tdM.Weight;

                        summary.TransactionDetails.Add(tdM);
                    }
                }
                else
                {
                    var listTD = _unitOfWork.TransactionDetails.GetAll(x => x.TransId == transaction.ID);
                    // tính tổng nợ cho wr
                    reportApiModel.TongNo += listTD.Where(x => !x.IsPaid).Sum(x => x.SellPrice * x.Weight);
                    var listFishtypeId = listTD.Select(x => x.FishTypeId).Distinct();
                    int count = 0;
                    foreach (var fishTypeId in listFishtypeId)
                    {
                        SummaryFishTypeTransactionModel tdM = new SummaryFishTypeTransactionModel();
                        tdM.Idx = count++;
                        tdM.FishType = _mapper.Map<FishType, FishTypeApiModel>(await _unitOfWork.FishTypes.FindAsync(fishTypeId));
                        tdM.Weight = listTD.Where(x => x.FishTypeId == fishTypeId).Sum(x => x.Weight);
                        tdM.SellPrice = listTD.Where(x => x.FishTypeId == fishTypeId).Sum(x => x.SellPrice * x.Weight);

                        summary.TotalWeight += tdM.Weight;
                        summary.TotalMoney += tdM.SellPrice;
                        summary.TotalCommission += transaction.CommissionUnit * tdM.Weight;

                        summary.TransactionDetails.Add(tdM);
                    }
                }

                reportApiModel.TransactionTotal.ListSummaryTransactionDetail.Add(summary);
            }

            reportApiModel.TransactionTotal.SummaryWeight = reportApiModel.TransactionTotal.ListSummaryTransactionDetail.Sum(x => x.TotalWeight);
            reportApiModel.TransactionTotal.SummaryMoney = reportApiModel.TransactionTotal.ListSummaryTransactionDetail.Sum(x => x.TotalMoney);
            reportApiModel.TransactionTotal.SummaryCommission = reportApiModel.TransactionTotal.ListSummaryTransactionDetail.Sum(x => x.TotalCommission);

            // Remain
            reportApiModel.RemainTotal = new ReportRemainModal();
            var listRemain = _unitOfWork.Purchases.GetAll(x => x.Date.Date == date.Date.AddDays(1) && x.isCompleted == PurchaseStatus.Remain && x.TraderID == userId);
            foreach (var item in listRemain)
            {
                //SummaryRemainModal summary = new SummaryRemainModal();

                var listPD = _unitOfWork.PurchaseDetails.GetAll(x => x.PurchaseId == item.ID);
                var listFishTypeId = listPD.Select(x => x.FishTypeID).Distinct();
                int count = 0;
                foreach (var fishTypeId in listFishTypeId)
                {
                    var fishType = await _unitOfWork.FishTypes.FindAsync(fishTypeId);
                    SummaryFishTypePurchaseModel pdM = new SummaryFishTypePurchaseModel();
                    pdM.Idx = count++;
                    pdM.FishType = _mapper.Map<FishType, FishTypeApiModel>(fishType);
                    pdM.Weight = _unitOfWork.FishTypes.GetTotalWeightOfFishType(fishTypeId);
                    pdM.Price = fishType.Price * pdM.Weight;

                    /*summary.TotalWeight += pdM.Weight;
                    summary.TotalMoney += pdM.Price;*/
                    reportApiModel.RemainTotal.TotalWeight += pdM.Weight;
                    reportApiModel.RemainTotal.TotalMoney += pdM.Price;

                    reportApiModel.RemainTotal.RemainDetails.Add(pdM);
                }

                //reportApiModel.RemainTotal.SummaryRemainDetail = summary;
            }

            /*reportApiModel.RemainTotal.SummaryWeight = reportApiModel.RemainTotal.SummaryRemainDetail.TotalWeight;
            reportApiModel.RemainTotal.SummaryMoney = reportApiModel.RemainTotal.SummaryRemainDetail.TotalMoney;*/

            // Cost Incurred
            var listCI = _unitOfWork.CostIncurreds.GetAll(x => x.UserId == userId && x.TypeOfCost == "day" && x.Date.Date == closestDate.Date);
            foreach (var ci in listCI)
            {
                reportApiModel.ListCostIncurred.Add(_mapper.Map<CostIncurred, CostIncurredApiModel>(ci));
            }

            reportApiModel.Date = closestDate;

            // tính lỗ lãi
            if (isTrader)
            {
                reportApiModel.TongChi = (reportApiModel.PurchaseTotal != null ? reportApiModel.PurchaseTotal.SummaryMoney : 0) + reportApiModel.ListCostIncurred.Sum(x => x.Cost);
                reportApiModel.TongThu = (reportApiModel.TransactionTotal != null ? reportApiModel.TransactionTotal.SummaryMoney : 0) - reportApiModel.TransactionTotal.SummaryCommission;
                reportApiModel.TongNo = 0;
            }
            else
            {
                reportApiModel.TongChi = reportApiModel.ListCostIncurred.Sum(x => x.Cost);
                reportApiModel.TongThu = reportApiModel.TransactionTotal != null ? reportApiModel.TransactionTotal.SummaryCommission : 0;
            }

            return reportApiModel;
        }

        public async Task<ReportMonthApiModel> GetReportForMonthAsync(DateTime date, int userId)
        {
            ReportMonthApiModel reportApiModel = new ReportMonthApiModel();
            int month = date.Month;
            int year = date.Year;
            reportApiModel.DailyData = new DailyData();

            bool isTrader = true;
            var role = await _unitOfWork.UserInfors.GetRolesAsync(userId);
            if (role.Contains(RoleName.WeightRecorder))
            {
                isTrader = false;
            }

            // Daily data
            DateTime startDate = date;
            DateTime endDate = date.AddMonths(1);
            for (var item = startDate; item < endDate && item <= DateTime.Now;)
            {
                if (isTrader)
                {
                    /*TraderDailyData traderDailyData = new TraderDailyData();
                    traderDailyData.Date = item.Date;
                    traderDailyData.TotalIncome = await ReportGetTotalIncomeMonthAsync(item, userId);
                    traderDailyData.TotalOutcome = await ReportGetTotalOutcomeMonthAsync(item, userId);
                    reportApiModel.DailyData.ListTraderData.Add(traderDailyData);*/

                    TraderDailyData traderIncomeData = new TraderDailyData();
                    traderIncomeData.Date = item.Date.ToString("dd/MM/yyyy");
                    traderIncomeData.Name = DailyDataName.TotalIncome;
                    traderIncomeData.Value = await ReportGetTotalIncomeDayAsync(item, userId);
                    reportApiModel.DailyData.ListTraderData.Add(traderIncomeData);

                    TraderDailyData traderOutcomeData = new TraderDailyData();
                    traderOutcomeData.Date = item.Date.ToString("dd/MM/yyyy");
                    traderOutcomeData.Name = DailyDataName.TotalOutcome;
                    traderOutcomeData.Value = await ReportGetTotalOutcomeDayAsync(item, userId);
                    reportApiModel.DailyData.ListTraderData.Add(traderOutcomeData);
                }
                else
                {
                    WeightRecorderDailyData wrIncomeData = new WeightRecorderDailyData();
                    wrIncomeData.Date = item.Date.ToString("dd/MM/yyyy");
                    wrIncomeData.Name = DailyDataName.TotalIncome;
                    wrIncomeData.Value = await ReportGetTotalIncomeDayAsync(item, userId);
                    reportApiModel.DailyData.ListWRData.Add(wrIncomeData);

                    WeightRecorderDailyData wrOutcomeData = new WeightRecorderDailyData();
                    wrOutcomeData.Date = item.Date.ToString("dd/MM/yyyy");
                    wrOutcomeData.Name = DailyDataName.TotalOutcome;
                    wrOutcomeData.Value = await ReportGetTotalOutcomeDayAsync(item, userId);
                    reportApiModel.DailyData.ListWRData.Add(wrOutcomeData);

                    WeightRecorderDailyData wrDebtData = new WeightRecorderDailyData();
                    wrDebtData.Date = item.Date.ToString("dd/MM/yyyy");
                    wrDebtData.Name = DailyDataName.TotalDebt;
                    wrDebtData.Value = await ReportGetTotalDebtDayAsync(item, userId);
                    reportApiModel.DailyData.ListWRData.Add(wrDebtData);
                }

                item = item.AddDays(1);
            }

            // Cost Incurred
            var listCIMonth = _unitOfWork.CostIncurreds.GetAll(x => x.Date.Month == month && x.Date.Year == year && x.TypeOfCost == "month" && x.UserId == userId).ToList();
            foreach (var item in listCIMonth)
            {
                reportApiModel.ListCostIncurred.Add(_mapper.Map<CostIncurred, CostIncurredApiModel>(item));
            }
            // Total Cost
            reportApiModel.SummaryDailyCost = _unitOfWork.CostIncurreds.GetAll(x => x.Date.Month == month && x.Date.Year == year && x.TypeOfCost == "day" && x.UserId == userId).Sum(x => x.Cost);
            /*// Chi
            reportApiModel.SummaryOutcome = (isTrader ? reportApiModel.DailyData.ListTraderData.Sum(x => x.TotalOutcome) : 0) + reportApiModel.SummaryDailyCost + reportApiModel.ListCostIncurred.Sum(x => x.Cost);
            // Thu
            reportApiModel.SummaryIncome = isTrader ? reportApiModel.DailyData.ListTraderData.Sum(x => x.TotalIncome) : reportApiModel.DailyData.ListWRData.Sum(x => x.TotalIncome);*/
            // Chi
            reportApiModel.SummaryOutcome = (isTrader ? reportApiModel.DailyData.ListTraderData.Where(x => x.Name == DailyDataName.TotalOutcome).Sum(x => x.Value) : 0) + reportApiModel.SummaryDailyCost + reportApiModel.ListCostIncurred.Sum(x => x.Cost);
            // Thu
            reportApiModel.SummaryIncome = isTrader ? reportApiModel.DailyData.ListTraderData.Where(x => x.Name == DailyDataName.TotalIncome).Sum(x => x.Value) : reportApiModel.DailyData.ListWRData.Where(x => x.Name == DailyDataName.TotalIncome).Sum(x => x.Value);
            // Debt
            if (isTrader)
            {
                //var listPurchase = _unitOfWork.Purchases.GetAll(x => x.TraderID == userId && !x.isPaid);
                var listPurchase = _unitOfWork.Purchases.GetAll(x => x.TraderID == userId && x.SentMoney < x.PayForPondOwner);
                foreach (var purchase in listPurchase)
                {
                    reportApiModel.TienPhaiTra += await GetTotalAmountPurchaseAsync(purchase.ID);
                }

                var listTranId = _unitOfWork.Transactions.GetAll(x => x.TraderId == userId && x.Date.Month == date.Month && x.Date.Year == year).Select(x => x.ID);
                var listTranDe = _unitOfWork.TransactionDetails.GetAllByListTransaction(listTranId.ToList());
                reportApiModel.TienPhaiThu = listTranDe.Where(x => !x.IsPaid).Sum(x => x.SellPrice);
            }
            else
            {
                var listTranId = _unitOfWork.Transactions.GetAll(x => x.WeightRecorderId == userId && x.Date.Month == date.Month && x.Date.Year == year).Select(x => x.ID);
                var listTranDe = _unitOfWork.TransactionDetails.GetAllByListTransaction(listTranId.ToList());
                reportApiModel.TienPhaiThu = listTranDe.Where(x => !x.IsPaid).Sum(x => x.SellPrice);
            }

            return reportApiModel;
        }

        private async Task<double> ReportGetTotalIncomeDayAsync(DateTime date, int userId)
        {
            var role = await _unitOfWork.UserInfors.GetRolesAsync(userId);
            var listTran = _unitOfWork.Transactions.GetAllTransactionsByDate(userId, date);
            double totalIncome = 0;
            if (role.Contains(RoleName.Trader))
            {
                foreach (var tran in listTran)
                {
                    if (tran.isCompleted == TransactionStatus.Completed)
                    {
                        var listCTD = _unitOfWork.CloseTransactionDetails.GetAll(x => x.TransactionId == tran.ID);
                        totalIncome += listCTD.Sum(x => x.Weight * x.SellPrice - x.Weight * tran.CommissionUnit);
                    }
                    else
                    {
                        var listTranDe = _unitOfWork.TransactionDetails.GetAll(x => x.TransId == tran.ID);
                        totalIncome += listTranDe.Sum(x => x.Weight * x.SellPrice - x.Weight * tran.CommissionUnit);
                    }
                }
            }
            else
            {
                foreach (var tran in listTran)
                {
                    if (tran.isCompleted == TransactionStatus.Completed)
                    {
                        var listCTD = _unitOfWork.CloseTransactionDetails.GetAll(x => x.TransactionId == tran.ID);
                        totalIncome += listCTD.Sum(x => x.Weight * tran.CommissionUnit);
                        //totalIncome += listCTD.Sum(x => x.Weight * x.SellPrice);
                    }
                    else
                    {
                        var listTranDe = _unitOfWork.TransactionDetails.GetAll(x => x.TransId == tran.ID);
                        totalIncome += listTranDe.Sum(x => listTran.Find(y => y.ID == x.TransId).CommissionUnit * x.Weight);
                        //totalIncome += listTranDe.Sum(x => x.SellPrice * x.Weight);
                    }
                }
            }

            return totalIncome;
        }

        private async Task<double> ReportGetTotalOutcomeDayAsync(DateTime date, int userId)
        {
            var role = await _unitOfWork.UserInfors.GetRolesAsync(userId);
            double totalOutcome = 0;
            if (role.Contains(RoleName.Trader))
            {
                var listPurchase = _unitOfWork.Purchases.GetAll(x => x.TraderID == userId && x.Date.Date == date && x.isCompleted != PurchaseStatus.Remain);
                foreach (var purchase in listPurchase)
                {
                    /* if (purchase.isCompleted == PurchaseStatus.Completed)
                     {
                         var listCPD = _unitOfWork.ClosePurchaseDetails.GetAllByPurchase(purchase);
                         totalOutcome += listCPD.Sum(x => (x.Weight - x.BasketWeight) * x.FishTypePrice);
                     }
                     else
                     {
                         var listPD = _unitOfWork.PurchaseDetails.GetAll(x => x.PurchaseId == purchase.ID);
                         foreach (var pd in listPD)
                         {
                             totalOutcome += GetTotalWeightPurchase
                         }
                     }*/
                    totalOutcome += await GetTotalAmountPurchaseAsync(purchase.ID);
                }
            }

            totalOutcome += _unitOfWork.CostIncurreds.GetAll(x => x.Date.Date == date.Date && x.UserId == userId && x.TypeOfCost == "day").Sum(x => x.Cost);
            return totalOutcome;
        }

        private async Task<double> ReportGetTotalDebtDayAsync(DateTime date, int userId)
        {
            var role = await _unitOfWork.UserInfors.GetRolesAsync(userId);
            double totalDebt = 0;
            if (role.Contains(RoleName.Trader))
            {
                /*var listPurchase = _unitOfWork.Purchases.GetAll(x => x.TraderID == userId && x.Date.Date == date);
                foreach (var purchase in listPurchase)
                {
                    *//* if (purchase.isCompleted == PurchaseStatus.Completed)
                     {
                         var listCPD = _unitOfWork.ClosePurchaseDetails.GetAllByPurchase(purchase);
                         totalOutcome += listCPD.Sum(x => (x.Weight - x.BasketWeight) * x.FishTypePrice);
                     }
                     else
                     {
                         var listPD = _unitOfWork.PurchaseDetails.GetAll(x => x.PurchaseId == purchase.ID);
                         foreach (var pd in listPD)
                         {
                             totalOutcome += GetTotalWeightPurchase
                         }
                     }*//*
                    totalOutcome += await GetTotalAmountPurchaseAsync(purchase.ID);
                }*/
                return 0;
            }
            else
            {
                var listTran = _unitOfWork.Transactions.GetAllTransactionsByDate(userId, date.Date);
                foreach (var tran in listTran)
                {
                    if (tran.isCompleted == TransactionStatus.Completed)
                    {
                        var listCTD = _unitOfWork.CloseTransactionDetails.GetAll(x => x.TransactionId == tran.ID && !x.IsPaid);
                        totalDebt += listCTD.Sum(x => x.Weight * x.SellPrice);
                        //totalIncome += listCTD.Sum(x => x.Weight * x.SellPrice);
                    }
                    else
                    {
                        var listTranDe = _unitOfWork.TransactionDetails.GetAll(x => x.TransId == tran.ID && !x.IsPaid);
                        totalDebt += listTranDe.Sum(x => x.SellPrice * x.Weight);
                        //totalIncome += listTranDe.Sum(x => x.SellPrice * x.Weight);
                    }
                }
                return totalDebt;
            }
        }
    }
}
