using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.DrumModel;
using TnR_SS.Domain.ApiModels.PurchaseModal;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        #region Private function
        private double GetTotalWeightPurchase(Purchase purchase)
        {
            if (purchase.isCompleted == PurchaseStatus.Completed)
            {
                var listClosePD = _unitOfWork.ClosePurchaseDetails.GetAllByPurchase(purchase);
                if (listClosePD.Count() != 0)
                {
                    return listClosePD.Sum(x => x.Weight);
                }
            }

            return _unitOfWork.PurchaseDetails.GetAll(x => x.PurchaseId == purchase.ID).Sum(x => x.Weight);
        }

        private async Task<double> GetTotalAmountPurchaseAsync(int purchaseId)
        {
            var purchase = await _unitOfWork.Purchases.FindAsync(purchaseId);
            if (purchase.isCompleted == PurchaseStatus.Completed)
            {
                var totalAmountIf = 0.0;
                var listPDIf = _unitOfWork.ClosePurchaseDetails.GetAllByPurchase(purchase);
                if (listPDIf.Count() != 0)
                {
                    foreach (var item in listPDIf)
                    {
                        totalAmountIf += (item.Weight - item.BasketWeight) * item.FishTypePrice;
                    }

                    return totalAmountIf;
                }

            }
            // nếu đơn chưa được chốt sổ
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
            newPurchase.PondOwnerName = pondOwner != null ? pondOwner.Name : "Cá cũ";
            newPurchase.TotalWeight = GetTotalWeightPurchase(purchase);
            newPurchase.TotalAmount = await GetTotalAmountPurchaseAsync(purchase.ID);
            newPurchase.Status = purchase.isCompleted.ToString();
            if (purchase.isCompleted != PurchaseStatus.Pending && purchase.SentMoney >= purchase.PayForPondOwner)
            {
                newPurchase.isPaid = true;
            }

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
            //await _unitOfWork.Purchases.ClearPurchaseAsync();
            var listPurchase = _unitOfWork.Purchases.GetAll(x => x.TraderID == traderId)
                .OrderByDescending(x => x.Date).ThenByDescending(x => x.ID);
            List<PurchaseResModel> list = new List<PurchaseResModel>();
            foreach (var purchase in listPurchase)
            {
                if (purchase.isCompleted == PurchaseStatus.Completed)
                {
                    var countClosePurchase = _unitOfWork.ClosePurchaseDetails.GetAll(x => x.PurchaseId == purchase.ID).Count();
                    var countPurchase = _unitOfWork.PurchaseDetails.GetAll(x => x.PurchaseId == purchase.ID).Count();
                    if (countClosePurchase == 0 && countPurchase != 0)
                    {
                        purchase.isCompleted = PurchaseStatus.Pending;
                        _unitOfWork.Purchases.Update(purchase);
                        await _unitOfWork.SaveChangeAsync();
                    }
                }

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
            var pondOwner = await _unitOfWork.PondOwners.FindAsync(purchaseModel.PondOwnerID);
            if (pondOwner is null)
            {
                throw new Exception("Không tìm thấy chủ ao !!!");
            }

            var trader = await _unitOfWork.UserInfors.FindAsync(purchaseModel.TraderID);
            if (trader is null)
            {
                throw new Exception("Không tìm thấy thương lái !!!");
            }

            var roles = await _unitOfWork.UserInfors.GetRolesAsync(purchaseModel.TraderID);
            if (!roles.Contains(RoleName.Trader))
            {
                throw new Exception("Không tìm thấy thương lái !!!");
            }

            var checkPurchase = _unitOfWork.Purchases.GetAll(x => x.TraderID == purchaseModel.TraderID && x.Date.Date == purchaseModel.Date.Date && x.PondOwnerID == purchaseModel.PondOwnerID).FirstOrDefault();
            if (checkPurchase != null)
            {
                throw new Exception("Đơn mua với chủ ao trong ngày " + purchaseModel.Date.ToString("dd/MM/yyyy") + " đã có!!!");
            }

            var purchase = _mapper.Map<PurchaseCreateReqModel, Purchase>(purchaseModel);
            await _unitOfWork.Purchases.CreateAsync(purchase);
            await _unitOfWork.SaveChangeAsync();

            PurchaseResModel newPurchase = _mapper.Map<Purchase, PurchaseResModel>(purchase);
            newPurchase.PondOwnerName = pondOwner.Name;
            newPurchase.PondOwnerId = pondOwner.ID;
            newPurchase.Status = PurchaseStatus.Pending.ToString();

            return newPurchase;
        }

        public async Task UpdatePurchaseAsync(UpdatePurchaseApiModel model, int traderId)
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

            var checkPurchase = _unitOfWork.Purchases.GetAll(x => x.TraderID == traderId && x.Date.Date == model.Date.Date && x.PondOwnerID == model.PondOwnerID).FirstOrDefault();
            if (checkPurchase != null)
            {
                throw new Exception("Đơn mua với chủ ao trong ngày " + model.Date.ToString("dd/MM/yyyy") + " đã có!!!");
            }

            purchase = _mapper.Map<UpdatePurchaseApiModel, Purchase>(model, purchase);
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
            var strategy = _unitOfWork.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        var purchase = await _unitOfWork.Purchases.FindAsync(data.ID);

                        if (purchase.TraderID != traderId)
                        {
                            throw new Exception("Đơn mua không tồn tại !!!");
                        }

                        if (purchase.isCompleted.Equals(PurchaseStatus.Completed))
                        {
                            throw new Exception("Đơn mua này đã được chốt !!");
                        }

                        var totalAmount = await GetTotalAmountPurchaseAsync(data.ID);
                        purchase.Commission = totalAmount * data.CommissionPercent / 100;
                        purchase.PayForPondOwner = totalAmount - purchase.Commission;
                        purchase.isCompleted = PurchaseStatus.Completed;
                        //purchase.isPaid = data.IsPaid;
                        if (data.IsPaid)
                        {
                            purchase.SentMoney = purchase.PayForPondOwner;
                        }
                        else
                        {
                            purchase.SentMoney = data.SentMoney;
                        }


                        _unitOfWork.Purchases.Update(purchase);
                        await _unitOfWork.SaveChangeAsync();

                        // create close purchase detail
                        var listPD = _unitOfWork.PurchaseDetails.GetAll(x => x.PurchaseId == purchase.ID);
                        foreach (var item in listPD)
                        {
                            ClosePurchaseDetail closePD = new ClosePurchaseDetail();
                            closePD.Price = await GetPurchaseDetailPriceAsync(item.ID);
                            closePD.Weight = item.Weight;
                            closePD.PurchaseId = purchase.ID;

                            Basket bk = await _unitOfWork.Baskets.FindAsync(item.BasketId);
                            closePD.BasketId = bk.ID;
                            closePD.BasketType = bk.Type;
                            closePD.BasketWeight = bk.Weight;

                            FishType ft = await _unitOfWork.FishTypes.FindAsync(item.FishTypeID);
                            closePD.FishTypeId = ft.ID;
                            closePD.FishName = ft.FishName;
                            closePD.FishTypeDescription = ft.Description;
                            closePD.FishTypeMinWeight = ft.MinWeight;
                            closePD.FishTypeMaxWeight = ft.MaxWeight;
                            closePD.FishTypePrice = ft.Price;
                            closePD.FishTypeTransactionPrice = ft.TransactionPrice;

                            await _unitOfWork.ClosePurchaseDetails.CreateAsync(closePD);
                            await _unitOfWork.SaveChangeAsync();

                            _unitOfWork.LK_PurchaseDetail_Drums.AddClosePurchaseDetailId(item.ID, closePD.ID);
                            await _unitOfWork.SaveChangeAsync();
                        }

                        var rs = await MapPurchaseResModelAsync(purchase);
                        await transaction.CommitAsync();
                        return rs;
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            });

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
                                var checkFish = _unitOfWork.FishTypes.CheckFishTypeOfPurchaseInUse(purchaseId);
                                if (checkFish)
                                {
                                    throw new Exception("Cá đã được bán, không thể xóa !!!");
                                }

                                var purchaseDetail = _unitOfWork.PurchaseDetails.GetAll(x => x.PurchaseId == purchaseId);
                                if (purchaseDetail != null && purchaseDetail.Count() > 0)
                                {
                                    foreach (var item in purchaseDetail)
                                    {
                                        _unitOfWork.LK_PurchaseDetail_Drums.RemoveLKByPurchaseDetailId(item.ID);
                                        await _unitOfWork.SaveChangeAsync();
                                        //await _unitOfWork.ClosePurchaseDetails.DeleteByPurchaseDetailIdAsync(item.ID);
                                        _unitOfWork.PurchaseDetails.Delete(item);
                                    }
                                }

                                // remove close purchase detail
                                await _unitOfWork.ClosePurchaseDetails.DeleteByPurchaseIdAsync(purchaseId);

                                // set purchaseid of fishtype = null
                                await _unitOfWork.FishTypes.RemoveFishTypeByPurchaseId(purchaseId);
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
            catch
            {
                throw;
            }
        }

        public async Task UpdatePondOwnerInPurchaseAsync(PurchaseUpdatePondOwnerModel apiModel, int traderId)
        {
            var purchase = await _unitOfWork.Purchases.FindAsync(apiModel.PurchaseId);
            if (purchase.TraderID != traderId)
            {
                throw new Exception("Đơn mua không hợp lệ !!!");
            }

            if (purchase.isCompleted == PurchaseStatus.Completed)
            {
                throw new Exception("Đơn mua đã được chốt không thể thay đổi !!!");
            }

            var pO = await _unitOfWork.PondOwners.FindAsync(apiModel.PondOwnerId);
            if (pO == null)
            {
                throw new Exception("Chủ ao không hợp lệ !!!");
            }

            var checkPurchase = _unitOfWork.Purchases.GetAll(x => x.TraderID == traderId && x.Date.Date == purchase.Date.Date && x.PondOwnerID == apiModel.PondOwnerId).FirstOrDefault();
            if (checkPurchase != null)
            {
                throw new Exception("Đơn mua với chủ ao " + pO.Name + " trong ngày " + purchase.Date.ToString("dd/MM/yyyy") + " đã có!!!");
            }

            purchase.PondOwnerID = apiModel.PondOwnerId;

            _unitOfWork.Purchases.Update(purchase);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
