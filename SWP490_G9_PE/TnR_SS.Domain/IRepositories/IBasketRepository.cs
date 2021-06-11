using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.IRepositories
{
    public interface IBasketRepository : IRepositoryBase<Basket>
    {
        Task CreateBasketAsync(Basket basket);
        Task UpdateBasketAsync(Basket basket, string type, int weight);
        Task DeleteBasketByIdAsync(int basketID);
        List<Basket> ListAllBasket();
        //Task<Basket> FindBasketByIdAsync(int basketID);
    }
}
