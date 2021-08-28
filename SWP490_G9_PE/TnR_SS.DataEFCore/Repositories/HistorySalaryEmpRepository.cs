using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class HistorySalaryEmpRepository : RepositoryBase<HistorySalaryEmp>, IHistorySalaryEmpRepository
    {
        public HistorySalaryEmpRepository(TnR_SSContext context) : base(context) { }

        public double GetTotalSalaryOfEmpByMonth(int month, int year, int traderId)
        {
            return _context.Employees.Where(x => x.TraderId == traderId).Join(
                        _context.HistorySalaryEmp,
                        emp => emp.ID,
                        hse => hse.EmpId,
                        (emp, hse) => new
                        {
                            HistoryEmpSalary = hse
                        }
                    ).Where(x => x.HistoryEmpSalary.DateStart.Month == month && x.HistoryEmpSalary.DateStart.Year == year)
                    .Sum(x => x.HistoryEmpSalary.Salary);
        }
    }
}
