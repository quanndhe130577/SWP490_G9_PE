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
        List<FishType> GetAllLastByTraderIdAndPondOwnerId(int traderId);
        List<FishType> GetAllByTraderId(int traderId);
        void RemoveFishTypeByPurchaseId(int purchaseId);
        double GetTotalWeightOfFishType(int fishTypeId);
        double GetSellWeightOfFishType(int fishTypeId);
        List<FishType> GetAllFishTypeByPurchaseIds(List<int> listPurchaseId);
        bool CheckFishTypeOfPurchaseInUse(int purchaseId);
        List<FishType> GetAllFishTypeForTransaction(int? traderId, int userId, DateTime date);
    }
}
