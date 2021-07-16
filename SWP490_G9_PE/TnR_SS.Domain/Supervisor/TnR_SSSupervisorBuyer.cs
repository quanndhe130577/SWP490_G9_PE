using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.BuyerModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public List<BuyerApiModel> GetAllBuyerByWCId(int wcId)
        {
            return _unitOfWork.Buyers.GetAll(x => x.WeightRecorderId == wcId).Select(x => _mapper.Map<Buyer, BuyerApiModel>(x)).ToList();
            /*List<BuyerApiModel> buyers = new();
            foreach (var item in listBuyer)
            {
                buyers.Add(_mapper.Map<Buyer, BuyerApiModel>(item));
            }
            return buyers;*/
        }

        public async Task CreateBuyerAsync(BuyerApiModel model, int wcId)
        {
            var buyer = _mapper.Map<BuyerApiModel, Buyer>(model);
            buyer.WeightRecorderId = wcId;
            await _unitOfWork.Buyers.CreateAsync(buyer);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task UpdateBuyerAsync(BuyerApiModel model, int wcId)
        {
            var buyer = await _unitOfWork.Buyers.FindAsync(model.ID);
            if (buyer.WeightRecorderId != wcId)
            {
                throw new Exception("Thông tin người mua không tồn tại !!!");
            }

            buyer = _mapper.Map<BuyerApiModel, Buyer>(model, buyer);
            _unitOfWork.Buyers.Update(buyer);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteBuyerAsync(int buyerId, int wcId)
        {
            var buyer = await _unitOfWork.Buyers.FindAsync(buyerId);
            if (buyer != null && buyer.WeightRecorderId == wcId)
            {
                _unitOfWork.Buyers.DeleteById(buyerId);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Thông tin người mua không tồn tại !!!");
            }

        }

        public async Task<BuyerApiModel> GetDetailBuyerAsync(int buyerId, int wcId)
        {
            var buyerDetail = await _unitOfWork.Buyers.FindAsync(buyerId);
            if (buyerDetail.WeightRecorderId != wcId)
            {
                throw new Exception("Thông tin người mua không tồn tại !!!");
            }
            BuyerApiModel buyerApi = _mapper.Map<Buyer, BuyerApiModel>(buyerDetail);
            return buyerApi;

        }

        public List<BuyerApiModel> GetBuyerByNameOrPhone(string input, int wcId)
        {
            return _unitOfWork.Buyers.GetAll(x => x.WeightRecorderId == wcId)
                .Where(x => x.Name == input || x.PhoneNumber == input)
                .Select(x => _mapper.Map<Buyer, BuyerApiModel>(x)).ToList();
        }
    }
}
