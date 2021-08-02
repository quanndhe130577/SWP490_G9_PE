using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.PurchaseModal;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/purchase")]
    [ApiController]
    [Authorize]
    public class PurchaseController : ControllerBase
    {

        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public PurchaseController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        #region create purchase
        [HttpPost("create")]
        public async Task<ResponseModel> CreatePurchase(PurchaseCreateReqModel purchaseData)
        {
            if (!TokenManagement.CheckUserIdFromToken(HttpContext, purchaseData.TraderID))
            {
                return new ResponseBuilder().Error("Truy cập bị trừ chối").ResponseModel;
            }

            var newData = await _tnrssSupervisor.CreatePurchaseAsync(purchaseData);

            return new ResponseBuilder<PurchaseResModel>().Success("Thêm đơn mua thành công").WithData(newData).ResponseModel;
        }
        #endregion

        #region Get all purchase
        [HttpGet("getall")]
        public async Task<ResponseModel> GetAll()
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);

            var newData = await _tnrssSupervisor.GetAllPurchaseAsync(traderId);

            return new ResponseBuilder<List<PurchaseResModel>>().Success("Lấy thông tin tất cả đơn mua thành công").WithData(newData).ResponseModel;
        }
        #endregion

        #region Get one purchase
        [HttpGet("getone/{purchaseId}")]
        public async Task<ResponseModel> Get(int purchaseId)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);

            var newData = await _tnrssSupervisor.GetPurchaseByIdAsync(purchaseId, traderId);

            return new ResponseBuilder<PurchaseResModel>().Success("Lấy thông tin đơn mua thành công").WithData(newData).ResponseModel;
        }
        #endregion

        #region Update Purchase
        [HttpPost("update")]
        public async Task<ResponseModel> Update(PurchaseApiModel data)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.UpdatePurchaseAsync(data, traderId);
            return new ResponseBuilder().Success("Cập nhật đơn mua thành công").ResponseModel;
        }
        #endregion

        #region Chốt sổ Purchase
        [HttpPost("chot-so")]
        public async Task<ResponseModel> ChotSo(ChotSoApiModel data)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            var purchaseRes = await _tnrssSupervisor.ChotSoAsync(data, traderId);
            return new ResponseBuilder<PurchaseResModel>().Success("Chốt sổ Đơn mua thành công").WithData(purchaseRes).ResponseModel;
        }
        #endregion

        #region Delete Purchase
        [HttpPost("delete")]
        public async Task<ResponseModel> Delete(IdModel data)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.DeletePurchaseAsync(data.PurchaseId, traderId);
            return new ResponseBuilder().Success("Xóa đơn mua thành công").ResponseModel;
        }
        #endregion

        #region Update PondOwner
        [HttpPost("updatePO")]
        public async Task<ResponseModel> UpdatePondOwner(PurchaseUpdatePondOwnerModel apiModel)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.UpdatePondOwnerInPurchaseAsync(apiModel, traderId);
            return new ResponseBuilder().Success("Cập nhật chủ ao thành công").ResponseModel;
        }
        #endregion
    }
}

