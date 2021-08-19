using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.AccountModel.ResponseModel;
using TnR_SS.Domain.ApiModels.BuyerModel;
using TnR_SS.Domain.ApiModels.DebtModel;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.PurchaseModal;
using TnR_SS.Domain.ApiModels.TransactionDetailModel;
using TnR_SS.Domain.ApiModels.TransactionModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task<List<DebtApiModel>> GetAllDebtTraderAsync(int traderId)
        {
            PondOwner pondOwner = new();
            List<DebtApiModel> list = new();

            var listPurchase = _unitOfWork.Purchases.GetAll(x => x.TraderID == traderId)
                .Where(x => x.isCompleted == Entities.PurchaseStatus.Completed && x.isPaid == false)
                .OrderByDescending(x => x.Date).ThenByDescending(x => x.ID);
            var user = await _unitOfWork.UserInfors.FindAsync(traderId);
            foreach (var purchase in listPurchase)
            {
                DebtApiModel model = new();
                pondOwner = await _unitOfWork.PondOwners.FindAsync(purchase.PondOwnerID);
                model.Creditors = pondOwner.Name;
                model.Debtor = user.LastName + " " + user.FirstName;
                model.DebtMoney = purchase.PayForPondOwner;
                model.Date = purchase.Date;

                list.Add(model);
            }

            return list;
        }

        public async Task<List<DebtApiModel>> GetAllDebtWRAsync(int userId, DateTime? date)
        {
            List<DebtApiModel> list = new();
            var roleUser = await _unitOfWork.UserInfors.GetRolesAsync(userId);
            List<TransactionDetail> listTranDe = new List<TransactionDetail>();           

            if (roleUser.Contains(RoleName.WeightRecorder))
            {
                listTranDe = _unitOfWork.TransactionDetails.GetAllByWcIDAndDate(userId, date).Where(x => x.IsPaid == false).ToList();
            }
            else if (roleUser.Contains(RoleName.Trader))
            {
                listTranDe = _unitOfWork.TransactionDetails.GetAllByTraderIdAndDate(userId, date).Where(x => x.IsPaid == false).ToList();
            }
            else
            {
                throw new Exception("Người mua không tồn tại !!");
            }
            var user = await _unitOfWork.UserInfors.FindAsync(userId);

            foreach (var td in listTranDe)
            {
                DebtApiModel model = new();

                model.Creditors = user.LastName + " " + user.FirstName;
                var buyer = await _unitOfWork.Buyers.FindAsync(td.BuyerId);
                model.Debtor = buyer != null ? _mapper.Map<Buyer, BuyerApiModel>(buyer).Name : null;
                model.DebtMoney = td.SellPrice;
                model.Date = _mapper.Map<Transaction, TransactionResModel>(await _unitOfWork.Transactions.FindAsync(td.TransId)).Date;

                list.Add(model);
            }
            return list;
        }

        public async Task<List<DebtApiModel>> GetDebtAsync(int userId, DateTime? date)
        {
            var roleUser = await _unitOfWork.UserInfors.GetRolesAsync(userId);
            if (roleUser.Contains(RoleName.WeightRecorder))
            {
                return await GetAllDebtWRAsync(userId, date);
            }
            else if (roleUser.Contains(RoleName.Trader))
            {
                return await GetAllDebtTraderAsync(userId);
            }
            return null;
        }

        public async Task<List<DebtTraderApiModel>> GetAllDebtTransactionOfTrader(int id)
        {
            UserResModel user = await GetUserByIdAsync(id);
            List<TransactionDetail> transactionDetails = new List<TransactionDetail>();
            if (user.RoleName == "Trader")
            {
                foreach (Transaction transaction in _unitOfWork.Transactions.GetAll(filter: t => t.TraderId == id && t.WeightRecorderId == null, orderBy: ps => ps.OrderByDescending(p => p.Date)))
                {
                    transactionDetails.AddRange(_unitOfWork.TransactionDetails.GetAll(td => td.TransId == transaction.ID && td.IsPaid == false));
                }
            }
            else
            {
                foreach (Transaction transaction in _unitOfWork.Transactions.GetAll(filter: t => t.WeightRecorderId == id, orderBy: ps => ps.OrderByDescending(p => p.Date)))
                {
                    transactionDetails.AddRange(_unitOfWork.TransactionDetails.GetAll(td => td.TransId == transaction.ID && td.IsPaid == false));
                }
            }
            List<DebtTraderApiModel> debtTraderApiModels = new List<DebtTraderApiModel>();
            foreach (TransactionDetail transactionDetail in transactionDetails)
            {
                Buyer buyer = await _unitOfWork.Buyers.FindAsync(transactionDetail.BuyerId);
                FishType fishType = await _unitOfWork.FishTypes.FindAsync(transactionDetail.FishTypeId);
                Transaction transaction = await _unitOfWork.Transactions.FindAsync(transactionDetail.TransId);
                debtTraderApiModels.Add(new DebtTraderApiModel()
                {
                    ID = transactionDetail.ID,
                    Partner = buyer == null ? null : buyer.Name,
                    FishName = fishType == null ? null : fishType.FishName,
                    Weight = transactionDetail.Weight,
                    Trader = user.FirstName + " " + user.LastName,
                    Amount = transactionDetail.SellPrice * transactionDetail.Weight,
                    Date = transaction.Date
                });
            }
            return debtTraderApiModels;
        }

        public async Task UpdateDebtTransationDetail(int userId, int id)
        {
            UserResModel user = await GetUserByIdAsync(userId);
            TransactionDetail transactionDetail = await _unitOfWork.TransactionDetails.FindAsync(id);
            if (transactionDetail != null)
            {
                if (user.RoleName == "Trader")
                {
                    Transaction transaction = await _unitOfWork.Transactions.FindAsync(transactionDetail.TransId);
                    if (transaction.TraderId == userId)
                    {
                        transactionDetail.IsPaid = true;
                        _unitOfWork.TransactionDetails.Update(transactionDetail);
                        await _unitOfWork.SaveChangeAsync();
                    }
                }
                else
                {
                    Transaction transaction = await _unitOfWork.Transactions.FindAsync(transactionDetail.TransId);
                    if (transaction.WeightRecorderId == userId)
                    {
                        transactionDetail.IsPaid = true;
                        _unitOfWork.TransactionDetails.Update(transactionDetail);
                        await _unitOfWork.SaveChangeAsync();
                    }
                }
            }
        }
        public async Task<List<DebtTraderApiModel>> GetAllDebtPurchaseOfTrader(int id)
        {
            UserResModel trader = await GetUserByIdAsync(id);
            List<DebtTraderApiModel> debtTraderApiModels = new List<DebtTraderApiModel>();
            foreach (Purchase purchase in _unitOfWork.Purchases.GetAll(filter: p => p.TraderID == id && p.isPaid == false && p.isCompleted == PurchaseStatus.Completed,
              orderBy: ps => ps.OrderByDescending(p => p.Date)))
            {
                PondOwner pondOwner = await _unitOfWork.PondOwners.FindAsync(purchase.PondOwnerID);
                double amount = await CalculatePayForPondOwnerAsync(purchase.ID, purchase.Commission);
                debtTraderApiModels.Add(new DebtTraderApiModel()
                {
                    ID = purchase.ID,
                    Partner = pondOwner.Name,
                    Trader = trader.FirstName + " " + trader.LastName,
                    Amount = amount,
                    Date = purchase.Date
                });
            }
            return debtTraderApiModels;
        }
        public async Task UpdateDebtPurchaseDetail(int userId, int id)
        {
            Purchase purchase = await _unitOfWork.Purchases.FindAsync(id);
            if (purchase != null)
            {
                if (purchase.TraderID == userId)
                {
                    purchase.isPaid = true;
                    _unitOfWork.Purchases.Update(purchase);
                    await _unitOfWork.SaveChangeAsync();
                }
            }
        }
    }
}
