using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class FishTypePriceRepository : RepositoryBase<FishTypePrice>, IFishTypePriceRepository
    {
        public FishTypePriceRepository(TnR_SSContext context) : base(context) { }
        public double GetPriceByDate(FishType fishType, DateTime date)
        {
            var rs = _context.FishTypePrices.Where(x => x.FishTypeId == fishType.ID).FirstOrDefault();
            return rs.PurchasePrice;
        }
    }
}
