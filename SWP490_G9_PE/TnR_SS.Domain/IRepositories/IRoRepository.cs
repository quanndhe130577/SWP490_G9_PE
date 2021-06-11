using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.IRepositories
{
    public interface IRoRepository : IDisposable
    {
        Task CreateRoAsync(Ro ro);
        Task UpdateRoAsync(Ro ro);
        Task DeleteRoByIdAsync(int roID);
        List<Ro> ListAllRo();
        Task<Ro> FindRoByIdAsync(int roID)
    }
}
