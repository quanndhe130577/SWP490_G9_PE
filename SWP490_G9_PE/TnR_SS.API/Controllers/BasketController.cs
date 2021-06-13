﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
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
        [AllowAnonymous]
        public async Task<ResponseModel> CreateNewBasketAsync(BasketApiModel basketApi)
        {
            await _tnrssSupervisor.CreateBasketAsync(basketApi);
            return new ResponseBuilder<BasketApiModel>().Success("Create Basket Success").WithData(basketApi).ResponseModel;
        }

        [HttpPost("getallbasket")]
        [AllowAnonymous]
        public ResponseModel GetAllBasket()
        {
            List<BasketApiModel> list = _tnrssSupervisor.GetAllBasket();
            return new ResponseBuilder<List<BasketApiModel>>().Success("Get all basket").WithData(list).ResponseModel;
        }
    }
}
