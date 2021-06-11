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

    }
}
