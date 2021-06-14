using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Repositories
{
    public interface IFishTypeRepository : IRepositoryBase<FishType>
    {
        Task CreateFishTypeAsync(FishType basket);
        Task UpdateFishTypeAsync(FishType basket, string type, int weight);
        Task DeleteFishTypeByIdAsync(int basketID);
        List<FishType> ListFishTypeByTraderID(int id);
        Task<FishType> FindFishTypeByIdAsync(int basketID);
    }
}
