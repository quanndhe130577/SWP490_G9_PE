using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class BuyerRepository : RepositoryBase<Buyer>, IBuyerRepository
    {
        public BuyerRepository(TnR_SSContext context) : base(context) { }

        public List<Buyer> GetAllBuyer()
        {
            var rs = _context.Buyers.AsEnumerable().OrderByDescending(x => x.Name).ToList();
            return rs;
        }
    }
}
