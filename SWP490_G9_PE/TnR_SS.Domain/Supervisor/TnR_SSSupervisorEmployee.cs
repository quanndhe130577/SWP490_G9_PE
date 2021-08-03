using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.EmployeeModel;
using TnR_SS.Domain.ApiModels.HistorySalaryEmpModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public List<EmployeeApiModel> GetAllEmployeeByStatus(string status, int traderId)
        {
            var listEmpApi = AddStatusToEmployee(traderId);
            if (status.ToLower() == EmployeeStatus.available.ToString()
                || status.ToLower() == EmployeeStatus.unavailable.ToString())
            {
                foreach (EmployeeApiModel employeeApi in listEmpApi)
                {
                    employeeApi.Salary = _unitOfWork.Employees.GetEmployeeSalary(employeeApi.ID, DateTime.Now).Salary;
                }
                return listEmpApi.Where(x => x.Status == status.ToLower()).ToList();
            }
            else if (status.ToLower() == EmployeeStatus.all.ToString())
            {
                foreach (EmployeeApiModel employeeApi in listEmpApi)
                {
                    BaseSalaryEmp historySalaryEmp = _unitOfWork.Employees.GetEmployeeSalary(employeeApi.ID, DateTime.Now);
                    employeeApi.Salary = historySalaryEmp == null ? null : historySalaryEmp.Salary;
                }
                return listEmpApi;
            }
            else
            {
                throw new Exception("Thông tin URL không hợp lệ");
            }
        }

        public List<EmployeeApiModel> GetAllEmployeeByTraderId(int traderId)
        {
            var listEmp = _unitOfWork.Employees.GetAllEmployeeByTraderId(traderId);
            List<EmployeeApiModel> list = new();
            foreach (var type in listEmp)
            {
                EmployeeApiModel employee = _mapper.Map<Employee, EmployeeApiModel>(type);
                list.Add(employee);
                list = AddStatusToEmployee(traderId);
                foreach (EmployeeApiModel employeeApi in list)
                {
                    BaseSalaryEmp historySalaryEmp = _unitOfWork.Employees.GetEmployeeSalary(employeeApi.ID, DateTime.Now);
                    employeeApi.Salary = historySalaryEmp == null ? null : historySalaryEmp.Salary;
                }
            }
            return list.OrderBy(a => a.Status).ToList();
        }

        public async Task<EmployeeApiModel> CreateEmployeesAsync(EmployeeApiModel employee, int traderId)
        {
            Employee obj = _mapper.Map<EmployeeApiModel, Employee>(employee);
            obj.TraderId = traderId;
            if (!CheckEmployeeExist(traderId, employee))
            {
                throw new Exception("Nhân viên đã tồn tại");
            }
            else if (obj.DOB > DateTime.Now)
            {
                throw new Exception("Thông tin ngày sinh không hợp lệ");
            }
            else
            {
                await _unitOfWork.Employees.CreateAsync(obj);
                await _unitOfWork.SaveChangeAsync();
                if (employee.Salary != null)
                {
                    HistorySalaryEmp historySalaryEmp = new HistorySalaryEmp()
                    {
                        ID = 0,
                        EmpId = obj.ID,
                        DateStart = obj.StartDate,
                        DateEnd = null,
                        Salary = (double)employee.Salary,
                    };
                    await _unitOfWork.HistorySalaryEmps.CreateAsync(historySalaryEmp);
                }
                await _unitOfWork.SaveChangeAsync();
                return _mapper.Map<Employee, EmployeeApiModel>(obj);
            }
        }

        public async Task UpdateEmployeeAsync(EmployeeApiModel employee, int traderId)
        {
            var empEdit = await _unitOfWork.Employees.FindAsync(employee.ID);
            empEdit = _mapper.Map<EmployeeApiModel, Employee>(employee, empEdit);
            if (empEdit.TraderId == traderId)
            {
                if (CheckEmployeeExist(traderId, employee))
                {
                    throw new Exception("Nhân viên đã tồn tại");
                }
                else if (empEdit.DOB > DateTime.Now)
                {
                    throw new Exception("Thông tin ngày sinh không hợp lệ");
                }
                /*else if (empEdit.StartDate >= empEdit.EndDate)
                {
                    throw new Exception("Start Date can not sooner than End Date");
                }*/
                else
                {
                    _unitOfWork.Employees.Update(empEdit);
                    await _unitOfWork.SaveChangeAsync();
                }
            }
            else
            {
                throw new Exception("Thông tin nhân viên không chính xác");
            }
        }

        public async Task DeleteEmployeeAsync(int empId, int traderId)
        {
            var empEdit = await _unitOfWork.Employees.FindAsync(empId);
            if (empEdit.TraderId == traderId)
            {
                _unitOfWork.Employees.Delete(empEdit);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Thông tin nhân viên không chính xác");
            }
        }

        public EmployeeApiModel GetDetailEmployee(int traderId, int empId)
        {
            var listEmp = _unitOfWork.Employees.GetAllEmployeeByTraderId(traderId);
            foreach (var obj in listEmp)
            {
                if (obj.ID == empId)
                {
                    return _mapper.Map<Employee, EmployeeApiModel>(obj);
                }
            }
            return null;
        }

        public bool CheckEmployeeExist(int traderId, EmployeeApiModel employee)
        {
            var listEmp = _unitOfWork.Employees.GetAllEmployeeByTraderId(traderId);
            var flag = listEmp.Where(x => x.PhoneNumber == employee.PhoneNumber).Count() == 0;
            if (flag)
            {
                return true;
            }
            return false;
        }

        public List<EmployeeApiModel> AddStatusToEmployee(int traderId)
        {
            var list = _unitOfWork.Employees.GetAllEmployeeByTraderId(traderId);
            List<EmployeeApiModel> listEmpApi = new();
            foreach (var emp in list)
            {
                var empMap = _mapper.Map<Employee, EmployeeApiModel>(emp);
                if (empMap.EndDate != null && empMap.EndDate <= DateTime.Now)
                {
                    //empMap.Status = EmployeeStatus.unavailable.ToString();
                    empMap.Status = "Nghỉ làm";
                }
                else
                {
                    //empMap.Status = EmployeeStatus.available.ToString();
                    empMap.Status = "Đang làm";
                }
                listEmpApi.Add(empMap);
            }
            return listEmpApi;
        }

        public List<EmployeeSalaryDetailApiModel> GetAllEmployeeSalaryDetailByTraderId(int traderId, DateTime date)
        {
            return _unitOfWork.Employees.GetAllEmployeeSalaryDetailByTraderId(traderId, date);
        }
        public async Task<int> UpdateSalaryEmployee(BaseSalaryEmpApiModel salaryEmpApiModel)
        {
            BaseSalaryEmp baseSalaryEmp = _unitOfWork.Employees.GetEmployeeSalary(salaryEmpApiModel.EmpId, DateTime.Now);
            if (baseSalaryEmp == null)
            {
                Employee employee = await _unitOfWork.Employees.FindAsync(salaryEmpApiModel.EmpId);
                await _unitOfWork.BaseSalaryEmps.CreateAsync(new BaseSalaryEmp()
                {
                    ID = 0,
                    EmpId = salaryEmpApiModel.EmpId,
                    Salary = salaryEmpApiModel.Salary,
                    StartDate = new DateTime(employee.StartDate.Year, employee.StartDate.Month, 1),
                    EndDate = null
                });
            }
            else
            {
                baseSalaryEmp.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                _unitOfWork.BaseSalaryEmps.Update(baseSalaryEmp);
                await _unitOfWork.BaseSalaryEmps.CreateAsync(new BaseSalaryEmp()
                {
                    ID = 0,
                    EmpId = salaryEmpApiModel.EmpId,
                    Salary = salaryEmpApiModel.Salary,
                    StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                    EndDate = null
                });
            }
            return await _unitOfWork.SaveChangeAsync();
        }
    }
}