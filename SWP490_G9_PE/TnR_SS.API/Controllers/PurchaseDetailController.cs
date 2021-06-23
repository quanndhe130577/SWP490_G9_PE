using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.PurchaseDetailModel;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseDetailController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public PurchaseDetailController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [HttpPost("create")]
        public async Task<ResponseModel> CreateAsync(PurchaseDetailReqModel data)
        {
            var purchaseDetailId = await _tnrssSupervisor.CreatePurchaseDetailAsync(data);
            return new ResponseBuilder<object>().Success("Create Fish Type Success").WithData(new { purchaseDetailId = purchaseDetailId }).ResponseModel;
        }

        [HttpGet("getall")]
        public async Task<ResponseModel> All(int purchaseId)
        {
            var list = await _tnrssSupervisor.GetAllPurchaseDetailAsync(purchaseId);
            return new ResponseBuilder<List<PurchaseDetailResModel>>().Success("Get all type").WithData(list).ResponseModel;
        }

        [HttpPost("update")]
        public async Task<ResponseModel> Update(PurchaseDetailReqModel fishType)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.UpdateFishTypeAsync(fishType, traderId);
            return new ResponseBuilder().Success("Update Fish Type Success").ResponseModel;
        }

        /*[HttpPost("delete/{fishTypeId}")]
        public async Task<ResponseModel> DeleteFishTypeAsync(int fishTypeId)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.DeleteFishTypeAsync(fishTypeId, traderId);
            return new ResponseBuilder().Success("Delete Fish Type Success").ResponseModel;
        }*/
    }
}
