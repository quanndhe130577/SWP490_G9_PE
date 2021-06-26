using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.PurchaseModal;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task<PurchaseResModel> CreatePurchaseAsync(PurchaseReqModel purchaseModel)
        {
            var purchase = _mapper.Map<PurchaseReqModel, Purchase>(purchaseModel);
            await _unitOfWork.Purchases.CreateAsync(purchase);
            await _unitOfWork.SaveChangeAsync();

            PurchaseResModel newPurchase = _mapper.Map<Purchase, PurchaseResModel>(purchase);
            var pondOwner = await _unitOfWork.PondOwners.FindAsync(purchaseModel.PondOwnerID);
            newPurchase.PondOwnerName = pondOwner.Name;

            /*var allFT = _unitOfWork.FishTypes.GetAllLastByTraderId(purchaseModel.TraderID);
            foreach (var r in allFT)
            {
                newPurchase.ListFishTypeWithPrice.Add(_mapper.Map<FishType, FishTypeApiModel>(r));
            }*/

            return newPurchase;
        }
    }
}
