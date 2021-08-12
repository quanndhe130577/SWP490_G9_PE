using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.AccountModel.ResponseModel;
using TnR_SS.Domain.ApiModels.RoleUserModel;
using TnR_SS.Domain.ApiModels.UserInforModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api")]
    [ApiController]
    // [Authorize(Roles = RoleName.WeightRecorder)]
    public class UserInforController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public UserInforController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }
        [Authorize(Roles = RoleName.WeightRecorder)]
        [HttpGet("wc/suggest-traders-by-phone/{phoneNumber?}")]
        public async Task<ResponseModel> GetTradersByPhone(string phoneNumber = null)
        {
            var weightRecorderId = TokenManagement.GetUserIdInToken(HttpContext);
            var trader = await _tnrssSupervisor.SuggestTradersByPhoneAsync(phoneNumber, weightRecorderId);
            return new ResponseBuilder<List<FindTraderByPhoneApiModel>>().Success("Tìm thương lái thành công").WithData(trader).ResponseModel;
        }
        [Authorize(Roles = RoleName.WeightRecorder)]
        [HttpGet("wc/find-trader-by-phone/{phoneNumber}")]
        public async Task<ResponseModel> GetTraderByPhone(string phoneNumber)
        {
            var weightRecorderId = TokenManagement.GetUserIdInToken(HttpContext);
            var trader = await _tnrssSupervisor.FindTraderByPhoneAsync(phoneNumber);
            return new ResponseBuilder<FindTraderByPhoneApiModel>().Success("Tìm thương lái thành công").WithData(trader).ResponseModel;
        }
        [Authorize(Roles = RoleName.WeightRecorder)]
        [HttpGet("wc/get-all-trader")]
        public ResponseModel GetTraders()
        {
            var weightRecorderId = TokenManagement.GetUserIdInToken(HttpContext);
            var listTrader = _tnrssSupervisor.FindTradersOfWeightRecorder(weightRecorderId);
            return new ResponseBuilder<List<FindTraderByPhoneApiModel>>().Success("Lấy thông tin thương lái thành công").WithData(listTrader).ResponseModel;
        }
        [Authorize(Roles = RoleName.WeightRecorder)]
        [HttpPost("wc/add-trader")]
        public async Task<ResponseModel> AddTrader(UserIdModel TraderIdModel)
        {
            var weightRecorderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.WeightRecorderAddTrader(TraderIdModel.TraderId, weightRecorderId);
            return new ResponseBuilder().Success("Thêm thương lái thành công").ResponseModel;
        }
        [Authorize(Roles = RoleName.Trader)]
        [HttpGet("trader/get-wr")]
        public async Task<ResponseModel> TraderGetWeightRecorder()
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            var rp = await _tnrssSupervisor.TraderGetWeightRecorder(traderId);
            return new ResponseBuilder<List<WeightRecorderModal>>().Success("Lấy dữ liệu thành công").WithData(rp).ResponseModel;
        }
        [Authorize(Roles = RoleName.Trader)]
        [HttpPost("trader/edit-wr")]
        public async Task<ResponseModel> EditTrader(WeightRecorderModal weightRecorderModals)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.TraderUpdateWeightRecorders(weightRecorderModals);
            return new ResponseBuilder().Success(weightRecorderModals.IsDeleted ? "Xoá thành công" : "Cập nhật thành công").ResponseModel;
        }
    }
    public class UserIdModel
    {
        public int TraderId { get; set; }
    }
}
