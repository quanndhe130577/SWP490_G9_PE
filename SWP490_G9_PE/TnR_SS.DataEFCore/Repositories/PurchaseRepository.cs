using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class PurchaseRepository : RepositoryBase<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(TnR_SSContext context) : base(context) { }

        public async Task ClearPurchaseAsync()
        {
            var rs = (from p in _context.Purchases
                      from ft in _context.FishTypes.Where(x => x.PurchaseID == p.ID).DefaultIfEmpty()
                      select new
                      {
                          purchase = p,
                          fishtype = ft
                      }).Where(x => x.fishtype == null).Select(x => x.purchase);

            _context.Purchases.RemoveRange(rs);

            await _context.SaveChangesAsync();
        }
    }
}
