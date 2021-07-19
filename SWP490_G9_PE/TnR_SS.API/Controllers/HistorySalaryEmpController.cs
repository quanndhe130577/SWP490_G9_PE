using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.HistorySalaryEmpModel;
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

        [HttpPost("create")]
        public async Task<ResponseModel> CreateBaseSalaryAsync(CreateHistorySalaryEmpModel salary)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.CreateHistorySalaryAsync(salary, traderId);
            return new ResponseBuilder().Success("Tạo lương cho nhân viên thành công").ResponseModel;
        }
    }
}
