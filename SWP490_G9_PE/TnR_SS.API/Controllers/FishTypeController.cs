using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.FishTypeModel.ResponseModel;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/fishtype")]
    [ApiController]
    [Authorize]
    public class FishTypeController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public FishTypeController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [HttpPost("createlist")]
        public async Task<ResponseModel> CreateFishTypeAsync(ListFishTypeModel listType)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.CreateListFishTypeAsync(listType.ListFishType, traderId);
            return new ResponseBuilder().Success("Tạo giá cá thành công").ResponseModel;
        }

        [HttpPost("create")]
        public async Task<ResponseModel> CreateFishTypeAsync(FishTypeApiModel fishType)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.CreateFishTypeAsync(fishType, traderId);
            return new ResponseBuilder().Success("Tạo giá cá thành công").ResponseModel;
        }

        [HttpGet("getlastall")]
        public async Task<ResponseModel> GetLastAllFishType()
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            var fishTypes = await _tnrssSupervisor.GetAllLastFishTypeWithPondOwnerId(traderId);
            return new ResponseBuilder<List<FishTypeApiModel>>().Success("Lấy thông tin giá cá thành công").WithData(fishTypes).ResponseModel;
        }

        [HttpGet("getall")]
        public ResponseModel GetAllFishType()
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            var fishTypes = _tnrssSupervisor.GetAllFishTypeByTraderIdAsync(traderId);
            return new ResponseBuilder<List<FishTypeResModel>>().Success("Lấy thông tin giá cá thành công").WithData(fishTypes).ResponseModel;
        }

        [HttpGet("getone/{date_str}/{poId}")]
        public async Task<ResponseModel> GetByDate(string date_str, int poId)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            DateTime date = DateTime.Now;
            CultureInfo enUS = new CultureInfo("en-US");
            if (DateTime.TryParseExact(date_str, "ddMMyyyy", enUS, DateTimeStyles.None, out date))
            {
                var fishTypes = await _tnrssSupervisor.GetFishTypesByPondOwnerIdAndDate(traderId, poId, date);
                return new ResponseBuilder<List<FishTypeApiModel>>().Success("Lấy thông tin loại cá thành công").WithData(fishTypes).ResponseModel;
            }
            else
            {
                return new ResponseBuilder().Error("Lỗi format date !!!").ResponseModel;
            }
        }

        [HttpGet("getall/{purchaseId}")]
        public async Task<ResponseModel> GetByPurchaseId(int purchaseId)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            var fishTypes = await _tnrssSupervisor.GetListFishTypeByPurchaseIdAsync(purchaseId, traderId);
            return new ResponseBuilder<List<FishTypeApiModel>>().Success("Lấy thông tin loại cá thành công").WithData(fishTypes).ResponseModel;
        }

        [HttpPost("update")]
        public async Task<ResponseModel> UpdateFishTypeAsync(FishTypeApiModel fishType)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.UpdateFishTypeAsync(fishType, traderId);
            return new ResponseBuilder().Success("Cập nhật giá cá thành công").ResponseModel;
        }

        [HttpPost("updatelist")]
        public async Task<ResponseModel> UpdateListFishTypeAsync(ListFishTypeModel listFishType)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.UpdateListFishTypeAsync(listFishType, traderId);
            return new ResponseBuilder<List<FishTypeApiModel>>().Success("Cập nhật giá cá thành công").WithData(listFishType.ListFishType).ResponseModel;
        }

        [HttpPost("delete/{fishTypeId}")]
        public async Task<ResponseModel> DeleteFishTypeAsync(int fishTypeId)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.DeleteFishTypeAsync(fishTypeId, traderId);
            return new ResponseBuilder().Success("Xóa giá cá thành công").ResponseModel;
        }
    }
}
