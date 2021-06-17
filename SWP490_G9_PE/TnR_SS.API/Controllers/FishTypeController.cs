using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
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
        /*
                [HttpPost("create")]
                [AllowAnonymous]
                public async Task<ResponseModel> CreateFishTypePriceAsync(ListTypeModel listType)
                {
                    await _tnrssSupervisor.CreateFishTypePricesAsync(listType.ListFishType);
                    return new ResponseBuilder().Success("Create Fish Type Success").ResponseModel;
                }*/

        [HttpGet("getlastall/{traderId}")]
        public ResponseModel GetAllFishType(int traderId)
        {
            ListFishTypeModel list = new ListFishTypeModel();
            list.ListFishType = _tnrssSupervisor.GetAllLastFishTypeByTraderId(traderId);
            return new ResponseBuilder<ListFishTypeModel>().Success("Get all type").WithData(list).ResponseModel;
        }
    }
}
