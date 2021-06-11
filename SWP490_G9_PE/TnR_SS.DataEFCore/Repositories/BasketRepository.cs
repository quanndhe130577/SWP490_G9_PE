using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.IRepositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly TnR_SSContext _context;

        public BasketRepository(TnR_SSContext context)
        {
            _context = context;
        }

        public List<Basket> ListAllRo() => _context.Baskets.ToList();

        public async Task<Basket> FindRoByIdAsync(int roID) => await _context.Baskets.FindAsync(roID);

        public async Task CreateRoAsync(Basket ro)
        {
            await _context.Baskets.AddAsync(ro);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoByIdAsync(int roID)
        {
            var ro = await _context.Baskets.FindAsync(roID);
            _context.Baskets.Remove(ro);
            await _context.SaveChangesAsync();
        }

        public void Dispose() => _context.Dispose();

        public async Task UpdateRoAsync(Basket ro, string type, int weight)
        {
            ro = new Basket() { Type = type, Weight = weight };
            _context.Baskets.Update(ro);
            await _context.SaveChangesAsync();
        }

    }
}
