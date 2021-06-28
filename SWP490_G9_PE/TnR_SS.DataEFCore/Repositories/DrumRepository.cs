using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.DrumModel;
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
        public List<LK_Drum_Truck> GetLKPurchaseApiModel(PurchaseDetail data)
        {
            return _context.LK_PurchaseDeatil_Drums.Where(x => x.PurchaseDetailID == data.ID)
                .Join(
                    _context.Drums,
                    lk => lk.DrumID,
                    dr => dr.ID,
                    (lk, dr) => new
                    {
                        Drum = dr,
                        TruckID = dr.TruckID
                    }
                ).Join(
                    _context.Trucks,
                    o => o.TruckID,
                    tr => tr.ID,
                    (o, tr) => new LK_Drum_Truck
                    {
                        Drum = o.Drum,
                        Truck = tr
                    }).ToList();

        }
    }
}
