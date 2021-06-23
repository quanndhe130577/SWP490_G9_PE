using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.TruckModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/truck")]
    [ApiController]
    [Authorize]
    public class TruckController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public TruckController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [HttpGet("getall")]
        public ResponseModel GettAllTruckByTraderId()
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            var listTruck = _tnrssSupervisor.GetAllTruckByTraderId(traderId);
            return new ResponseBuilder<object>().Success("Get all type").WithData(listTruck).ResponseModel;
        }

        [HttpPost("create")]
        public async Task<ResponseModel> CreateTruck(TruckApiModel truckModel)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            var truckId = await _tnrssSupervisor.CreateTruckAsync(truckModel, traderId);
            return new ResponseBuilder<object>().Success("Create truck success").WithData(new { truckId = truckId }).ResponseModel;
        }

        [HttpPost("update")]
        public async Task<ResponseModel> UpdateTruck(TruckApiModel truckModel)
        {
            var truckId = await _tnrssSupervisor.UpdateTruckAsync(truckModel);
            return new ResponseBuilder<object>().Success("Update truck success").ResponseModel;
        }

        [HttpPost("delete/{id}")]
        public async Task<ResponseModel> DeleteTruck(int id)
        {
            Truck truck = await _tnrssSupervisor.GetTruck(id);
            if (truck == null)
            {
                return new ResponseBuilder<object>().Error("Truck not found").ResponseModel;
            }
            await _tnrssSupervisor.DeleteTruck(truck);
            return new ResponseBuilder<object>().Success("Delete truck success").ResponseModel;
        }
    }
}
