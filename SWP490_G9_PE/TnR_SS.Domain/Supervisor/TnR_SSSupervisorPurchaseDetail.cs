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
            await _unitOfWork.SaveChangeWithoutCommitAsync();
        }

        public async Task<int> CreatePurchaseDetailAsync(PurchaseDetailReqModel data)
        {
            try
            {
                var fishType = await _unitOfWork.FishTypes.FindAsync(data.FishTypeID);
                var basket = await _unitOfWork.Baskets.FindAsync(data.BasketId);
                var purchaseDetail = _mapper.Map<PurchaseDetailReqModel, PurchaseDetail>(data);
                double totalFishWeight = data.ListDrum.Sum(x => x.Weight) - basket.Weight;
                purchaseDetail.BuyPrice = fishType.Price * totalFishWeight;
                await _unitOfWork.PurchaseDetails.CreateAsync(purchaseDetail);
                await _unitOfWork.SaveChangeWithoutCommitAsync();
                //create lk
                await CreateLK(data.ListDrum, purchaseDetail.ID);
                await _unitOfWork.CommitAsync();
                return purchaseDetail.ID;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

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
                data.BuyPrice = item.BuyPrice;
                data.Weight = _unitOfWork.LK_PurchaseDeatil_Drums.GetAll(x => x.PurchaseDetailID == item.ID).Sum(x => x.Weight);
                list.Add(data);
            }

            return list;
        }

        public async Task UpdatePurchaseDetailAsync(PurchaseDetailReqModel data)
        {
            var purchaseDetail = await _unitOfWork.PurchaseDetails.FindAsync(data.Id);
            purchaseDetail = _mapper.Map<PurchaseDetailReqModel, PurchaseDetail>(data, purchaseDetail);

            // delete current LK
            _unitOfWork.LK_PurchaseDeatil_Drums.DeleteMany(x => x.PurchaseDetailID == data.Id);

            // create new LK
            await CreateLK(data.ListDrum, data.Id);

            await _unitOfWork.SaveChangeAsync();
        }
    }
}
