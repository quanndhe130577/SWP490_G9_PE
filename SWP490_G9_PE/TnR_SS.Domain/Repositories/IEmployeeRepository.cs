using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.EmployeeModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Repositories
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        List<Employee> GetAllEmployeeByTraderId(int traderId);
        BaseSalaryEmp GetEmployeeSalary(int employeeId, DateTime date);
        List<EmployeeSalaryDetailApiModel> GetAllEmployeeSalaryDetailByTraderId(int employeeId, DateTime date);

        //List<EmployeeApiModel> GetAllEmployeeByStatus(int status);
    }
}
