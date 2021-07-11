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
            return new ResponseBuilder<object>().Success("Lấy thông tin chi phí thành công").WithData(list).ResponseModel;
        }

        [HttpPost("create")]
        public async Task<ResponseModel> CreateEmployeeAsync(CostIncurredApiModel incurred)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.CreateCostIncurredAsync(incurred, userId);
            return new ResponseBuilder().Success("Tạo chi phí mới thành công").ResponseModel;
        }

        [HttpPost("update")]
        public async Task<ResponseModel> UpdateCostincurredAsync(CostIncurredApiModel incurred)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.UpdateCostIncurredAsync(incurred, userId);
            return new ResponseBuilder().Success("Cập nhật chi phí phát sinh thành công").ResponseModel;
        }

        [HttpPost("delete/{incurredId}")]
        public async Task<ResponseModel> DeleteCostIncurredAsync(int incurredId)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.DeleteCostIncurredAsync(incurredId, userId);
            return new ResponseBuilder().Success("Xóa thông tin chi phí thành công").ResponseModel;
        }

        [HttpGet("detail/{incurredId}")]
        public ResponseModel DetailEmployee(int incurredId)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            var detail = _tnrssSupervisor.GetDetailCostIncurred(userId, incurredId);
            return new ResponseBuilder<CostIncurredApiModel>().Success("Lấy thông tin chi phí thành công").WithData(detail).ResponseModel;
        }
    }
}
