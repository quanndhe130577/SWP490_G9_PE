using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.BasketModel.ResponseModel;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.PurchaseDetailModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task<int> CreatePurchaseDetailAsync(PurchaseDetailReqModel data)
        {
            var fishType = await _unitOfWork.FishTypes.FindAsync(data.FishTypeID);
            var basket = await _unitOfWork.Baskets.FindAsync(data.BasketId);
            var purchaseDetail = _mapper.Map<PurchaseDetailReqModel, PurchaseDetail>(data);
            double totalFishWeight = data.ListDrum.Sum(x => x.Weight) - basket.Weight;
            purchaseDetail.BuyPrice = fishType.Price * totalFishWeight;
            await _unitOfWork.PurchaseDetails.CreateAsync(purchaseDetail);
            return purchaseDetail.ID;
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
    }
}
