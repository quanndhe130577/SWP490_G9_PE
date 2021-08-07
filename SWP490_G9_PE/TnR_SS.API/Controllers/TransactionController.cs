using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.TransactionModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public TransactionController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [Authorize(Roles = RoleName.WeightRecorder)]
        [Route("createlist")]
        public async Task<ResponseModel> Create(CreateListTransactionReqModel createModel)
        {
            var wcId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.CreateListTransactionAsync(createModel, wcId);
            return new ResponseBuilder().Success("Tạo hóa đơn thành công !!").ResponseModel;
        }

        // get data for page transaction detail
        [Route("getall/{date_str?}")]
        [HttpGet]
        public async Task<ResponseModel> GetAll(string date_str = null)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            DateTime? date = DateTime.Now;

            if (date_str == null)
            {
                date = null;
            }
            else
            {
                CultureInfo enUS = new CultureInfo("en-US");
                DateTime newDate = DateTime.Now;
                if (DateTime.TryParseExact(date_str, "ddMMyyyy", enUS, DateTimeStyles.None, out newDate))
                {
                    date = newDate;
                }
                else
                {
                    return new ResponseBuilder().Error("Lỗi format date !!!").ResponseModel;
                }
            }

            var rs = await _tnrssSupervisor.GetAllTransactionAsync(userId, date);
            return new ResponseBuilder<List<TransactionResModel>>().Success("Lấy thông tin hóa đơn thành công !!").WithData(rs).ResponseModel;
        }

        // get data for page transaction
        [Route("getgeneral")]
        [HttpGet]
        public async Task<ResponseModel> GetGeneralTransactionFollowDate()
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            var rs = await _tnrssSupervisor.GetAllTransactionFollowDateAsync(userId);
            return new ResponseBuilder<List<GetGeneralTransactionFollowDateResModel>>().Success("Lấy thông tin hóa đơn thành công !!").WithData(rs).ResponseModel;
        }

        [Route("delete")]
        [HttpPost]
        public async Task<ResponseModel> Delete(TransactionIdModel apiModel)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.DeleteTransactionAsync(apiModel.TransactionId, userId);
            return new ResponseBuilder().Success("Xóa mã cân thành công !!").ResponseModel;
        }

        [Route("chotso")]
        [HttpPost]
        public async Task<ResponseModel> ChotSoTransaction(ChotSoTransactionReqModal chotSoApi)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.ChotSoTransactionAsync(chotSoApi, userId);
            return new ResponseBuilder().Success("Chốt đơn mua thành công !!").ResponseModel;
        }
    }

    public class TransactionIdModel
    {
        public int TransactionId { get; set; }
        public int TransactionDetailId { get; set; }
    }
}
