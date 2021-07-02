using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Repositories
{
    public interface IBuyerRepository : IRepositoryBase<Buyer>
    {
        List<Buyer> GetAllBuyer();
    }
}
