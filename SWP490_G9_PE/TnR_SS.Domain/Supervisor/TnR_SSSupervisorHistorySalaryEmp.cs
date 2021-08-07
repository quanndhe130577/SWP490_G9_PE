using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.HistorySalaryEmpModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public HistorySalaryEmp GetHistoryEmpSalary(DateTime date, int empId)
        {
            var rs = _unitOfWork.HistorySalaryEmps.GetAll(filter: hse => hse.DateStart.Month == date.Month && hse.DateStart.Year == date.Year && hse.EmpId == empId).ToList();
            if (rs.Count > 0)
            {
                return rs[0];
            }
            return null;
        }

        public List<CreateHistorySalaryEmpModel> GetAllHistoryEmpSalary(int empId)
        {
            return _unitOfWork.HistorySalaryEmps.GetAll(filter: hse => hse.EmpId == empId).
            Select(item => _mapper.Map<HistorySalaryEmp, CreateHistorySalaryEmpModel>(item)).
            ToList();
        }
        public async Task UpdateHistorySalaryAsync(HistorySalaryEmp historySalaryEmp, CreateHistorySalaryEmpModel salaryApi, int traderId)
        {
            var model = await _unitOfWork.Employees.FindAsync(salaryApi.EmpId);
            if (model == null || model.TraderId != traderId)
            {
                throw new Exception("Nhân viên không tồn tại");
            }
            historySalaryEmp.Salary = salaryApi.Salary;
            historySalaryEmp.Bonus = salaryApi.Bonus;
            historySalaryEmp.Punish = salaryApi.Punish;
            _unitOfWork.HistorySalaryEmps.Update(historySalaryEmp);
            await _unitOfWork.SaveChangeAsync();
        }
        public async Task CreateHistorySalaryAsync(CreateHistorySalaryEmpModel salaryApi, int traderId)
        {
            var model = await _unitOfWork.Employees.FindAsync(salaryApi.EmpId);
            if (model == null || model.TraderId != traderId)
            {
                throw new Exception("Nhân viên không tồn tại");
            }

            var salary = _mapper.Map<CreateHistorySalaryEmpModel, HistorySalaryEmp>(salaryApi);
            await _unitOfWork.HistorySalaryEmps.CreateAsync(salary);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<int> UpsertHistorySalary(DateTime date, int empId)
        {
            DateTime month = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
            double status = _unitOfWork.TimeKeepings.GetAll(tk => tk.EmpId == empId && tk.WorkDay.Month == date.Month && tk.WorkDay.Year == date.Year).Select(tk => tk.Status).Sum();
            BaseSalaryEmp baseSalaryEmp = _unitOfWork.Employees.GetEmployeeSalary(empId, date);
            HistorySalaryEmp historySalaryEmp = GetHistoryEmpSalary(date, empId);
            if (baseSalaryEmp != null)
            {
                if (historySalaryEmp == null)
                {
                    await _unitOfWork.HistorySalaryEmps.CreateAsync(new HistorySalaryEmp()
                    {
                        ID = 0,
                        DateStart = date,
                        EmpId = empId,
                        Bonus = 0,
                        Punish = 0,
                        Salary = (int)(baseSalaryEmp.Salary * status / month.Day) - _unitOfWork.Employees.GetEmployeeAdvanceSalary(empId, date),
                    });
                }
                else
                {
                    historySalaryEmp.Salary = (int)(baseSalaryEmp.Salary * status / month.Day) + historySalaryEmp.Bonus - historySalaryEmp.Punish - _unitOfWork.Employees.GetEmployeeAdvanceSalary(empId, date);
                    _unitOfWork.HistorySalaryEmps.Update(historySalaryEmp);
                }
            }

            return await _unitOfWork.SaveChangeAsync();
        }
    }
}
