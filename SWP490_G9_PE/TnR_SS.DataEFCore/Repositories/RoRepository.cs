using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.IRepositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class RoRepository : IRoRepository
    {
        private readonly TnR_SSContext _context;

        public RoRepository(TnR_SSContext context)
        {
            _context = context;
        }

        public List<Ro> ListAllRo() => _context.Ros.ToList();

        public async Task<Ro> FindRoByIdAsync(int roID) => await _context.Ros.FindAsync(roID);

        public async Task CreateRoAsync(Ro ro)
        {
            await _context.Ros.AddAsync(ro);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoByIdAsync(int roID)
        {
            var ro = await _context.Ros.FindAsync(roID);
            _context.Ros.Remove(ro);
            await _context.SaveChangesAsync();
        }

        public void Dispose() => _context.Dispose();

        public async Task UpdateRoAsync(Ro ro, string type, int weight)
        {
            ro = new Ro() { Type = type, Weight = weight };
            _context.Ros.Update(ro);
            await _context.SaveChangesAsync();
        }


    }
}
