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
    }
}
