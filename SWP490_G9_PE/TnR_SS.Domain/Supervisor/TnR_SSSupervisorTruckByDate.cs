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
        public async Task<List<TruckDateModel>> GetAllTruckByDateAsync(int traderId, DateTime date)
        {
           /* var query = _unitOfWork.Purchases.
                Join(_unitOfWork.PurchaseDetails, a => a.ID, b => b.ID,
                (a,b) => new { Purchases = a, PurchaseDetails = b})
                .Where(x => x.Date == date).Select(x => x.ID).ToList();*/

            return null;
        }


    }
}
