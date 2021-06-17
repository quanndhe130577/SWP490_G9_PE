using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class TimeKeepingRepository : RepositoryBase<TimeKeeping>, ITimeKeepingRepository
    {
        public TimeKeepingRepository(TnR_SSContext context) : base(context) { }

        public List<TimeKeeping> GetAllByTraderId(int id)
        {
            var rs = from timeKeeping in _context.TimeKeepings
                     join employee in _context.Employees on timeKeeping.EmpId equals employee.ID
                     where employee.TraderId == id
                     select timeKeeping;
            return rs.ToList();
        }
        public List<TimeKeeping> GetAllByEmployeeId(int id)
        {
            var rs = _context.TimeKeepings.Where(tk => tk.EmpId == id).ToList();
            return rs;
        }
    }
}
