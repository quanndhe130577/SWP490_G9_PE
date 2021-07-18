using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.EmployeeDebtModel;
using TnR_SS.Domain.ApiModels.EmployeeModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task<EmployeeDebt> GetEmpDebt(int id)
        {
            return await _unitOfWork.EmployeeDebts.FindAsync(id);
        }
        public List<EmployeeDebtApiModel> GetAllEmployeeDebt(int id)
        {
            return _unitOfWork.EmployeeDebts.GetAll(filter: ed => ed.EmpId == id).Select(ed => _mapper.Map<EmployeeDebtApiModel>(ed)).ToList();
        }
        public async Task CreateEmployeeDebt(EmployeeDebtApiModel apiModel)
        {
            EmployeeDebt employeeDebt = _mapper.Map<EmployeeDebt>(apiModel);
            await _unitOfWork.EmployeeDebts.CreateAsync(employeeDebt);
            await _unitOfWork.SaveChangeAsync();
        }
        public async Task UpdateEmployeeDebt(EmployeeDebtApiModel apiModel)
        {
            EmployeeDebt employeeDebt = await _unitOfWork.EmployeeDebts.FindAsync(apiModel.ID);
            employeeDebt.Date = apiModel.Date;
            employeeDebt.Debt = apiModel.Debt;
            employeeDebt.EmpId = apiModel.EmpId;
            employeeDebt.Paid = apiModel.Paid;
            _unitOfWork.EmployeeDebts.Update(employeeDebt);
            await _unitOfWork.SaveChangeAsync();
        }
        public async Task DeleteEmployeeDebt(EmployeeDebt apiModel)
        {
            _unitOfWork.EmployeeDebts.Delete(apiModel);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}