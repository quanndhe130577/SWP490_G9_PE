using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class CostIncurredRepository : RepositoryBase<CostIncurred>, ICostIncurredRepository
    {
        public CostIncurredRepository(TnR_SSContext context) : base(context) { }

        public List<CostIncurred> GetAllCostIncurredByTraderId(int traderId)
        {
            var rs = _context.CostIncurreds.AsEnumerable().Where(x => x.UserId == traderId)
                .OrderByDescending(x => x.Date)
                .ToList();
            return rs;
        }
    }
}
