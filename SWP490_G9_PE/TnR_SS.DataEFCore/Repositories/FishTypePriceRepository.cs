using System.Linq;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class FishTypePriceRepository : RepositoryBase<FishTypePrice>, IFishTypePriceRepository
    {
        public FishTypePriceRepository(TnR_SSContext context) : base(context) { }

        public FishTypePrice GetTopDateByFishTypeID(int fishTypeId)
        {
            return _context.FishTypePrices.Where(x => x.FishTypeID == fishTypeId).OrderByDescending(x => x.Date).FirstOrDefault();
        }
    }
}
