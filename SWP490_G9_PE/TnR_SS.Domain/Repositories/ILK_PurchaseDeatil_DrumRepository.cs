using System.Collections.Generic;
using TnR_SS.Domain.ApiModels.DrumModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Repositories
{
    public interface ILK_PurchaseDeatil_DrumRepository : IRepositoryBase<LK_PurchaseDeatil_Drum>
    {
        void RemoveLKByPurchaseDetailId(int purchaseDetailId);
    }
}
