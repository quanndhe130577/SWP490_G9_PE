using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
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
            await _tnrssSupervisor.CreateBuyerAsync(buyerApi);
            return new ResponseBuilder().Success("Create Buyer Success").ResponseModel;
        }

        [HttpGet("getall")]
        public ResponseModel GetAllBuyer()
        {
            List<BuyerApiModel> list = _tnrssSupervisor.GetAllBuyer();
            return new ResponseBuilder<List<BuyerApiModel>>().Success("Get all buyer").WithData(list).ResponseModel;
        }

        [HttpPost("update")]
        public async Task<ResponseModel> UpdateBuyerAsync(BuyerApiModel buyerApi)
        {
            await _tnrssSupervisor.UpdateBuyerAsync(buyerApi);
            return new ResponseBuilder().Success("Update Buyer Success").ResponseModel;
        }

        [HttpPost("delete/{buyerId}")]
        public async Task<ResponseModel> DeleteBuyerAsync(int buyerId)
        {
            await _tnrssSupervisor.DeleteBuyerAsync(buyerId);
            return new ResponseBuilder().Success("Delete Buyer Success").ResponseModel;
        }

        [HttpGet("detail/{buyerId}")]
        public async Task<ResponseModel> GetDetailBuyer(int buyerId)
        {
            var buyerDetail = await _tnrssSupervisor.GetDetailBuyerAsync(buyerId);
            return new ResponseBuilder<BuyerApiModel>().Success("Get Detail Buyer Success").WithData(buyerDetail).ResponseModel;
        }
    }
}
