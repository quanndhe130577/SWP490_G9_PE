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
        public async Task CreateHistorySalaryAsync(CreateHistorySalaryEmpModel salaryApi, int traderId)
        {
            var model = await _unitOfWork.HistorySalaryEmps.FindAsync(salaryApi.EmpId);
            if (model == null || model.Employee.TraderId != traderId)
            {
                throw new Exception("Nhân viên không tồn tại");
            }

            var salary = _mapper.Map<CreateHistorySalaryEmpModel, HistorySalaryEmp>(salaryApi);
            await _unitOfWork.HistorySalaryEmps.CreateAsync(salary);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
