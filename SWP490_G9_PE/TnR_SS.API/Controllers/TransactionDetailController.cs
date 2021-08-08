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
using TnR_SS.Domain.ApiModels.TransactionDetailModel;
using TnR_SS.Domain.ApiModels.TransactionModel;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionDetailController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public TransactionDetailController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [Route("create")]
        public async Task<ResponseModel> Create(CreateTransactionDetailReqModel model)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.CreateTransactionDetailAsync(model, userId);
            return new ResponseBuilder().Success("Tạo đơn bán thành công").ResponseModel;
        }

        // api này chưa tính đến tính năng chốt sổ // chưa dc dùng
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

            var rs = await _tnrssSupervisor.GetAllTransactionDetailAsync(userId, date);
            return new ResponseBuilder<List<GetAllTransactionDetailResModel>>().Success("Lấy thông tin hóa đơn thành công !!").WithData(rs).ResponseModel;
        }

        [Route("update")]
        [HttpPost]
        public async Task<ResponseModel> Update(UpdateTransactionDetailReqModel apiModel)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.UpdateTransactionDetailAsync(apiModel, userId);
            return new ResponseBuilder().Success("Cập nhật thông tin hóa đơn thành công !!").ResponseModel;
        }

        [Route("delete")]
        [HttpPost]
        public async Task<ResponseModel> Delete(TransactionIdModel apiModel)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.DeleteTransactionDetailAsync(apiModel.TransactionDetailId, userId);
            return new ResponseBuilder().Success("Xóa mã cân thành công !!").ResponseModel;
        }

        [Route("payment/{date_str}")]
        [HttpGet]
        public async Task<ResponseModel> PaymentForBuyer(string date_str)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            DateTime date = DateTime.Now;

            if (date_str != null)
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

            var rs = await _tnrssSupervisor.GetPaymentForBuyersAsync(userId, date);
            return new ResponseBuilder<List<PaymentForBuyer>>().Success("Lấy dữ liệu thanh toán thành công !!").WithData(rs).ResponseModel;
        }

        [Route("buyer/payment")]
        [HttpPost]
        public async Task<ResponseModel> PaymentForBuyer(FinishPaymentBuyerReqModel apiModel)
        {
            var userId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.PaymentForBuyersAsync(apiModel, userId);
            return new ResponseBuilder().Success("Thanh toán thành công !!").ResponseModel;
        }
    }
}
