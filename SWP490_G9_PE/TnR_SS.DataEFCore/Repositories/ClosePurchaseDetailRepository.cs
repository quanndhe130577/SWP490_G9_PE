using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class ClosePurchaseDetailRepository : RepositoryBase<ClosePurchaseDetail>, IClosePurchaseDetailRepository
    {
        public ClosePurchaseDetailRepository(TnR_SSContext context) : base(context) { }

        public List<ClosePurchaseDetail> GetAllByPurchase(Purchase purchase)
        {
            var rs = _context.PurchaseDetails.Where(x => x.PurchaseId == purchase.ID)
                .Join(
                    _context.ClosePurchaseDetails,
                    pd => pd.ID,
                    cpd => cpd.PurchaseDetailId,
                    (pd, cpd) => cpd
                ).ToList();
            return rs;
        }

        public async Task DeleteByPurchaseDetailIdAsync(int purchaseDetailId)
        {
            var list = _context.ClosePurchaseDetails.Where(x => x.PurchaseDetailId == purchaseDetailId);
            _context.ClosePurchaseDetails.RemoveRange(list);
            await _context.SaveChangesAsync();
        }
    }
}
