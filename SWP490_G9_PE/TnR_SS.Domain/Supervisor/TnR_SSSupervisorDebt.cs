using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.DebtModel;
using TnR_SS.Domain.ApiModels.PurchaseModal;
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
                model.Debtor = user.Lastname + user.FirstName;
                model.DebtMoney = purchase.PayForPondOwner;
                model.Date = purchase.Date;

                list.Add(model);
            }

            return list;
        }

        public List<DebtApiModel> GetAllDebtWRAsync(int wrId)
        {

            return null;
        }
    }
}
 