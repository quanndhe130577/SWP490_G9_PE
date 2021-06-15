using System.Collections.Generic;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Repositories
{
    public interface ITruckRepository : IRepositoryBase<Truck>
    {
        List<Truck> GetAllByTraderId(int id);
    }
}
