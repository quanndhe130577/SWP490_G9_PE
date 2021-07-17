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
        public List<HistorySalaryEmpApiModel> GetAllSalaryByEmpId(int empId)
        {
            var listSalary = _unitOfWork.HistorySalaryEmps.GetAllSalaryByEmpId(empId);
            List<HistorySalaryEmpApiModel> listSalaryApi = new();
            foreach(var item in listSalary)
            {
                listSalaryApi.Add(_mapper.Map<HistorySalaryEmp, HistorySalaryEmpApiModel>(item));
            }
            return listSalaryApi;
        }

        public async Task CreateHistorySalaryAsync(HistorySalaryEmpApiModel salaryApi, int empId)
        {
            var salary = _mapper.Map<HistorySalaryEmpApiModel, HistorySalaryEmp>(salaryApi);
            salary.EmpId = empId;
            await _unitOfWork.HistorySalaryEmps.CreateAsync(salary);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task UpdateHistorySalaryAsync(HistorySalaryEmpApiModel salaryApi, int empId)
        {
            var salaryUpdate = await _unitOfWork.HistorySalaryEmps.FindAsync(salaryApi.Id);
            salaryUpdate = _mapper.Map<HistorySalaryEmpApiModel, HistorySalaryEmp>(salaryApi, salaryUpdate);
            if(salaryUpdate.EmpId == empId)
            {
                _unitOfWork.HistorySalaryEmps.Update(salaryUpdate);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Cập nhật thông tin lương lỗi");
            }
        }

        public async Task DeleteHistorySalaryAsync(int salaryId, int empId)
        {
            var salary = await _unitOfWork.HistorySalaryEmps.FindAsync(salaryId);
            if (salary.EmpId == empId)
            {
                _unitOfWork.HistorySalaryEmps.Delete(salary);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Không thể xóa");
            }
        }

        public HistorySalaryEmpApiModel GetDetailHistorySalary(int salaryId, int empId)
        {
            var listSalary = _unitOfWork.HistorySalaryEmps.GetAllSalaryByEmpId(empId);

            foreach (var item in listSalary)
            {
                if (item.ID == salaryId)
                {
                    return _mapper.Map<HistorySalaryEmp, HistorySalaryEmpApiModel>(item);
                }
                else
                {
                    throw new Exception("Thông tin lương không chính xác");
                }
            }
            return null;
        }
    }
}
