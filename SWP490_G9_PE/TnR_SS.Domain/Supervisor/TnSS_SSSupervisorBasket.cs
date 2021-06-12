using System;
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
            return (List<BasketApiModel>)_basketRepository.GetAllAsync();
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
