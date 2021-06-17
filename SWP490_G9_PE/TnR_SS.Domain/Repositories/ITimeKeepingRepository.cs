using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Repositories
{
    public interface ITimeKeepingRepository : IRepositoryBase<TimeKeeping>
    {
        List<TimeKeeping> GetAllByTraderId(int id);
        public List<TimeKeeping> GetAllByEmployeeId(int id);
    }
}