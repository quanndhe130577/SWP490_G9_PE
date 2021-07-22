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
        public List<BaseSalaryEmpApiModel> GetAllSalaryByEmpId(int empId)
        {
            var listSalary = _unitOfWork.BaseSalaryEmps.GetAllSalaryByEmpId(empId);
            List<BaseSalaryEmpApiModel> listSalaryApi = new();
            foreach (var item in listSalary)
            {
                listSalaryApi.Add(_mapper.Map<BaseSalaryEmp, BaseSalaryEmpApiModel>(item));
            }
            return listSalaryApi;
        }

        public async Task CreateBaseSalaryAsync(BaseSalaryEmpApiModel salaryApi, int empId)
        {
            var salary = _mapper.Map<BaseSalaryEmpApiModel, BaseSalaryEmp>(salaryApi);
            salary.EmpId = empId;
            await _unitOfWork.BaseSalaryEmps.CreateAsync(salary);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task UpdateBaseSalaryAsync(BaseSalaryEmpApiModel salaryApi, int empId)
        {
            var salaryUpdate = await _unitOfWork.BaseSalaryEmps.FindAsync(salaryApi.Id);
            salaryUpdate = _mapper.Map<BaseSalaryEmpApiModel, BaseSalaryEmp>(salaryApi, salaryUpdate);
            if (salaryUpdate.EmpId == empId)
            {
                _unitOfWork.BaseSalaryEmps.Update(salaryUpdate);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Cập nhật thông tin lương lỗi");
            }
        }

        public async Task DeleteBaseSalaryAsync(int salaryId, int empId)
        {
            var salary = await _unitOfWork.BaseSalaryEmps.FindAsync(salaryId);
            if (salary.EmpId == empId)
            {
                _unitOfWork.BaseSalaryEmps.Delete(salary);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Không thể xóa");
            }
        }

        public BaseSalaryEmpApiModel GetDetailBaseSalary(int salaryId, int empId)
        {
            var listSalary = _unitOfWork.BaseSalaryEmps.GetAllSalaryByEmpId(empId);

            foreach (var item in listSalary)
            {
                if (item.ID == salaryId)
                {
                    return _mapper.Map<BaseSalaryEmp, BaseSalaryEmpApiModel>(item);
                }
                else
                {
                    throw new Exception("Thông tin lương không chính xác");
                }
            }
            return null;
        }

        public BaseSalaryEmpApiModel GetSalaryByDate(DateTime date, int empId)
        {
            var listSalary = _unitOfWork.BaseSalaryEmps.GetAllSalaryByEmpId(empId)
                .Where(x => x.StartDate <= date && x.EndDate >= date)
                .OrderByDescending(x => x.StartDate)
                .Select(x => _mapper.Map<BaseSalaryEmp, BaseSalaryEmpApiModel>(x))
                .FirstOrDefault();
            return listSalary;
        }
    }
}
