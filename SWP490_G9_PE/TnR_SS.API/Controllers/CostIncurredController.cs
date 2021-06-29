using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.CostIncurredModel;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/costincurred")]
    [ApiController]
    public class CostIncurredController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public CostIncurredController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [HttpGet("getall")]
        public ResponseModel GetAllCostIncurredByTraderId()
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            var list = _tnrssSupervisor.GetAllCostIncurredTraderId(userId);
            return new ResponseBuilder<object>().Success("Get All Cost Incurred Success").WithData(list).ResponseModel;
        }

        [HttpPost("create")]
        public async Task<ResponseModel> CreateEmployeeAsync(CostIncurredApiModel incurred)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.CreateCostIncurredAsync(incurred, userId);
            return new ResponseBuilder().Success("Create Cost Incurred Success").ResponseModel;
        }

        [HttpPost("update")]
        public async Task<ResponseModel> UpdateCostincurredAsync(CostIncurredApiModel incurred)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.UpdateCostIncurredAsync(incurred, userId);
            return new ResponseBuilder().Success("Update Cost Incurred Success").ResponseModel;
        }

        [HttpPost("delete/{incurredId}")]
        public async Task<ResponseModel> DeleteCostIncurredAsync(int incurredId)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.DeleteCostIncurredAsync(incurredId, userId);
            return new ResponseBuilder().Success("Delete Cost Incurred Success").ResponseModel;
        }

        [HttpGet("detail/{incurredId}")]
        public ResponseModel DetailEmployee(int incurredId)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            var detail = _tnrssSupervisor.GetDetailCostIncurred(userId, incurredId);
            return new ResponseBuilder<CostIncurredApiModel>().Success("Get Detail Cost Incurred Success").WithData(detail).ResponseModel;
        }
    }
}
