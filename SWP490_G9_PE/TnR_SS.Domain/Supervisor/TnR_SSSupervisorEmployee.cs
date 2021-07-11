using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.EmployeeModel;
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
                return listEmpApi.Where(x => x.Status == status.ToLower()).ToList();
            }
            else if (status.ToLower() == EmployeeStatus.all.ToString())
            {
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
                list.Add(_mapper.Map<Employee, EmployeeApiModel>(type));
            }
            return list;
        }

        public async Task CreateEmployeesAsync(EmployeeApiModel employee, int traderId)
        {
            var obj = _mapper.Map<EmployeeApiModel, Employee>(employee);
            obj.TraderId = traderId;
            if (!CheckEmployeeExist(traderId, employee))
            {
                throw new Exception("Nhân viên đã tồn tại");
            }
            else if (obj.DOB > DateTime.Now)
            {
                throw new Exception("Thông tin ngày sinh không hợp lệ");
            }
            /*else if (obj.StartDate >= obj.EndDate)
            {
                throw new Exception("Start Date can not sooner than End Date");
            }*/
            else
            {
                await _unitOfWork.Employees.CreateAsync(obj);
                await _unitOfWork.SaveChangeAsync();
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
            var flag = listEmp.Where(x => x.PhoneNumber == employee.PhoneNumber && x.DOB == employee.DOB).Count() == 0;
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
                    empMap.Status = EmployeeStatus.unavailable.ToString();
                }
                else
                {
                    empMap.Status = EmployeeStatus.available.ToString();
                }
                listEmpApi.Add(empMap);
            }
            return listEmpApi;
        }
    }
}