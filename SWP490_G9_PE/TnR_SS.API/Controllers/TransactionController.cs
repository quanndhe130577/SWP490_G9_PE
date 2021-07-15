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
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public TransactionController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [Route("createlist")]
        public async Task<ResponseModel> Create(CreateListTransactionModel createModel)
        {
            var wcId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.CreateListTransactionAsync(createModel, wcId);
            return new ResponseBuilder().Success("Tạo hóa đơn thành công !!").ResponseModel;
        }

        [Route("getall/{date_str?}")]
        public async Task<ResponseModel> GetAll(string date_str = null)
        {
            var wcId = TokenManagement.GetUserIdInToken(HttpContext);
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

            var rs = await _tnrssSupervisor.GetAllTransactionAsync(wcId, date);
            return new ResponseBuilder<List<TransactionResModel>>().Success("Lấy thông tin hóa đơn thành công !!").WithData(rs).ResponseModel;

        }
    }
}
