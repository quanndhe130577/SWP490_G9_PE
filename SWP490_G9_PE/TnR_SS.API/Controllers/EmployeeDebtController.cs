using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.EmployeeDebtModel;
using TnR_SS.Domain.ApiModels.EmployeeModel;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/employeeDebt")]
    [ApiController]
    public class EmployeeDebtController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public EmployeeDebtController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [HttpGet("getall/{empId}")]
        public ResponseModel GetAll(int empId)
        {
            var list = _tnrssSupervisor.GetAllEmployeeDebt(empId);
            return new ResponseBuilder<List<EmployeeDebtApiModel>>().Success("Lấy thông tin ứng trước lương").WithData(list).ResponseModel;
        }

        [HttpPost("create")]
        public async Task<ResponseModel> CreateEmployeeDebtAsync(EmployeeDebtApiModel employee)
        {
            await _tnrssSupervisor.CreateEmployeeDebt(employee);
            return new ResponseBuilder().Success("Tạo ứng trước thành công").ResponseModel;
        }

        [HttpPost("update")]
        public async Task<ResponseModel> UpdateEmployeeDebtAsync(EmployeeDebtApiModel employee)
        {
            await _tnrssSupervisor.UpdateEmployeeDebt(employee);
            return new ResponseBuilder().Success("Cập nhật thông ứng trước lương").ResponseModel;
        }

        [HttpPost("delete/{empId}")]
        public async Task<ResponseModel> DeleteEmployeeDebtAsync(int edId)
        {
            var ed = await _tnrssSupervisor.GetEmpDebt(edId);
            await _tnrssSupervisor.DeleteEmployeeDebt(ed);
            return new ResponseBuilder().Success("Xóa thông tin nợ thành công").ResponseModel;
        }
    }
}
