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
        public bool CheckBasketExist(string typeRo)
        {
            List<Basket> list = _basketRepository.ListAllBasket();
            var rs = list.FirstOrDefault(x => x.Type == typeRo);
            return rs is not null;
        }

        public async Task<bool> CreateBasket(string typeRo, int weight)
        {
            if (typeRo != null && weight != 0)
            {
                Basket basket = new() { Type = typeRo, Weight = weight };
                await _basketRepository.CreateAsync(basket);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task UpdateBasket(BasketApiModel basketRes)
        {
            var basket = await _basketRepository.FindAsync(basketRes.ID);
            basket = _mapper.Map<BasketApiModel, Basket>(basketRes, basket);
            await _basketRepository.UpdateAsync(basket);
        }
    }
}
