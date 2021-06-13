﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.BasketModel.ResponseModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public List<BasketApiModel> GetAllBasket()
        {
            var listBaskets = _basketRepository.GetAllAsync();
            List<BasketApiModel> list = new List<BasketApiModel>();
            foreach (var basket in listBaskets)
            {
                list.Add(_mapper.Map<Basket, BasketApiModel>(basket));
            }
            return list;
        }

        public async Task CreateBasketAsync(BasketApiModel basketRes)
        {
            var basket = _mapper.Map<BasketApiModel, Basket>(basketRes);
            await _basketRepository.CreateAsync(basket);
        }

        public async Task UpdateBasketAsync(BasketApiModel basketRes)
        {
            var basket = await _basketRepository.FindAsync(basketRes.ID);
            basket = _mapper.Map<BasketApiModel, Basket>(basketRes, basket);
            await _basketRepository.UpdateAsync(basket);
        }
    }
}