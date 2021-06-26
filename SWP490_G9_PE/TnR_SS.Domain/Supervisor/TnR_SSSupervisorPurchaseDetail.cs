using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.BasketModel.ResponseModel;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.LK_PurchaseDetail_DrumModel;
using TnR_SS.Domain.ApiModels.PurchaseDetailModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        private async Task CreateLK(List<LK_PurchaseDetail_DrumApiModel> listDrum, int purchaseDetailId)
        {
            foreach (var item in listDrum)
            {
                var lk = _mapper.Map<LK_PurchaseDetail_DrumApiModel, LK_PurchaseDeatil_Drum>(item);
                lk.PurchaseDetailID = purchaseDetailId;
                await _unitOfWork.LK_PurchaseDeatil_Drums.CreateAsync(lk);
            }
            await _unitOfWork.SaveChangeAsync();
        }

        private async Task<double> GetPurchaseDetailPriceAsync(int purchaseId)
        {
            var purchase = await _unitOfWork.PurchaseDetails.FindAsync(purchaseId);
            var fishType = await _unitOfWork.FishTypes.FindAsync(purchase.FishTypeID);
            var basket = await _unitOfWork.Baskets.FindAsync(purchase.BasketId);
            var listDrum = _unitOfWork.LK_PurchaseDeatil_Drums.GetAll(x => x.PurchaseDetailID == purchaseId);
            double totalFishWeight = listDrum.Sum(x => x.Weight) - basket.Weight;
            return totalFishWeight > 0 ? fishType.Price * totalFishWeight : 0;
        }
        private double GetPurchaseDetailPrice(double fishTypePrice, double basketWeight, double totalWeight)
        {
            double totalFishWeight = totalWeight - basketWeight;
            return totalFishWeight > 0 ? fishTypePrice * totalFishWeight : 0;
        }

        private double GetPurchaseDetailWeight(int purchaseDetailId)
        {
            return _unitOfWork.LK_PurchaseDeatil_Drums.GetAll(x => x.PurchaseDetailID == purchaseDetailId).Sum(x => x.Weight);
        }

        public async Task<int> CreatePurchaseDetailAsync(PurchaseDetailReqModel data)
        {
            var strategy = _unitOfWork.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        var fishType = await _unitOfWork.FishTypes.FindAsync(data.FishTypeID);
                        var basket = await _unitOfWork.Baskets.FindAsync(data.BasketId);
                        var purchaseDetail = _mapper.Map<PurchaseDetailReqModel, PurchaseDetail>(data);
                        //double totalFishWeight = data.ListDrum.Sum(x => x.Weight) - basket.Weight;
                        //purchaseDetail.BuyPrice = fishType.Price * totalFishWeight;
                        //purchaseDetail.BuyPrice = GetBuyPrice(data.ListDrum, fishType, basket);
                        await _unitOfWork.PurchaseDetails.CreateAsync(purchaseDetail);
                        await _unitOfWork.SaveChangeAsync();
                        //create lk
                        await CreateLK(data.ListDrum, purchaseDetail.ID);
                        await transaction.CommitAsync();
                        return purchaseDetail.ID;
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            });

        }

        public async Task<List<PurchaseDetailResModel>> GetAllPurchaseDetailAsync(int purchaseId)
        {
            var listPurchaseDetail = _unitOfWork.PurchaseDetails.GetAll(x => x.PurchaseId == purchaseId);

            List<PurchaseDetailResModel> list = new List<PurchaseDetailResModel>();
            foreach (var item in listPurchaseDetail)
            {
                PurchaseDetailResModel data = new PurchaseDetailResModel();
                data.Basket = _mapper.Map<Basket, BasketApiModel>(await _unitOfWork.Baskets.FindAsync(item.BasketId));
                data.FishType = _mapper.Map<FishType, FishTypeApiModel>(await _unitOfWork.FishTypes.FindAsync(item.FishTypeID));
                data.Weight = GetPurchaseDetailWeight(item.ID);
                data.Price = GetPurchaseDetailPrice(data.FishType.Price, data.Basket.Weight, data.Weight);
                list.Add(data);
            }

            return list;
        }

        public async Task UpdatePurchaseDetailAsync(PurchaseDetailReqModel data)
        {
            var strategy = _unitOfWork.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        var purchaseDetail = await _unitOfWork.PurchaseDetails.FindAsync(data.Id);
                        purchaseDetail = _mapper.Map<PurchaseDetailReqModel, PurchaseDetail>(data, purchaseDetail);

                        // delete current LK
                        _unitOfWork.LK_PurchaseDeatil_Drums.DeleteMany(x => x.PurchaseDetailID == data.Id);

                        // create new LK
                        await CreateLK(data.ListDrum, data.Id);

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
    }
}
