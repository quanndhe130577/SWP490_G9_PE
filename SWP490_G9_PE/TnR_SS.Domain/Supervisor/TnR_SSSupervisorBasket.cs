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
        public List<BasketApiModel> GetAllBasket(int traderId)
        {
            var listBaskets = _unitOfWork.Baskets.GetAllBasketByTraderId(traderId);
            List<BasketApiModel> list = new List<BasketApiModel>();
            foreach (var type in listBaskets)
            {
                list.Add(_mapper.Map<Basket, BasketApiModel>(type));
            }
            return list;
        }

        public async Task CreateBasketAsync(BasketApiModel basketRes, int traderId)
        {
            var basket = _mapper.Map<BasketApiModel, Basket>(basketRes);
            basket.TraderID = traderId;
            await _unitOfWork.Baskets.CreateAsync(basket);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task UpdateBasketAsync(BasketApiModel basketModel, int traderId)
        {
            var basketEdit = await _unitOfWork.Baskets.FindAsync(basketModel.ID);
            basketEdit = _mapper.Map<BasketApiModel, Basket>(basketModel, basketEdit);
            if (basketEdit.TraderID == traderId)
            {
                _unitOfWork.Baskets.Update(basketEdit);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Update fail");
            }
        }

        public async Task DeleteBasketAsync(int basketId, int traderId)
        {
            var basketEdit = await _unitOfWork.Baskets.FindAsync(basketId);
            if (basketEdit.TraderID == traderId)
            {
                _unitOfWork.Baskets.Delete(basketEdit);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Thông tin rổ không đúng");
            }
        }
    }
}
