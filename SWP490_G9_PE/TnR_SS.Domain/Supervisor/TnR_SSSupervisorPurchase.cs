using System.Collections.Generic;
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

            return newPurchase;
        }

        private double GetTotalWeightPurchase(int purchaseId)
        {
            var totalWeight = 0.0;
            var listPD = _unitOfWork.PurchaseDetails.GetAll(x => x.PurchaseId == purchaseId);
            foreach (var item in listPD)
            {
                totalWeight += GetPurchaseDetailWeight(item.ID);
            }

            return totalWeight;
        }

        private async Task<double> GetTotalAmountPurchaseAsync(int purchaseId)
        {
            var totalAmount = 0.0;
            var listPD = _unitOfWork.PurchaseDetails.GetAll(x => x.PurchaseId == purchaseId);
            foreach (var item in listPD)
            {
                totalAmount += await GetPurchaseDetailPriceAsync(item.ID);
            }

            return totalAmount;
        }

        public async Task<List<PurchaseResModel>> GetAllPurchaseAsync(int traderId)
        {
            var listPurchase = _unitOfWork.Purchases.GetAll(x => x.TraderID == traderId);
            List<PurchaseResModel> list = new List<PurchaseResModel>();
            foreach (var purchase in listPurchase)
            {
                PurchaseResModel newPurchase = _mapper.Map<Purchase, PurchaseResModel>(purchase);
                var pondOwner = await _unitOfWork.PondOwners.FindAsync(purchase.PondOwnerID);
                newPurchase.PondOwnerName = pondOwner.Name;
                newPurchase.TotalWeight = GetTotalWeightPurchase(purchase.ID);
                newPurchase.TotalAmount = await GetTotalAmountPurchaseAsync(purchase.ID);

                list.Add(newPurchase);
            }

            return list;
        }
    }
}
