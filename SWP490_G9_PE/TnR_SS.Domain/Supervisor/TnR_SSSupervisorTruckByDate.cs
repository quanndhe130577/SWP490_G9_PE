using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.TruckModel;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public List<TruckDateModel> GetAllTruckByDate(int traderId, DateTime date)
        {
            var listPurchase = _unitOfWork.Purchases.GetAll(x => x.TraderID == traderId)
                .Where(x => x.Date == date);
            var listPurchaseDetail = _unitOfWork.PurchaseDetails.GetAll();
            var listLK_Drum_PurchaseDetail = _unitOfWork.LK_PurchaseDetail_Drums.GetAll();
            var join1 = listPurchase.Join(listPurchaseDetail, a => a.ID, b => b.PurchaseId,
             (a, b) => new { ID = b.ID, Date = a.Date, TotalWeight = b.Weight}).ToList();

            return null;
        }


    }
}
