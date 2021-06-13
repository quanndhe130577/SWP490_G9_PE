using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class BasketRepository : RepositoryBase<Basket>, IBasketRepository
    {

        public BasketRepository(TnR_SSContext context) : base(context) { }

        public List<Basket> ListAllBasket() => _context.Baskets.ToList();

        //public async Task<Basket> FindBasketByIdAsync(int basketId) => await _context.Baskets.FindAsync(basketId);

        /*public async Task CreateBasketAsync(Basket basket)
        {
            basket.CreatedAt = DateTime.Now;
            await _context.Baskets.AddAsync(basket);
            await _context.SaveChangesAsync();
        }*/

        public async Task DeleteBasketByIdAsync(int basketId)
        {
            var basket = await _context.Baskets.FindAsync(basketId);
            _context.Baskets.Remove(basket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBasketAsync(Basket basket, string type, int weight)
        {
            basket.UpdatedAt = DateTime.Now;
            basket.Type = type;
            basket.Weight = weight;
            _context.Baskets.Update(basket);
            await _context.SaveChangesAsync();
        }

    }
}
