using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class FishTypeRepository : RepositoryBase<FishType>, IFishTypeRepository
    {
        public FishTypeRepository(TnR_SSContext context) : base(context) { }
        public List<FishType> GetAllLastByTraderIdAndPondOwnerId(int traderId)
        {
            var rs = _context.FishTypes.AsEnumerable().Where(x => x.TraderID == traderId)
                .OrderByDescending(x => x.Date).ThenBy(x => x.ID).GroupBy(x => x.FishName)
                .Select(x => x.First()).ToList();
            return rs;
        }

        public List<FishType> GetAllByTraderId(int traderId)
        {
            var rs = _context.FishTypes.AsEnumerable().Where(x => x.TraderID == traderId && x.PurchaseID != null)
                .OrderByDescending(x => x.Date).ToList();
            return rs;
        }

        public void RemoveFishTypeByPurchaseId(int purchaseId)
        {
            var rs = _context.FishTypes.Where(x => x.PurchaseID == purchaseId);
            _context.FishTypes.RemoveRange(rs);
        }
    }
}
