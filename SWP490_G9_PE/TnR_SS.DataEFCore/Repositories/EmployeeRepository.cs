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
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(TnR_SSContext context) : base(context) { }

        public List<Employee> GetAllEmployeeByTraderId(int traderId)
        {
            var rs = _context.Employees.AsEnumerable().Where(x => x.TraderId == traderId)
                .OrderByDescending(x => x.Name).ToList();
            return rs;
        }

        public HistorySalaryEmp GetEmployeeSalary(int employeeId, DateTime date)
        {
            List<HistorySalaryEmp> historySalaryEmps = _context.HistorySalaryEmp.Where(hse => hse.DateStart < date && hse.DateEnd == null || hse.DateEnd > date).ToList();
            if (historySalaryEmps.Count > 0)
            {
                return historySalaryEmps[0];
            }
            return null;
        }

        public List<EmployeeSalaryDetailApiModel> GetAllEmployeeSalaryDetailByTraderId(int employeeId, DateTime date)
        {
            List<EmployeeSalaryDetailApiModel> employeeSalaryDetails = new List<EmployeeSalaryDetailApiModel>();
            List<Employee> employees = _context.Employees.Where(e => e.StartDate < date && e.EndDate == null || e.EndDate > date).ToList();
            foreach (Employee employee in employees)
            {
                HistorySalaryEmp historySalaryEmp = GetEmployeeSalary(employeeId, date);
                if (historySalaryEmp != null)
                {
                    double? salary = GetEmployeeSalary(employeeId, date).Salary, paid = 0, notpaid = 0; ;
                    List<TimeKeeping> timeKeepings = _context.TimeKeepings.Where(tk => tk.WorkDay.Month == date.Month && tk.WorkDay.Year == date.Year && tk.EmpId == employeeId).ToList();
                    foreach (TimeKeeping timeKeeping in timeKeepings)
                    {
                        if (timeKeeping.Note == TimeKeepingNote.IsPaid)
                        {
                            paid += salary;
                        }
                        else
                        {
                            notpaid += salary;
                        }
                    }
                    employeeSalaryDetails.Add(new EmployeeSalaryDetailApiModel()
                    {
                        ID = employee.ID,
                        Name = employee.Name,
                        Salary = salary,
                        Paid = paid,
                        NotPaid = notpaid,
                    });
                }
                else
                {
                    employeeSalaryDetails.Add(new EmployeeSalaryDetailApiModel()
                    {
                        ID = employee.ID,
                        Name = employee.Name,
                        Salary = null,
                        Paid = null,
                        NotPaid = null,
                    });
                }
            }
            return null;
        }

        /*public List<EmployeeApiModel> GetAllEmployeeByStatus(int status, int traderId)
        {
            var list = _context.Employees.AsEnumerable().Where(x => x.TraderId == traderId)
                .OrderByDescending(x => x.Name).ToList();

            foreach(var emp in list)
            {
                if(emp.EndDate == null)
                {
                 
                }
            }
        }*/
    }
}
