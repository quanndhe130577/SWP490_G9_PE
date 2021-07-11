using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.DrumModel;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/drum")]
    [ApiController]
    [Authorize]
    public class DrumController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public DrumController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [HttpGet("getall/{truckId}")]
        public ResponseModel GetAllByTruckId(int truckId)
        {
            var rs = _tnrssSupervisor.GetAllDrumByTruckId(truckId);
            return new ResponseBuilder<object>().Success("Lấy thông tin lồ thành công").WithData(rs).ResponseModel;
        }

        [HttpPost("create")]
        public async Task<ResponseModel> CreateAsync(DrumApiModel drumModel)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            var drumId = await _tnrssSupervisor.CreateDrumAsync(drumModel, userId);
            return new ResponseBuilder<object>().Success("Tạo mới lồ thành công").WithData(new { drumId = drumId }).ResponseModel;
        }

        [HttpGet("getall")]
        public ResponseModel GetAllByTraderId()
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            var rs = _tnrssSupervisor.GetAllDrumByTraderId(userId);
            return new ResponseBuilder<object>().Success("Lấy thông tin lồ thành công").WithData(rs).ResponseModel;
        }

        [HttpPost("update")]
        public async Task<ResponseModel> UpdateDrumAsync(DrumApiModel drum)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.UpdateDrumAsync(drum, userId);
            return new ResponseBuilder().Success("Cập nhật thông tin lồ thành công").ResponseModel;
        }

        [HttpPost("delete/{drumId}")]
        public async Task<ResponseModel> DeleteFishTypeAsync(int drumId)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.DeleteDrumAsync(drumId, userId);
            return new ResponseBuilder().Success("Xóa thông tin lồ thành công").ResponseModel;
        }

        [HttpGet("detail/{drumId}")]
        public ResponseModel DetailEmployee(int drumId)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            var detail = _tnrssSupervisor.GetDetailDrum(userId, drumId);
            return new ResponseBuilder<DrumApiModel>().Success("Lấy thông tin lồ thành công").WithData(detail).ResponseModel;
        }
    }
}
