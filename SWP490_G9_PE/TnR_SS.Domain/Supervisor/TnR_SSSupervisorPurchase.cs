using System;
using System.Collections.Generic;
using System.Linq;
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
            newPurchase.PondOwnerId = pondOwner.ID;

            return newPurchase;
        }

        private double GetTotalWeightPurchase(int purchaseId)
        {
            return _unitOfWork.PurchaseDetails.GetAll(x => x.PurchaseId == purchaseId).Sum(x => x.Weight);
        }

        private async Task<double> GetTotalAmountPurchaseAsync(int purchaseId)
        {
            var totalAmount = 0.0;
            var listPD = _unitOfWork.PurchaseDetails.GetAll(x => x.PurchaseId == purchaseId);
            foreach (var item in listPD)
            {
                totalAmount += await GetPurchaseDetailPriceAsync(item);
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

        public async Task UpdatePurchaseAsync(PurchaseApiModel model, int traderId)
        {
            var purchase = await _unitOfWork.Purchases.FindAsync(model.ID);
            if (purchase.TraderID != traderId)
            {
                throw new Exception("Purchase không tồn tại");
            }

            purchase = _mapper.Map<PurchaseApiModel, Purchase>(model, purchase);

            _unitOfWork.Purchases.Update(purchase);

            await _unitOfWork.SaveChangeAsync();
        }
    }
}
