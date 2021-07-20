using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                model.Debtor = user.Lastname + " " + user.FirstName;
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
                listTranDe = _unitOfWork.TransactionDetails.GetAllTransactionByWcIDAndDate(userId, date).Where(x => x.IsPaid == false).ToList();
            }
            else if (roleUser.Contains(RoleName.Trader))
            {
                listTranDe = _unitOfWork.TransactionDetails.GetAllTransactionByTraderIdAndDate(userId, date).Where(x => x.IsPaid == false).ToList();
            }
            else
            {
                throw new Exception("Người mua không tồn tại !!");
            }
            var user = await _unitOfWork.UserInfors.FindAsync(userId);
            
            foreach (var td in listTranDe)
            {
                DebtApiModel model = new();

                model.Creditors = user.Lastname + " " + user.FirstName;
                model.Debtor = _mapper.Map<Buyer, BuyerApiModel>(await _unitOfWork.Buyers.FindAsync(td.BuyerId)).Name;
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
    }
}
 