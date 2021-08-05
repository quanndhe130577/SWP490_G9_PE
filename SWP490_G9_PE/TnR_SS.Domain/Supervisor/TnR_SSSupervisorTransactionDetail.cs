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
            if (!apiModel.IsPaid && apiModel.BuyerId is null)
            {
                throw new Exception("Không có thông tin người mua thì ko thể ghi nợ !!!");
            }

            var trans = await _unitOfWork.Transactions.FindAsync(apiModel.TransId);
            if (trans == null || trans.WeightRecorderId != wcId)
            {
                throw new Exception("Hãy tạo hóa đơn trước !!");
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

        private async Task TraderCreateTransactionDetailAsync(CreateTransactionDetailReqModel apiModel, int traderId)
        {
            if (!apiModel.IsPaid && apiModel.BuyerId is null)
            {
                throw new Exception("Không có thông tin người mua thì ko thể ghi nợ !!!");
            }

            var strategy = _unitOfWork.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var dbTransaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        /*// create transaction if not existed
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

                        await dbTransaction.CommitAsync();*/

                        // create transaction if not existed
                        var transaction = _unitOfWork.Transactions.GetAll(x => x.TraderId == traderId && x.Date.Date == apiModel.Date.Date && x.WeightRecorderId == null).FirstOrDefault();
                        if (transaction == null)
                        {
                            transaction = new Transaction()
                            {
                                TraderId = traderId,
                                Date = apiModel.Date,
                                CommissionUnit = 0,
                                WeightRecorderId = null
                            };

                            await _unitOfWork.Transactions.CreateAsync(transaction);
                            await _unitOfWork.SaveChangeAsync();
                        }

                        var transactionDetail = _mapper.Map<CreateTransactionDetailReqModel, TransactionDetail>(apiModel);
                        transactionDetail.TransId = transaction.ID;
                        // create transaction detail                   
                        await _unitOfWork.TransactionDetails.CreateAsync(transactionDetail);
                        await _unitOfWork.SaveChangeAsync();

                        await dbTransaction.CommitAsync();
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
            if (!apiModel.IsPaid && apiModel.BuyerId is null)
            {
                throw new Exception("Không có thông tin người mua thì ko thể ghi nợ !!!");
            }

            var roleUser = await _unitOfWork.UserInfors.GetRolesAsync(userId);
            if (roleUser.Contains(RoleName.WeightRecorder))
            {
                await WeightRecorderCreateTransactionDetailAsync(apiModel, userId);
            }
            else if (roleUser.Contains(RoleName.Trader))
            {
                await TraderCreateTransactionDetailAsync(apiModel, userId);
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

        public async Task UpdateTransactionDetailAsync(UpdateTransactionDetailReqModel apiModel, int userId)
        {
            var tranDe = await _unitOfWork.TransactionDetails.FindAsync(apiModel.ID);
            if (tranDe == null)
            {
                throw new Exception("Không tìm thấy đơn mua !!");
            }

            var buyer = await _unitOfWork.Buyers.FindAsync(apiModel.BuyerId);
            // nếu người mua không tồn tại hoặc người mua không phải là của người bán
            if ((apiModel.BuyerId != null && buyer == null) || (buyer != null && buyer.SellerId != userId))
            {
                throw new Exception("Không tìm thấy người mua !!");
            }

            var tran = await _unitOfWork.Transactions.FindAsync(tranDe.TransId);
            // nếu có weight recorder => userId phải là  weight recorder
            if (tran.WeightRecorderId != null && tran.WeightRecorderId != userId)
            {
                throw new Exception("Không tìm thấy đơn mua !!");
            }// nếu ko có weight recorder => userId phải là trader
            else if (tran.WeightRecorderId == null && tran.TraderId != userId)
            {
                throw new Exception("Không tìm thấy đơn mua !!");
            }

            var fishType = await _unitOfWork.FishTypes.FindAsync(apiModel.FishTypeId);
            // nếu loại cá không tồn tại hoặc loại cá không phải là của trader của transaction hoặc loại cá không phải cùng ngày với transaction
            if (fishType == null || fishType.TraderID != tran.TraderId || fishType.Date.Date != tran.Date.Date)
            {
                throw new Exception("Loại cá không đúng !!");
            }

            tranDe = _mapper.Map<UpdateTransactionDetailReqModel, TransactionDetail>(apiModel, tranDe);
            _unitOfWork.TransactionDetails.Update(tranDe);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteTransactionDetailAsync(int tranDtId, int userId)
        {
            var strategy = _unitOfWork.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var dbTransaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {

                        var tranDt = await _unitOfWork.TransactionDetails.FindAsync(tranDtId);
                        if (tranDt == null)
                        {
                            throw new Exception("Mã cân này không tồn tại hoặc đã bị xóa !!!");
                        }

                        var tran = await _unitOfWork.Transactions.FindAsync(tranDt.TransId);
                        if (tran == null || (tran.WeightRecorderId != null && tran.WeightRecorderId != userId) || (tran.WeightRecorderId == null && tran.TraderId != userId))
                        {
                            throw new Exception("Mã cân này không tồn tại hoặc đã bị xóa !!!");
                        }

                        _unitOfWork.TransactionDetails.Delete(tranDt);
                        await _unitOfWork.SaveChangeAsync();

                        await dbTransaction.CommitAsync();
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
    }
}
