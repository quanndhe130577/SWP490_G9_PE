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
        public async Task<ReportDayApiModel> GetReportAsync(DateTime date, int userId)
        {
            return await GetReportForDayAsync(date, userId);
        }

        private async Task<ReportDayApiModel> GetReportForDayAsync(DateTime date, int userId)
        {
            ReportDayApiModel reportApiModel = new ReportDayApiModel();
            DateTime closestDate = date;
            // Purchase
            var listPurchase = _unitOfWork.Purchases.GetAll(x => x.Date.Date == date.Date && x.TraderID == userId);
            if (listPurchase.Count() == 0)
            {
                closestDate = _unitOfWork.Purchases.GetAll(x => x.Date.Date <= date.Date).Select(x => x.Date.Date).OrderByDescending(x => x.Date).FirstOrDefault();
                listPurchase = _unitOfWork.Purchases.GetAll(x => x.Date.Date == closestDate.Date && x.TraderID == userId);
            }

            reportApiModel.PurchaseTotal = new ReportPurchaseModal();
            foreach (var purchase in listPurchase)
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
                    foreach (var fishtype in listFishtype)
                    {
                        SummaryFishTypePurchaseModel pdM = new SummaryFishTypePurchaseModel();
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

                        pdM.Price = listCPD.Where(x => x.FishName == fishtype.FishName).Sum(x => x.FishTypePrice * x.Weight);
                        pdM.Weight = listCPD.Where(x => x.FishName == fishtype.FishName).Sum(x => x.Weight);

                        summary.TotalWeight += pdM.Weight;
                        summary.TotalMoney += pdM.Price;

                        summary.PurchaseDetails.Add(pdM);
                    }
                }
                else
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
                        x.FishTypePrice,
                        x.SellPrice
                    }).Distinct();
                    int count = 0;
                    foreach (var fishType in listFishtype)
                    {
                        SummaryFishTypeTransactionModel tdM = new SummaryFishTypeTransactionModel();
                        tdM.FishType = new FishTypeApiModel()
                        {
                            ID = fishType.FishTypeId,
                            FishName = fishType.FishName,
                            Description = fishType.FishTypeDescription,
                            MinWeight = fishType.FishTypeMinWeight,
                            MaxWeight = fishType.FishTypeMaxWeight,
                            Price = fishType.FishTypePrice,
                            TransactionPrice = fishType.SellPrice
                        };
                        tdM.Idx = count++;
                        tdM.Weight = listCTD.Where(x => x.FishName == fishType.FishName).Sum(x => x.Weight);
                        tdM.SellPrice = fishType.SellPrice * tdM.Weight;


                        summary.TotalWeight += tdM.Weight;
                        summary.TotalMoney += tdM.SellPrice;

                        summary.TransactionDetails.Add(tdM);
                    }
                }
                else
                {
                    var listTD = _unitOfWork.TransactionDetails.GetAll(x => x.TransId == transaction.ID);
                    // tính tổng nợ cho wr
                    reportApiModel.TongNo += listTD.Where(x => !x.IsPaid).Sum(x => x.SellPrice * x.Weight);
                    var listFishtypeId = listTD.Select(x => x.FishTypeId).Distinct();
                    foreach (var fishTypeId in listFishtypeId)
                    {
                        SummaryFishTypeTransactionModel tdM = new SummaryFishTypeTransactionModel();
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
                reportApiModel.TransactionTotal.SummaryWeight = reportApiModel.TransactionTotal.ListSummaryTransactionDetail.Sum(x => x.TotalWeight);
                reportApiModel.TransactionTotal.SummaryMoney = reportApiModel.TransactionTotal.ListSummaryTransactionDetail.Sum(x => x.TotalMoney);
                reportApiModel.TransactionTotal.SummaryCommission = reportApiModel.TransactionTotal.ListSummaryTransactionDetail.Sum(x => x.TotalCommission);
            }

            // Cost Incurred
            var listCI = _unitOfWork.CostIncurreds.GetAll(x => x.UserId == userId && x.TypeOfCost == "day");
            foreach (var ci in listCI)
            {
                reportApiModel.ListCostIncurred.Add(_mapper.Map<CostIncurred, CostIncurredApiModel>(ci));
            }

            reportApiModel.Date = closestDate;

            // tính lỗ lãi
            var role = await _unitOfWork.UserInfors.GetRolesAsync(userId);
            if (role.Contains(RoleName.Trader))
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
    }
}
