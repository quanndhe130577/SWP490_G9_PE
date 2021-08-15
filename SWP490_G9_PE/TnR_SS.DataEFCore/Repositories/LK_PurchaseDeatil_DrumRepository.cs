using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.DrumModel;
using TnR_SS.Domain.ApiModels.LK_PurchaseDetail_DrumModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class LK_PurchaseDeatil_DrumRepository : RepositoryBase<LK_PurchaseDeatil_Drum>, ILK_PurchaseDeatil_DrumRepository
    {
        public LK_PurchaseDeatil_DrumRepository(TnR_SSContext context) : base(context) { }

        public void RemoveLKByPurchaseDetailId(int purchaseDetailId)
        {
            var list = _context.LK_PurchaseDeatil_Drums.Where(x => x.PurchaseDetailID == purchaseDetailId && x.ClosePurchaseDetailID == null);
            _context.LK_PurchaseDeatil_Drums.RemoveRange(list);

            var list2 = _context.LK_PurchaseDeatil_Drums.Where(x => x.PurchaseDetailID == purchaseDetailId && x.ClosePurchaseDetailID != null);
            foreach (var item in list2)
            {
                item.PurchaseDetailID = null;
            }

            _context.LK_PurchaseDeatil_Drums.UpdateRange(list2);
        }


        public void AddClosePurchaseDetailId(int purchaseDetailId, int closePurchaseDetailId)
        {
            var list = _context.LK_PurchaseDeatil_Drums.Where(x => x.PurchaseDetailID == purchaseDetailId);
            foreach (var item in list)
            {
                item.ClosePurchaseDetailID = closePurchaseDetailId;
            }

            _context.LK_PurchaseDeatil_Drums.UpdateRange(list);
        }
    }
}
