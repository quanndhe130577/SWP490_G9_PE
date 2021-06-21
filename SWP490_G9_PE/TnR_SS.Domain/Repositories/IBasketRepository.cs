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
        void UpdateBasket(Basket basket, string type, int weight);
        Task DeleteBasketByIdAsync(int basketID);
        List<Basket> ListAllBasket();
        List<Basket> GetAllBasketByTraderId(int traderId);
    }
}
