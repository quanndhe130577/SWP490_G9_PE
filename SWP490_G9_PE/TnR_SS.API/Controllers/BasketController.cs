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
            return new ResponseBuilder().Success("Create Basket Success").ResponseModel;
        }

        [HttpGet("getall")]
        public ResponseModel GetAllBasket()
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            List<BasketApiModel> list = _tnrssSupervisor.GetAllBasket(traderId);
            return new ResponseBuilder<List<BasketApiModel>>().Success("Get all basket").WithData(list).ResponseModel;
        }

        [HttpPost("update")]
        public async Task<ResponseModel> UpdateBasketAsync(BasketApiModel basketApi)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            List<BasketApiModel> list = _tnrssSupervisor.GetAllBasket(traderId);
            await _tnrssSupervisor.UpdateBasketAsync(basketApi, traderId);
            return new ResponseBuilder<List<BasketApiModel> >().Success("Update Basket Success").WithData(list).ResponseModel;
        }

        [HttpPost("delete/{basketId}")]
        public async Task<ResponseModel> DeleteBasketAsync(int basketId)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.DeleteBasketAsync(basketId, traderId);
            return new ResponseBuilder().Success("Delete Basket Success").ResponseModel;
        }
    }
}
