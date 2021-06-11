using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.IRepositories
{
    public interface IBasketRepository : IDisposable
    {
        Task CreateRoAsync(Basket ro);
        Task UpdateRoAsync(Basket ro);
        Task DeleteRoByIdAsync(int roID);
        List<Basket> ListAllRo();
        Task<Basket> FindRoByIdAsync(int roID);

    }
}
