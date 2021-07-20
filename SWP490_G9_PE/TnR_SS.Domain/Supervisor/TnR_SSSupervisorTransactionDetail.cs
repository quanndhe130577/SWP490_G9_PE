using Microsoft.EntityFrameworkCore;
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
        private async Task WeightRecorderCreateTransactionDetailAsync(CreateTransactionDetailReqModel apiModel, int wcId)
        {
            var trans = await _unitOfWork.Transactions.FindAsync(apiModel.TransId);
            if (trans == null || trans.WeightRecorderId != wcId)
            {
                throw new Exception("Hóa đơn không tồn tại !!");
            }

            var fishType = await _unitOfWork.FishTypes.FindAsync(apiModel.FishTypeId);
            if (fishType == null)
            {
                throw new Exception("Loại cá không đúng !!");
            }

            if (apiModel.BuyerId != null)
            {
                var buyer = await _unitOfWork.Buyers.FindAsync(apiModel.BuyerId.Value);
                if (buyer == null || buyer.SellerId != wcId)
                {
                    throw new Exception("Người mua không tồn tại !!");
                }
            }

            var transactionDetail = _mapper.Map<CreateTransactionDetailReqModel, TransactionDetail>(apiModel);
            await _unitOfWork.TransactionDetails.CreateAsync(transactionDetail);
            await _unitOfWork.SaveChangeAsync();
        }

        private async Task TraderRecorderCreateTransactionDetailAsync(CreateTransactionDetailReqModel apiModel, int traderId)
        {
            var strategy = _unitOfWork.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var dbTransaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        // create transaction if not existed
                        var transaction = _unitOfWork.Transactions.GetAll(x => x.TraderId == traderId && x.Date.Date == apiModel.Date.Date && x.WeightRecorderId == null);
                        var transactionDetail = _mapper.Map<CreateTransactionDetailReqModel, TransactionDetail>(apiModel);
                        if (transaction == null)
                        {
                            var newTransaction = new Transaction()
                            {
                                TraderId = traderId,
                                Date = apiModel.Date,
                                CommissionUnit = 0,
                                WeightRecorderId = null
                            };

                            await _unitOfWork.Transactions.CreateAsync(newTransaction);
                            await _unitOfWork.SaveChangeAsync();

                            transactionDetail.TransId = newTransaction.ID;
                        }

                        // create transaction detail                   
                        await _unitOfWork.TransactionDetails.CreateAsync(transactionDetail);
                        await _unitOfWork.SaveChangeAsync();
                    }
                    catch
                    {
                        await dbTransaction.RollbackAsync();
                        throw;
                        //throw new Exception("Đã có lỗi xay ra, hãy thử lại sau");
                    }
                }
            });
        }

        public async Task CreateTransactionDetailAsync(CreateTransactionDetailReqModel apiModel, int userId)
        {
            var roleUser = await _unitOfWork.UserInfors.GetRolesAsync(userId);
            if (roleUser.Contains(RoleName.WeightRecorder))
            {
                await WeightRecorderCreateTransactionDetailAsync(apiModel, userId);
            }
            else if (roleUser.Contains(RoleName.Trader))
            {
                await TraderRecorderCreateTransactionDetailAsync(apiModel, userId);
            }
            else
            {
                throw new Exception("Người mua không tồn tại !!");
            }
        }

        public async Task<List<GetAllTransactionDetailResModel>> GetAllTransactionDetailAsync(int userId, DateTime? date)
        {
            var roleUser = await _unitOfWork.UserInfors.GetRolesAsync(userId);
            List<TransactionDetail> listTranDe = new List<TransactionDetail>();
            if (roleUser.Contains(RoleName.WeightRecorder))
            {
                listTranDe = _unitOfWork.TransactionDetails.GetAllTransactionByWcIDAndDate(userId, date);
            }
            else if (roleUser.Contains(RoleName.Trader))
            {
                listTranDe = _unitOfWork.TransactionDetails.GetAllTransactionByTraderIdAndDate(userId, date);
            }
            else
            {
                throw new Exception("Người mua không tồn tại !!");
            }

            List<GetAllTransactionDetailResModel> list = new List<GetAllTransactionDetailResModel>();
            foreach (var td in listTranDe)
            {
                GetAllTransactionDetailResModel apiModel = _mapper.Map<TransactionDetail, GetAllTransactionDetailResModel>(td);
                apiModel.FishType = _mapper.Map<FishType, FishTypeApiModel>(await _unitOfWork.FishTypes.FindAsync(td.FishTypeId));
                apiModel.Transaction = _mapper.Map<Transaction, TransactionResModel>(await _unitOfWork.Transactions.FindAsync(td.TransId));
                apiModel.Buyer = _mapper.Map<Buyer, BuyerApiModel>(await _unitOfWork.Buyers.FindAsync(td.BuyerId));
                list.Add(apiModel);
            }

            return list;
        }
    }
}
