using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.EmployeeModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class AdvanceSalaryRepository : RepositoryBase<AdvanceSalary>, IAdvanceSalaryRepository
    {
        public AdvanceSalaryRepository(TnR_SSContext context) : base(context) { }
        public async Task<EmployeeSalaryApiModel> GetSalaryDetail(int id, DateTime date)
        {
            Employee employee = await _context.Employees.FindAsync(id);
            double? salary = null;
            double advanceSlalry = 0;
            List<BaseSalaryEmp> listSlalry = _context.BaseSalaryEmp.Where(bse => bse.StartDate < date && (bse.EndDate == null || date < bse.EndDate) && bse.EmpId == id).ToList();
            if (listSlalry.Count > 0)
            {
                salary = listSlalry[0].Salary;
            }
            List<AdvanceSalary> advanceSalaries = _context.AdvanceSalaries.Where(eas => eas.Date.Month == date.Month && eas.Date.Year == date.Year && eas.EmpId == id).ToList();
            advanceSalaries.ForEach(eas =>
            {
                advanceSlalry += eas.Amount;
            });

            return new EmployeeSalaryApiModel()
            {
                EmployeeId = id,
                EmployeeName = employee.Name,
                Slalry = salary,
                AdvanceSlalry = advanceSlalry
            };
        }
    }
}
