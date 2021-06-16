using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.EmployeeModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public List<EmployeeApiModel> GetAllEmployeeByTraderId(int traderId)
        {
            var listType = _unitOfWork.Employees.GetAllEmployeeByTraderId(traderId);
            List<EmployeeApiModel> list = new();
            foreach (var type in listType)
            {
                list.Add(_mapper.Map<Employee, EmployeeApiModel>(type));
            }
            return list;
        }

        public async Task CreateEmployeesAsync(EmployeeApiModel employee, int traderId)
        {
            var obj = _mapper.Map<EmployeeApiModel, Employee>(employee);
            obj.TraderId = traderId;
            await _unitOfWork.Employees.CreateAsync(obj);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task UpdateEmployeeAsync(EmployeeApiModel employee)
        {
            var empEdit = await _unitOfWork.Employees.FindAsync(employee.ID);
            empEdit = _mapper.Map<EmployeeApiModel, Employee>(employee, empEdit);
            _unitOfWork.Employees.Update(empEdit);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteEmployeeAsync(int empId)
        {
            var empEdit = await _unitOfWork.Employees.FindAsync(empId);
            _unitOfWork.Employees.Delete(empEdit);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}