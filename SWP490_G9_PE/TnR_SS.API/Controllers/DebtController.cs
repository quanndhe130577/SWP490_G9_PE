using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.DebtModel;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/debt")]
    [ApiController]
    public class DebtController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public DebtController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [HttpGet("getall")]
        public async Task<ResponseModel> GetAllDebtByTrader()
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            var list = await _tnrssSupervisor.GetAllDebtTraderAsync(traderId);
            return new ResponseBuilder<List<DebtApiModel>>().Success("Lấy thông tin rổ thành công")
                .WithData(list).ResponseModel;
        }
    }
}
