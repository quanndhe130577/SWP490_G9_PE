using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.ReportModel;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public ReportController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [HttpGet("getall/{date_str}")]
        public async Task<ResponseModel> GetAll(string date_str = null)
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

            var rs = await _tnrssSupervisor.GetReportAsync(date, userId);
            return new ResponseBuilder<ReportDayApiModel>().Success("Lấy thông tin báo cáo thành công !!").WithData(rs).ResponseModel;
        }
    }
}
