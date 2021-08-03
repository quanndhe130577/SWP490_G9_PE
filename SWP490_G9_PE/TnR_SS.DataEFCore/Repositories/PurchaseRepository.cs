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
        public double GetTotalWeight(int purchaseId)
        {
            return _context.PurchaseDetails.Where(x => x.PurchaseId == purchaseId)
                .Join(
                    _context.Baskets,
                    pd => pd.BasketId,
                    bk => bk.ID,
                    (pd, bk) => new
                    {
                        weight = pd.Weight + bk.Weight
                    }
                ).Sum(x => x.weight);
        }
    }
}
