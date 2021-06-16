using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.Domain.ApiModels.TruckModel;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/truck")]
    [ApiController]
    public class TruckController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public TruckController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [HttpGet("getall/{traderId}")]
        public ResponseModel GettAllTruckByTraderId(int traderId)
        {
            var listTruck = _tnrssSupervisor.GetAllTruckByTraderId(traderId);
            return new ResponseBuilder<List<TruckApiModel>>().Success("Get all type").WithData(listTruck).ResponseModel;
        }

        [HttpPost("create")]
        public ResponseModel CreateTruck(TruckApiModel truckModel)
        {
            var truckId = _tnrssSupervisor.CreateTruckAsync(truckModel);
            return new ResponseBuilder<object>().Success("Create truck success").WithData(new { truckId = truckId }).ResponseModel;
        }
    }
}
