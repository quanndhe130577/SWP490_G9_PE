using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.BuyerModel;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/buyer")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public BuyerController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [HttpPost("create")]
        public async Task<ResponseModel> CreateNewBasketAsync(BuyerApiModel buyerApi)
        {
            var wcId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.CreateBuyerAsync(buyerApi, wcId);
            return new ResponseBuilder().Success("Create Buyer Success").ResponseModel;
        }

        [HttpGet("getall")]
        public ResponseModel GetAllBuyer()
        {
            var wcId = TokenManagement.GetUserIdInToken(HttpContext);
            List<BuyerApiModel> list = _tnrssSupervisor.GetAllBuyerByWCId(wcId);
            return new ResponseBuilder<List<BuyerApiModel>>().Success("Get all buyer").WithData(list).ResponseModel;
        }

        [HttpPost("update")]
        public async Task<ResponseModel> UpdateBuyerAsync(BuyerApiModel buyerApi)
        {
            var wcId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.UpdateBuyerAsync(buyerApi, wcId);
            return new ResponseBuilder().Success("Update Buyer Success").ResponseModel;
        }

        [HttpPost("delete/{buyerId}")]
        public async Task<ResponseModel> DeleteBuyerAsync(int buyerId)
        {
            var wcId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.DeleteBuyerAsync(buyerId, wcId);
            return new ResponseBuilder().Success("Delete Buyer Success").ResponseModel;
        }

        [HttpGet("detail/{buyerId}")]
        public async Task<ResponseModel> GetDetailBuyer(int buyerId)
        {
            var wcId = TokenManagement.GetUserIdInToken(HttpContext);
            var buyerDetail = await _tnrssSupervisor.GetDetailBuyerAsync(buyerId, wcId);
            return new ResponseBuilder<BuyerApiModel>().Success("Get Detail Buyer Success").WithData(buyerDetail).ResponseModel;
        }
    }
}
