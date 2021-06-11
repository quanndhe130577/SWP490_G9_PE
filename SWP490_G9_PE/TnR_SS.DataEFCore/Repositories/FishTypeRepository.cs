using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.IRepositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class FishTypeRepository : IFishTypeRepository
    {
        private readonly TnR_SSContext _context;

        public FishTypeRepository(TnR_SSContext context)
        {
            _context = context;
        }

        public async Task CreateFishTypeAsync(FishType fishType)
        {
            fishType.CreatedAt = DateTime.Now;
            await _context.FishTypes.AddAsync(fishType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFishTypeByIdAsync(int fishTypeID)
        {
            var fishType = await _context.Baskets.FindAsync(fishTypeID);
            _context.Baskets.Remove(fishType);
            await _context.SaveChangesAsync();
        }

        public void Dispose() => _context.Dispose();

        public async Task<FishType> FindFishTypeByIdAsync(int fishTypeID) => await _context.FishTypes.FindAsync(fishTypeID);

        public List<FishType> ListAllFishType()
        {
            throw new NotImplementedException();
        }

        public Task UpdateFishTypeAsync(FishType fishType, string type, int weight)
        {
            throw new NotImplementedException();
        }
    }
}
