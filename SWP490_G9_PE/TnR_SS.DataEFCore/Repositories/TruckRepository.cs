using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class TruckRepository : RepositoryBase<Truck>, ITruckRepository
    {
        public TruckRepository(TnR_SSContext context) : base(context) { }

        public List<Truck> GetAllByTraderId(int traderId)
        {
            return _context.Trucks.Where(x => x.TraderID == traderId).ToList();
        }

    }
}
