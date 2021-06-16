using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class DrumRepository : RepositoryBase<Drum>, IDrumRepository
    {
        public DrumRepository(TnR_SSContext context) : base(context) { }

        public List<Drum> GetAllByTraderkId(int traderId)
        {
            var rs = _context.Drums.Join(
                    _context.Trucks,
                    drums => drums.TruckID,
                    trucks => trucks.ID,
                    (drums, trucks) => new
                    {
                        drums = drums,
                        trucks = trucks
                    }).Where(x => x.trucks.TraderID == traderId).Select(x => x.drums);

            return rs.ToList();
        }
    }
}
