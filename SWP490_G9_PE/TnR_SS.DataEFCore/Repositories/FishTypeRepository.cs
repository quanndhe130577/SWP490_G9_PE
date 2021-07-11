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
                .OrderByDescending(x => x.Date).GroupBy(x => x.FishName)
                .Select(x => x.First()).ToList();
            return rs;
        }

        public List<FishType> GetAllByTraderId(int traderId)
        {
            var rs = _context.FishTypes.AsEnumerable().Where(x => x.TraderID == traderId)
                .OrderByDescending(x => x.Date).ToList();
            return rs;
        }
    }
}
