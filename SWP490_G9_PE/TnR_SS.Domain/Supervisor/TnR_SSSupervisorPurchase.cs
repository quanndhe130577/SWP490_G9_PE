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
        #region Private function
        private double GetTotalWeightPurchase(int purchaseId)
        {
            return _unitOfWork.PurchaseDetails.GetAll(x => x.PurchaseId == purchaseId).Sum(x => x.Weight);
        }

        private async Task<double> GetTotalAmountPurchaseAsync(int purchaseId)
        {
            var totalAmount = 0.0;
            var listPD = _unitOfWork.PurchaseDetails.GetAll(x => x.PurchaseId == purchaseId).ToList();
            foreach (var item in listPD)
            {
                totalAmount += await GetPurchaseDetailPriceAsync(item);
            }

            return totalAmount;
        }

        private async Task<PurchaseResModel> MapPurchaseResModelAsync(Purchase purchase)
        {
            PurchaseResModel newPurchase = _mapper.Map<Purchase, PurchaseResModel>(purchase);
            var pondOwner = await _unitOfWork.PondOwners.FindAsync(purchase.PondOwnerID);
            newPurchase.PondOwnerName = pondOwner.Name;
            newPurchase.TotalWeight = GetTotalWeightPurchase(purchase.ID);
            newPurchase.TotalAmount = await GetTotalAmountPurchaseAsync(purchase.ID);
            newPurchase.Status = purchase.isCompleted.ToString();

            return newPurchase;
        }

        private async Task<double> CalculatePayForPondOwnerAsync(int purchaseId, double commission)
        {
            return await GetTotalAmountPurchaseAsync(purchaseId) - commission;
        }

        private async Task UpdatePayForPondOwnerAsync(int purchaseId)
        {
            var purchase = await _unitOfWork.Purchases.FindAsync(purchaseId);
            if (purchase != null)
            {
                purchase.PayForPondOwner = await CalculatePayForPondOwnerAsync(purchaseId, purchase.Commission);
                _unitOfWork.Purchases.Update(purchase);
                await _unitOfWork.SaveChangeAsync();
            }
        }
        #endregion

        public async Task<List<PurchaseResModel>> GetAllPurchaseAsync(int traderId)
        {
            var listPurchase = _unitOfWork.Purchases.GetAll(x => x.TraderID == traderId)
                .OrderByDescending(x => x.Date).ThenBy(x => x.ID);
            List<PurchaseResModel> list = new List<PurchaseResModel>();
            foreach (var purchase in listPurchase)
            {
                list.Add(await MapPurchaseResModelAsync(purchase));
            }

            return list;
        }

        public async Task<PurchaseResModel> GetPurchaseByIdAsync(int purchaseId, int traderId)
        {
            var purchase = await _unitOfWork.Purchases.FindAsync(purchaseId);
            if (purchase.TraderID != traderId)
            {
                throw new Exception("Đơn mua không tồn tại");
            }

            return await MapPurchaseResModelAsync(purchase);
        }

        public async Task<PurchaseResModel> CreatePurchaseAsync(PurchaseCreateReqModel purchaseModel)
        {
            var purchase = _mapper.Map<PurchaseCreateReqModel, Purchase>(purchaseModel);
            await _unitOfWork.Purchases.CreateAsync(purchase);
            await _unitOfWork.SaveChangeAsync();

            PurchaseResModel newPurchase = _mapper.Map<Purchase, PurchaseResModel>(purchase);
            var pondOwner = await _unitOfWork.PondOwners.FindAsync(purchaseModel.PondOwnerID);
            newPurchase.PondOwnerName = pondOwner.Name;
            newPurchase.PondOwnerId = pondOwner.ID;
            newPurchase.Status = PurchaseStatus.Pending.ToString();

            return newPurchase;
        }

        public async Task UpdatePurchaseAsync(PurchaseApiModel model, int traderId)
        {
            var purchase = await _unitOfWork.Purchases.FindAsync(model.ID);

            if (purchase.TraderID != traderId)
            {
                throw new Exception("Đơn mua không tồn tại");
            }

            /*if (purchase.isCompleted == PurchaseStatus.Completed)
            {
                throw new Exception("Đơn mua này đã được chốt sổ. Bạn không thể chỉnh sửa thêm nữa !!!");
            }*/

            var pondOwner = await _unitOfWork.PondOwners.FindAsync(model.PondOwnerID);
            if (pondOwner == null || pondOwner.TraderID != traderId)
            {
                throw new Exception("Chủ ao không hợp lệ");
            }

            purchase = _mapper.Map<PurchaseApiModel, Purchase>(model, purchase);
            purchase.PayForPondOwner = await CalculatePayForPondOwnerAsync(purchase.ID, purchase.Commission);
            /*if (model.Status.Equals(PurchaseStatus.Completed.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                purchase.isCompleted = PurchaseStatus.Completed;
            }*/

            _unitOfWork.Purchases.Update(purchase);

            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<PurchaseResModel> ChotSoAsync(ChotSoApiModel data, int traderId)
        {
            var purchase = await _unitOfWork.Purchases.FindAsync(data.ID);

            if (purchase.TraderID != traderId)
            {
                throw new Exception("Đơn mua không tồn tại !!!");
            }

            if (purchase.isCompleted.Equals(PurchaseStatus.Completed))
            {
                if (purchase.isCompleted == PurchaseStatus.Completed)
                {
                    throw new Exception("Đơn mua này đã được chốt !!");
                }
            }

            var totalAmount = await  GetTotalAmountPurchaseAsync(data.ID);
            purchase.Commission = totalAmount * data.CommissionPercent / 100;
            purchase.PayForPondOwner = totalAmount - purchase.Commission;
            purchase.isCompleted = PurchaseStatus.Completed;

            _unitOfWork.Purchases.Update(purchase);
            await _unitOfWork.SaveChangeAsync();

            return await MapPurchaseResModelAsync(purchase);
        }

        public async Task DeletePurchaseAsync(int purchaseId, int traderId)
        {
            try
            {
                var purchase = await _unitOfWork.Purchases.FindAsync(purchaseId);
                if (purchase == null)
                {
                    throw new Exception("Đơn mua không tồn tại !!!");
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
                                        _unitOfWork.LK_PurchaseDetail_Drums.RemoveLKByPurchaseDetailId(item.ID);
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
                    throw new Exception("Đơn mua không hợp lệ !!!");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
