using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(TnR_SSContext context) : base(context) { }

        public List<Employee> GetAllEmployeeByTraderId(int traderId)
        {
            var rs = _context.Employees.AsEnumerable().Where(x => x.TraderId == traderId)
                .OrderByDescending(x => x.Name).ToList();
            return rs;
        }
    }
}
