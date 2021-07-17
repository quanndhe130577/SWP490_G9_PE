using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.Domain.ApiModels.HistorySalaryEmpModel;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/salary")]
    [ApiController]
    [Authorize]
    public class HistorySalaryController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public HistorySalaryController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [HttpPost("create/{empId}")]
        public async Task<ResponseModel> CreateHistorySalaryAsync(HistorySalaryEmpApiModel salary, int empId)
        {
            await _tnrssSupervisor.CreateHistorySalaryAsync(salary, empId);
            return new ResponseBuilder().Success("Tạo lương cho nhân viên thành công").ResponseModel;
        }

        [HttpGet("getall/{empId}")]
        public ResponseModel GetAllHistorySalary(int empId)
        {
            var listSalary = _tnrssSupervisor.GetAllSalaryByEmpId(empId);
            return new ResponseBuilder<List<HistorySalaryEmpApiModel>>().Success("Lấy thông tin lương nhân viên thành công").WithData(listSalary).ResponseModel;
        }

        [HttpPost("update/{empId}")]
        public async Task<ResponseModel> UpdateHistorySalaryAsync(HistorySalaryEmpApiModel salaryApi, int empId)
        {
            await _tnrssSupervisor.UpdateHistorySalaryAsync(salaryApi, empId);
            return new ResponseBuilder().Success("Cập nhật lương cho nhân viên thành công").ResponseModel;
        }


    }
}
