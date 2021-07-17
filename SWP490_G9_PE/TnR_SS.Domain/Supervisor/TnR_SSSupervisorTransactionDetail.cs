using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.BuyerModel;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.TransactionDetailModel;
using TnR_SS.Domain.ApiModels.TransactionModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task CreateTransactionDetailAsync(CreateTransactionDetailApiModel apiModel, int wcId)
        {
            var trans = await _unitOfWork.Transactions.FindAsync(apiModel.TransId);
            if (trans == null)
            {
                throw new Exception("Hóa đơn không tồn tại !!");
            }

            var fishType = await _unitOfWork.FishTypes.FindAsync(apiModel.FishTypeId);
            if (fishType == null)
            {
                throw new Exception("Loại cá không đúng !!");
            }

            var buyer = await _unitOfWork.Buyers.FindAsync(apiModel.BuyerId);
            if (buyer == null || buyer.WeightRecorderId != wcId)
            {
                throw new Exception("Người mua không tồn tại !!");
            }

            var transaction = _mapper.Map<CreateTransactionDetailApiModel, TransactionDetail>(apiModel);
            await _unitOfWork.TransactionDetails.CreateAsync(transaction);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<List<GetAllTransactionDetailApiModel>> GetAllTransactionDetailAsync(int wcId, DateTime? date)
        {
            var listTranDetail = _unitOfWork.TransactionDetails.GetAllTransactionByWcIDAndDate(wcId, date);
            List<GetAllTransactionDetailApiModel> list = new List<GetAllTransactionDetailApiModel>();
            foreach (var td in listTranDetail)
            {
                GetAllTransactionDetailApiModel apiModel = _mapper.Map<TransactionDetail, GetAllTransactionDetailApiModel>(td);
                apiModel.FishType = _mapper.Map<FishType, FishTypeApiModel>(await _unitOfWork.FishTypes.FindAsync(td.FishTypeId));
                apiModel.Transaction = _mapper.Map<Transaction, TransactionResModel>(await _unitOfWork.Transactions.FindAsync(td.TransId));
                apiModel.Buyer = _mapper.Map<Buyer, BuyerApiModel>(await _unitOfWork.Buyers.FindAsync(td.BuyerId));
                list.Add(apiModel);
            }

            return list;
        }
    }
}
