using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Basket ro = new() { Type = typeRo, Weight = weight };
                await _basketRepository.CreateBasketAsync(ro);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateBasket(Basket ro, string typeRo, int weight)
        {
            if (typeRo != null && weight != 0)
            {
                ro = new Basket() { Type = typeRo, Weight = weight };
                await _basketRepository.UpdateBasketAsync(ro, typeRo, weight);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
