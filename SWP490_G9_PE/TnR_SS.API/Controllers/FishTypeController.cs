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

        [HttpPost("create/{traderId}")]
        [AllowAnonymous]
        public async Task<ResponseModel> CreateFishTypePriceAsync(ListTypeModel listType)
        {
            await _tnrssSupervisor.CreateFishTypePricesAsync(listType.ListFishType);
            return new ResponseBuilder().Success("Create Fish Type Success").ResponseModel;
        }

        [HttpPost("getall/{id}")]
        [AllowAnonymous]
        public ResponseModel GetAllFishType(int id)
        {
            List<FishTypeApiModel> list = _tnrssSupervisor.GetAllFishTypeByTraderId(id);
            return new ResponseBuilder<List<FishTypeApiModel>>().Success("Get all type").WithData(list).ResponseModel;
        }
    }
}
