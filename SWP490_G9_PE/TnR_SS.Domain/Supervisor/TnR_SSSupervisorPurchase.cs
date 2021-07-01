using Microsoft.EntityFrameworkCore;
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
                newPurchase.Status = purchase.isCompleted.ToString();

                list.Add(newPurchase);
            }

            return list;
        }

        public async Task UpdatePurchaseAsync(PurchaseApiModel model, int traderId)
        {
            var purchase = await _unitOfWork.Purchases.FindAsync(model.ID);
            if (purchase.isCompleted == PurchaseStatus.Completed)
            {
                throw new Exception("You can't edit this purchase anymore !!");
            }

            if (purchase.TraderID != traderId)
            {
                throw new Exception("Purchase không tồn tại");
            }

            var pondOwner = await _unitOfWork.PondOwners.FindAsync(model.PondOwnerID);
            if (pondOwner == null || pondOwner.TraderID != traderId)
            {
                throw new Exception("PondOwner không hợp lệ");
            }

            purchase = _mapper.Map<PurchaseApiModel, Purchase>(model, purchase);
            if (model.Status.Equals(PurchaseStatus.Completed.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                purchase.isCompleted = PurchaseStatus.Completed;
            }

            _unitOfWork.Purchases.Update(purchase);

            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeletePurchaseAsync(int purchaseId, int traderId)
        {
            var purchase = await _unitOfWork.Purchases.FindAsync(purchaseId);
            if (purchase == null)
            {
                throw new Exception("Purchase không tồn tại !!!");
            }
            if (purchase.TraderID == traderId)
            {
                var strategy = _unitOfWork.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    using (var transaction = _unitOfWork.BeginTransaction())
                    {
                        try
                        {
                            var purchaseDetail = _unitOfWork.PurchaseDetails.GetAll(x => x.PurchaseId == purchaseId);
                            if (purchaseDetail != null && purchaseDetail.Count() > 0)
                            {
                                foreach (var item in purchaseDetail)
                                {
                                    _unitOfWork.LK_PurchaseDeatil_Drums.RemoveLKByPurchaseDetailId(item.ID);
                                    _unitOfWork.PurchaseDetails.Delete(item);
                                }
                            }

                            _unitOfWork.Purchases.DeleteById(purchaseId);
                            await _unitOfWork.SaveChangeAsync();

                            await transaction.CommitAsync();
                        }
                        catch
                        {
                            await transaction.RollbackAsync();
                            throw;
                        }
                    }
                });

            }
            else
            {
                throw new Exception("Purchase không hợp lệ !!!");
            }
        }
    }
}
