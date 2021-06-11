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
            List<Ro> list = _roRepository.ListAllRo();
            var rs = list.FirstOrDefault(x => x.Type == typeRo);
            return rs is not null;
        }

        public async Task<bool> CreateRo(string typeRo, int weight)
        {
            if (typeRo != null && weight != 0)
            {
                Ro ro = new() { Type = typeRo, Weight = weight };
                await _roRepository.CreateRoAsync(ro);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateRo(Ro ro, string typeRo, int weight)
        {
            if (typeRo != null && weight != 0)
            {
                ro = new() { Type = typeRo, Weight = weight };
                await _roRepository.UpdateRoAsync(ro);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
