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
            var list = await _tnrssSupervisor.GetAllDebtWRAsync(userId, null);
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

        [HttpGet("td-getDebtTransaction")]
        public async Task<ResponseModel> GetAllDebtTransactionOfTrader()
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            var list = await _tnrssSupervisor.GetAllDebtTransactionOfTrader(userId);
            return new ResponseBuilder<List<DebtTraderApiModel>>().Success("Lấy thông tin nợ thành công").WithData(list).ResponseModel;
        }

        [HttpGet("wr/debtWithTrader")]
        public async Task<ResponseModel> GetAllDebtTransactionOfWRWithTrader()
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            var list = await _tnrssSupervisor.GetAllDebtTransactionOfWRWithTraderAsync(userId);
            return new ResponseBuilder<List<GetDebtForWrWithTraderResModel>>().Success("Lấy thông tin nợ thành công").WithData(list).ResponseModel;
        }

        [HttpPost("wr/update/debtWithTrader")]
        public async Task<ResponseModel> GetTransactionOfTrader(UpdateDebtWrWithTraderReqModel apiModel)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.UpdateDebtTransactionOfWRWithTrader(apiModel, userId);
            return new ResponseBuilder().Success("Cập nhật thông tin thành công").ResponseModel;
        }

        /*[HttpGet("td-UpdateDebtTransactionDetail/{id}")]
        public async Task<ResponseModel> GetTransactionOfTrader(int id)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.UpdateDebtTransationDetail(userId, id);
            return new ResponseBuilder().Success("Cập nhật thông tin nợ của đơn bán thành công").ResponseModel;
        }*/

        [HttpGet("td-getDebtPurchase")]
        public async Task<ResponseModel> GetAllDebtPurchaseOfTrader()
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            var list = await _tnrssSupervisor.GetAllDebtPurchaseOfTrader(userId);
            return new ResponseBuilder<List<DebtTraderApiModel>>().Success("Lấy thông tin nợ thành công").WithData(list).ResponseModel;
        }      

        [HttpGet("td-UpdateDebtPurchase/{purchaseId}/{amount}")]
        public async Task<ResponseModel> UpdateDebtPurchaseOfTrader(int purchaseId, int amount)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.UpdateDebtPurchaseDetail(userId, purchaseId, amount);
            return new ResponseBuilder().Success("Cập nhật thông tin nợ của đơn mua thành công").ResponseModel;
        }
    }
}
