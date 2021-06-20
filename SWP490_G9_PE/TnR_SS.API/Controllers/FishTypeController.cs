using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.FishTypeModel.ResponseModel;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/fishtype")]
    [ApiController]
    public class FishTypeController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public FishTypeController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [HttpPost("createlist")]
        public async Task<ResponseModel> CreateFishTypeAsync(ListFishTypeModel listType)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.CreateListFishTypeAsync(listType.ListFishType, traderId);
            return new ResponseBuilder().Success("Create Fish Type Success").ResponseModel;
        }

        [HttpPost("create")]
        public async Task<ResponseModel> CreateFishTypeAsync(FishTypeApiModel fishType)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.CreateFishTypeAsync(fishType, traderId);
            return new ResponseBuilder().Success("Create Fish Type Success").ResponseModel;
        }

        [HttpGet("getlastall")]
        public ResponseModel GetAllFishType()
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            var fishTypes= _tnrssSupervisor.GetAllLastFishTypeByTraderId(traderId);
            return new ResponseBuilder<List<FishTypeApiModel>>().Success("Get all type").WithData(fishTypes).ResponseModel;
        }

        [HttpPost("update")]
        public async Task<ResponseModel> UpdateFishTypeAsync(FishTypeApiModel fishType)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.UpdateFishTypeAsync(fishType, traderId);
            return new ResponseBuilder().Success("Update Fish Type Success").ResponseModel;
        }

        [HttpPost("delete/{fishTypeId}")]
        public async Task<ResponseModel> DeleteFishTypeAsync(int fishTypeId)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.DeleteFishTypeAsync(fishTypeId, traderId);
            return new ResponseBuilder().Success("Delete Fish Type Success").ResponseModel;
        }
    }
}
