using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.DebtModel;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public List<DebtApiModel> GetAllDebtTrader(int traderId)
        {
            List<DebtApiModel> debtApis = new();
            DebtApiModel model = new();
            var listPurchaseDetail = _unitOfWork.PurchaseDetails.GetAllPurchaseDetailByTrader(traderId);

            foreach(var item in listPurchaseDetail)
            {
                model.Creditors = item.Purchase.PondOwner.Name;
                model.Debtor = item.Purchase.UserInfor.FirstName + " " + item.Purchase.UserInfor.Lastname;
                model.DebtMoney = item.Purchase.PayForPondOwner;
                model.Date = item.Purchase.Date;
                debtApis.Add(model);
            }

            return debtApis;
        }
    }
}
