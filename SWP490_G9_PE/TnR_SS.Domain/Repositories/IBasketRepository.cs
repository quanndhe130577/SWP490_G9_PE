using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Repositories
{
    public interface IBasketRepository : IRepositoryBase<Basket>
    {
        Task UpdateBasketAsync(Basket basket, string type, int weight);
        Task DeleteBasketByIdAsync(int basketID);
        List<Basket> ListAllBasket();
    }
}
