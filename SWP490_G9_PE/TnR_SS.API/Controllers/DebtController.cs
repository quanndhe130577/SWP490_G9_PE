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

        [HttpGet("getallTR")]
        public async Task<ResponseModel> GetAllDebtByTrader()
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            var list = await _tnrssSupervisor.GetAllDebtTraderAsync(traderId);
            return new ResponseBuilder<List<DebtApiModel>>().Success("Lấy thông tin nợ thành công")
                .WithData(list).ResponseModel;
        }

        [HttpGet("getallWR")]
        public async Task<ResponseModel> GetAllDebtByWR()
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            var list = await _tnrssSupervisor.GetAllDebtWRAsync(userId,null);
            return new ResponseBuilder<List<DebtApiModel>>().Success("Lấy thông tin nợ thành công")
                .WithData(list).ResponseModel;
        }

        [HttpGet("getall")]
        public async Task<ResponseModel> GetAllDebt()
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            var list = await _tnrssSupervisor.GetDebtAsync(userId, null);
            return new ResponseBuilder<List<DebtApiModel>>().Success("Lấy thông tin nợ thành công")
                .WithData(list).ResponseModel;
        }
    }
}
