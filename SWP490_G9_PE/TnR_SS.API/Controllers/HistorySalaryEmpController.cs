using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.HistorySalaryEmpModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/historysalary")]
    [ApiController]
    [Authorize]
    public class HistorySalaryController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public HistorySalaryController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }
        [HttpGet("getall/{empId}")]
        public ResponseModel GetAllBaseSalaryAsync(int empId)
        {
            var rs = _tnrssSupervisor.GetAllHistoryEmpSalary(empId);
            return new ResponseBuilder<List<CreateHistorySalaryEmpModel>>().Success("Tạo lương cho nhân viên thành công").WithData(rs).ResponseModel;
        }
        [HttpPost("create")]
        public async Task<ResponseModel> CreateBaseSalaryAsync(List<CreateHistorySalaryEmpModel> salaryEmpModels)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            foreach (var salary in salaryEmpModels)
            {
                HistorySalaryEmp historySalaryEmp = _tnrssSupervisor.GetHistoryEmpSalary(salary.DateStart, salary.EmpId);
                if (historySalaryEmp == null)
                {
                    await _tnrssSupervisor.CreateHistorySalaryAsync(salary, traderId);
                }
            }
            return new ResponseBuilder().Success("Tạo lương cho nhân viên thành công").ResponseModel;
        }
    }
}
