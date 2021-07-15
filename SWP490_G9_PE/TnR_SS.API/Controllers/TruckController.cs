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
using TnR_SS.Domain.ApiModels.TruckModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/truck")]
    [ApiController]
    //[Authorize(Roles = RoleName.Trader)]
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

            return new ResponseBuilder<object>().Success("Lấy thông tin xe tải thành công").WithData(listTruck).ResponseModel;
        }

        [HttpPost("create")]
        public async Task<ResponseModel> CreateTruck(TruckApiModel truckModel)
        {
            if (string.IsNullOrEmpty(truckModel.Name))
            {
                return new ResponseBuilder<List<TruckApiModel>>().Error("Tên bị để trống").ResponseModel;
            }

            if (string.IsNullOrEmpty(truckModel.LicensePlate))
            {
                return new ResponseBuilder<List<TruckApiModel>>().Error("Thông tin bị để trống").ResponseModel;
            }

            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            var truckId = await _tnrssSupervisor.CreateTruckAsync(truckModel, traderId);
            return new ResponseBuilder<object>().Success("Tạo xe mới thành công").WithData(new { truckId = truckId }).ResponseModel;
        }

        [HttpPost("update")]
        public async Task<ResponseModel> UpdateTruck(TruckApiModel truckModel)
        {
            if (string.IsNullOrEmpty(truckModel.Name))
            {
                return new ResponseBuilder<List<TruckApiModel>>().Error("Tên bị để trống").ResponseModel;
            }
            if (string.IsNullOrEmpty(truckModel.LicensePlate))
            {
                return new ResponseBuilder<List<TruckApiModel>>().Error("Thông tin bị để trống").ResponseModel;
            }
            Truck truck = await _tnrssSupervisor.GetTruck(truckModel.Id);
            if (truck == null)
            {
                return new ResponseBuilder<List<TruckApiModel>>().Error("Không tìm thấy truck").ResponseModel;
            }
            var truckId = await _tnrssSupervisor.UpdateTruckAsync(truckModel);
            return new ResponseBuilder<object>().Success("Cập nhật thông tin xe thành công").ResponseModel;
        }

        [HttpPost("delete/{id}")]
        public async Task<ResponseModel> DeleteTruck(int id)
        {
            Truck truck = await _tnrssSupervisor.GetTruck(id);
            if (truck == null)
            {
                return new ResponseBuilder<object>().Error("Không tìm thấy xe").ResponseModel;
            }
            await _tnrssSupervisor.DeleteTruck(truck);
            return new ResponseBuilder<object>().Success("Xóa thông tin xe thành công").ResponseModel;
        }

        [HttpGet("getall/{date_str}")]
        public async Task<ResponseModel> GetDetailTrucksByDate(string date_str)
        {
            DateTime date = DateTime.Now;
            CultureInfo enUS = new CultureInfo("en-US");
            if (DateTime.TryParseExact(date_str, "ddMMyyyy", enUS, DateTimeStyles.None, out date))
            {
                var traderId = TokenManagement.GetUserIdInToken(HttpContext);
                var rs = await _tnrssSupervisor.GetDetailTrucksByDate(traderId, date);
                return new ResponseBuilder<List<TruckDateModel>>().Success("Lấy thông tin xe thành công").WithData(rs).ResponseModel;
            }
            else
            {
                return new ResponseBuilder().Error("Lỗi format date !!!").ResponseModel;
            }

        }
    }
}
