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
        public bool CheckRoExist(string typeRo)
        {
            List<Basket> list = _basketRepository.ListAllRo();
            var rs = list.FirstOrDefault(x => x.Type == typeRo);
            return rs is not null;
        }

        public async Task<bool> CreateRo(string typeRo, int weight)
        {
            if (typeRo != null && weight != 0)
            {
                Basket ro = new() { Type = typeRo, Weight = weight };
                await _basketRepository.CreateRoAsync(ro);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateRo(Basket ro, string typeRo, int weight)
        {
            if (typeRo != null && weight != 0)
            {
                ro = new Basket() { Type = typeRo, Weight = weight };
                await _basketRepository.UpdateRoAsync(ro);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
