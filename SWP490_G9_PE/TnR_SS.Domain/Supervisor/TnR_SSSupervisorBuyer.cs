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
        public List<BuyerApiModel> GetAllBuyer()
        {
            var listBuyer = _unitOfWork.Buyers.GetAllBuyer();
            List<BuyerApiModel> buyers = new();
            foreach(var item in listBuyer)
            {
                buyers.Add(_mapper.Map<Buyer, BuyerApiModel>(item));
            }
            return buyers;
        }

        public async Task CreateBuyerAsync(BuyerApiModel model)
        {
            var mapper = _mapper.Map<BuyerApiModel, Buyer>(model);
            await _unitOfWork.Buyers.CreateAsync(mapper);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task UpdateBuyerAsync(BuyerApiModel model)
        {
            var buyerUpdate = await _unitOfWork.Buyers.FindAsync(model.ID);
            buyerUpdate = _mapper.Map<BuyerApiModel, Buyer>(model, buyerUpdate);
            _unitOfWork.Buyers.Update(buyerUpdate);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteBuyerAsync(int buyerId)
        {
            if (buyerId <= 0 )
            {
                throw new Exception("BuyerId invalid");
            }
            else
            {
                _unitOfWork.Buyers.DeleteById(buyerId);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task<BuyerApiModel> GetDetailBuyerAsync(int buyerId)
        {
            if(buyerId <= 0 )
            {
                throw new Exception("BuyerId invalid");
            }
            else
            {
                var buyerDetail = await _unitOfWork.Buyers.FindAsync(buyerId);
                BuyerApiModel buyerApi = new();
                buyerApi = _mapper.Map<Buyer, BuyerApiModel>(buyerDetail);
                return buyerApi;
            }
            
        }
    }
}
