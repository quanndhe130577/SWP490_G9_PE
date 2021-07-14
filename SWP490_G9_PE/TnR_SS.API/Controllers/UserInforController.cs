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
using TnR_SS.Domain.ApiModels.UserInforModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize(Roles = RoleName.WeightRecorder)]
    public class UserInforController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public UserInforController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [HttpGet("wc/find-trader-by-phone/{phoneNumber}")]
        public async Task<ResponseModel> GetTradersByPhone(string phoneNumber)
        {
            var weightRecorderId = TokenManagement.GetUserIdInToken(HttpContext);
            var trader = await _tnrssSupervisor.FindTraderByPhone(phoneNumber);
            return new ResponseBuilder<FindTraderByPhoneApiModel>().Success("Tìm thương lái thành công").WithData(trader).ResponseModel;
        }

        [HttpGet("wc/get-all-trader")]
        public ResponseModel GetTraders()
        {
            var weightRecorderId = TokenManagement.GetUserIdInToken(HttpContext);
            var listTrader = _tnrssSupervisor.FindTradersOfWeightRecorder(weightRecorderId);
            return new ResponseBuilder<List<FindTraderByPhoneApiModel>>().Success("Lấy thông tin thương lái thành công").WithData(listTrader).ResponseModel;
        }

        [HttpPost("wc/add-trader")]
        public async Task<ResponseModel> AddTrader(UserIdModel TraderIdModel)
        {
            var weightRecorderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.WeightRecorderAddTrader(TraderIdModel.TraderId, weightRecorderId);
            return new ResponseBuilder().Success("Thêm thương lái thành công").ResponseModel;
        }

    }
    public class UserIdModel
    {
        public int TraderId { get; set; }
    }
}
