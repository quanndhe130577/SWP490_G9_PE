using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.AdvanceSalaryModel;
using TnR_SS.Domain.ApiModels.EmployeeModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task<AdvanceSalary> GetEmpDebt(int id)
        {
            return await _unitOfWork.AdvanceSalaries.FindAsync(id);
        }
        public List<AdvanceSalaryApiModel> GetAllAdvanceSalary(int id)
        {
            return _unitOfWork.AdvanceSalaries.GetAll(filter: ed => ed.EmpId == id).Select(ed => _mapper.Map<AdvanceSalaryApiModel>(ed)).ToList();
        }
        public async Task CreateAdvanceSalary(AdvanceSalaryApiModel apiModel)
        {
            AdvanceSalary advanceSalary = _mapper.Map<AdvanceSalary>(apiModel);
            await _unitOfWork.AdvanceSalaries.CreateAsync(advanceSalary);
            await _unitOfWork.SaveChangeAsync();
        }
        public async Task UpdateAdvanceSalary(AdvanceSalaryApiModel apiModel)
        {
            AdvanceSalary advanceSalary = await _unitOfWork.AdvanceSalaries.FindAsync(apiModel.ID);
            advanceSalary.Date = apiModel.Date;
            advanceSalary.Amount = apiModel.Amount;
            advanceSalary.EmpId = apiModel.EmpId;
            _unitOfWork.AdvanceSalaries.Update(advanceSalary);
            await _unitOfWork.SaveChangeAsync();
        }
        public async Task DeleteAdvanceSalary(AdvanceSalary apiModel)
        {
            _unitOfWork.AdvanceSalaries.Delete(apiModel);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}