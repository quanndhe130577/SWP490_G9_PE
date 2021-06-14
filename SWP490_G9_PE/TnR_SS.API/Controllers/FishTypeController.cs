using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.Domain.ApiModels.FishTypeModel;
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

        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<ResponseModel> CreateNewBasketAsync(FishTypeApiModel fishTypeModel)
        {
            await _tnrssSupervisor.CreateFishTypeAsync(fishTypeModel);
            return new ResponseBuilder<FishTypeApiModel>().Success("Create Fish Type Success").WithData(fishTypeModel).ResponseModel;
        }

        [HttpPost("getall/{id}")]
        [AllowAnonymous]
        public ResponseModel GetAllFishType(int id)
        {
            List<FishTypeApiModel> list = _tnrssSupervisor.GetAllFishType(id);
            return new ResponseBuilder<List<FishTypeApiModel>>().Success("Get all type").WithData(list).ResponseModel;
        }
    }
}
