using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.TransactionDetailModel;
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

            var buyer = await _unitOfWork.Transactions.FindAsync(apiModel.BuyerId);
            if (buyer == null || buyer.WeightRecorderId != wcId)
            {
                throw new Exception("Người mua không tồn tại !!");
            }

            var transaction = _mapper.Map<CreateTransactionDetailApiModel, TransactionDetail>(apiModel);
            await _unitOfWork.TransactionDetails.CreateAsync(transaction);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
