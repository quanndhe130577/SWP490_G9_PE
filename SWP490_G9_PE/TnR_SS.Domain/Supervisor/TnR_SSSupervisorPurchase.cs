using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.FishTypePriceModel;
using TnR_SS.Domain.ApiModels.PurchaseModal;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task<PurchaseApiModel> CreatePurchaseAndFishTypePriceAsync(PurchaseApiModel purchaseModel)
        {
            var purchase = _mapper.Map<PurchaseApiModel, Purchase>(purchaseModel);
            await _unitOfWork.Purchases.CreateAsync(purchase);
            foreach (var ftp in purchaseModel.ListFishTypeWithPrice)
            {
                var fishTypePrice = _mapper.Map<FishTypeWithPriceApiModel, FishTypePrice>(ftp);
                await _unitOfWork.FishTypePrices.CreateAsync(fishTypePrice);
            }
            await _unitOfWork.SaveChangeAsync();
            purchaseModel.ListFishTypeWithPrice = GetAllLastFishTypeByTraderId(purchaseModel.TraderID);
            return purchaseModel;
        }
    }
}
