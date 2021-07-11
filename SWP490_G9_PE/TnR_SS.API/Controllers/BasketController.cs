using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.BasketModel.ResponseModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public BasketController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [HttpPost("create")]
        public async Task<ResponseModel> CreateNewBasketAsync(BasketApiModel basketApi)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.CreateBasketAsync(basketApi, traderId);
            return new ResponseBuilder().Success("Tạo rổ thành công").ResponseModel;
        }

        [HttpGet("getall")]
        public ResponseModel GetAllBasket()
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            List<BasketApiModel> list = _tnrssSupervisor.GetAllBasket(traderId);
            return new ResponseBuilder<List<BasketApiModel>>().Success("Lấy thông tin rổ thành công").WithData(list).ResponseModel;
        }

        [HttpPost("update")]
        public async Task<ResponseModel> UpdateBasketAsync(BasketApiModel basketApi)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.UpdateBasketAsync(basketApi, traderId);
            return new ResponseBuilder<List<BasketApiModel> >().Success("Cập nhật thông tin rổ thành công").WithData(_tnrssSupervisor.GetAllBasket(traderId)).ResponseModel;
        }

        [HttpPost("delete/{basketId}")]
        public async Task<ResponseModel> DeleteBasketAsync(int basketId)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.DeleteBasketAsync(basketId, traderId);
            return new ResponseBuilder().Success("Xóa thông tin rổ thành công").ResponseModel;
        }
    }
}
