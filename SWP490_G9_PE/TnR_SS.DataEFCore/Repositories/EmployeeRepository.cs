using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.EmployeeModel;
using TnR_SS.Domain.ApiModels.HistorySalaryEmpModel;
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

        public BaseSalaryEmp GetEmployeeSalary(int employeeId, DateTime date)
        {
            List<BaseSalaryEmp> baseSalaryEmps = _context.BaseSalaryEmp
            .Where(hse => hse.StartDate <= date && (hse.EndDate == null || hse.EndDate >= date) && hse.EmpId == employeeId)
            .OrderByDescending(hse => hse.ID).ToList();
            if (baseSalaryEmps.Count > 0)
            {
                return baseSalaryEmps[0];
            }
            return null;
        }

        public double GetEmployeeAdvanceSalary(int employeeId, DateTime date)
        {
            double salary = 0;
            foreach (AdvanceSalary advanceSalary in _context.AdvanceSalaries
             .Where(hse => hse.Date.Month == date.Month && hse.Date.Year == date.Year && hse.EmpId == employeeId))
            {
                salary += advanceSalary.Amount;
            }
            return salary;
        }
        public List<EmployeeSalaryDetailApiModel> GetAllEmployeeSalaryDetailByTraderId(int traderId, DateTime date)
        {
            List<EmployeeSalaryDetailApiModel> employeeSalaryDetails = new List<EmployeeSalaryDetailApiModel>();
            List<Employee> employees = _context.Employees.Where(e => e.StartDate < date && e.EndDate == null || e.EndDate > date).ToList();
            foreach (Employee employee in employees)
            {
                BaseSalaryEmp baseSalaryEmp = GetEmployeeSalary(employee.ID, date);
                double status = 0;
                foreach (TimeKeeping tk in _context.TimeKeepings.Where(tk => tk.WorkDay.Month == date.Month && tk.WorkDay.Year == date.Year && tk.EmpId == employee.ID))
                {
                    status += tk.Status;
                }
                List<HistorySalaryEmp> historySalaryEmps = _context.HistorySalaryEmp.Where(hse => hse.DateStart.Month == date.Month && hse.DateStart.Year == date.Year && hse.EmpId == employee.ID).ToList();
                if (baseSalaryEmp != null)
                {
                    double? baseSalary = baseSalaryEmp.Salary;
                    employeeSalaryDetails.Add(new EmployeeSalaryDetailApiModel()
                    {
                        ID = employee.ID,
                        Name = employee.Name,
                        BaseSalary = baseSalary,
                        Bonus = historySalaryEmps.Count > 0 ? historySalaryEmps[0].Bonus : 0,
                        Punish = historySalaryEmps.Count > 0 ? historySalaryEmps[0].Punish : 0,
                        Salary = historySalaryEmps.Count > 0 ? historySalaryEmps[0].Salary : null,
                        Status = status,
                        AdvanceSalary = GetEmployeeAdvanceSalary(employee.ID, date)
                    });
                }
                else
                {
                    employeeSalaryDetails.Add(new EmployeeSalaryDetailApiModel()
                    {
                        ID = employee.ID,
                        Name = employee.Name,
                        BaseSalary = null,
                        Salary = historySalaryEmps.Count > 0 ? historySalaryEmps[0].Salary : null,
                        Status = status,
                        AdvanceSalary = GetEmployeeAdvanceSalary(employee.ID, date)
                    });
                }
            }
            return employeeSalaryDetails;
        }
    }
}
