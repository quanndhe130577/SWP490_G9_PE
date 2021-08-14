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
using TnR_SS.Domain.ApiModels.UserInforModel;
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

            if (trans.isCompleted == TransactionStatus.Completed)
            {
                throw new Exception("Đã chốt sổ với thương lái này, không thể tạo thêm !!");
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
                        var phien = apiModel.Date;
                        if (apiModel.Date == DateTime.Now.Date)
                        {
                            phien = DateTime.Now.Hour < 18 ? DateTime.Now.AddDays(-1).Date : DateTime.Now.Date;
                        }
                        // create transaction if not existed
                        //var transaction = _unitOfWork.Transactions.GetAll(x => x.TraderId == traderId && x.Date.Date == apiModel.Date.Date && x.WeightRecorderId == null).FirstOrDefault();
                        var transaction = await _unitOfWork.Transactions.FindAsync(apiModel.TransId);
                        if (transaction == null || transaction.TraderId != traderId)
                        {
                            transaction = _unitOfWork.Transactions.GetAllTransactionsByDate(traderId, phien.Date).Where(x => x.WeightRecorderId == null).FirstOrDefault();
                        }

                        if (transaction == null)
                        {
                            transaction = new Transaction()
                            {
                                TraderId = traderId,
                                Date = phien.Date,
                                CommissionUnit = 0,
                                WeightRecorderId = null
                            };

                            await _unitOfWork.Transactions.CreateAsync(transaction);
                            await _unitOfWork.SaveChangeAsync();
                        }

                        if (transaction.isCompleted == TransactionStatus.Completed)
                        {
                            throw new Exception("Thông tin đơn bán đã được chốt, không thể tạo thêm !!");
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
                listTranDe = _unitOfWork.TransactionDetails.GetAllByWcIDAndDate(userId, date);
            }
            else if (roleUser.Contains(RoleName.Trader))
            {
                listTranDe = _unitOfWork.TransactionDetails.GetAllByTraderIdAndDate(userId, date);
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
            else if (tran.isCompleted == TransactionStatus.Completed)
            {
                throw new Exception("Thông tin đơn bán đã được chốt, không thể thay đổi !!");
            }

            var fishType = await _unitOfWork.FishTypes.FindAsync(apiModel.FishTypeId);
            // nếu loại cá không tồn tại hoặc loại cá không phải là của trader của transaction hoặc loại cá không phải cùng ngày với transaction
            if (fishType == null || fishType.TraderID != tran.TraderId /*|| fishType.Date.Date != tran.Date.Date*/)
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
                        else if (tran.isCompleted == TransactionStatus.Completed)
                        {
                            throw new Exception("Thông tin đơn bán đã được chốt, không thể thay đổi !!");
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

        public async Task<List<PaymentForBuyer>> GetPaymentForBuyersAsync(int userId, DateTime date)
        {
            var phien = date.Date;
            if (date.Date == DateTime.Now.Date)
            {
                phien = DateTime.Now.Hour < 18 ? DateTime.Now.AddDays(-1).Date : DateTime.Now.Date;
            }

            var listTran = _unitOfWork.Transactions.GetAllTransactionsByDate(userId, phien);
            var roleUser = await _unitOfWork.UserInfors.GetRolesAsync(userId);
            if (roleUser.Contains(RoleName.Trader))
            {
                listTran = listTran.Where(x => x.WeightRecorderId == null).ToList();
            }

            var listTranDe = _unitOfWork.TransactionDetails.GetAllByListTransaction(listTran.Select(x => x.ID).ToList());
            var listBuyerId = listTranDe.Where(x => x.BuyerId != null).Select(x => x.BuyerId).Distinct();
            List<PaymentForBuyer> list = new List<PaymentForBuyer>();
            foreach (var item in listBuyerId)
            {
                var listTranBuyer = listTranDe.Where(x => x.BuyerId == item);
                PaymentForBuyer payments = new PaymentForBuyer();
                payments.Date = phien;
                payments.Buyer = _mapper.Map<Buyer, BuyerApiModel>(await _unitOfWork.Buyers.FindAsync(item));
                payments.TotalWeight = listTranBuyer.Sum(x => x.Weight);
                payments.MoneyPaid = listTranBuyer.Where(x => x.IsPaid).Sum(x => x.SellPrice * x.Weight);
                payments.MoneyNotPaid = listTranBuyer.Where(x => !x.IsPaid).Sum(x => x.SellPrice * x.Weight);
                payments.TotalMoney = payments.MoneyPaid + payments.MoneyNotPaid;
                foreach (var tran in listTranBuyer)
                {
                    TransactionDetailPayment tdp = _mapper.Map<TransactionDetail, TransactionDetailPayment>(tran);
                    tdp.FishType = _mapper.Map<FishType, FishTypeApiModel>(await _unitOfWork.FishTypes.FindAsync(tran.FishTypeId));
                    tdp.Buyer = _mapper.Map<Buyer, BuyerApiModel>(await _unitOfWork.Buyers.FindAsync(tran.BuyerId));
                    tdp.Trader = _mapper.Map<UserInfor, UserInformation>(await _unitOfWork.UserInfors.FindAsync(listTran.Where(x => x.ID == tran.TransId).Select(x => x.TraderId).FirstOrDefault()));

                    payments.TransactionDetails.Add(tdp);
                }

                list.Add(payments);
            }

            return list;
        }

        public async Task PaymentForBuyersAsync(FinishPaymentBuyerReqModel apiModel, int userId)
        {
            var phien = apiModel.Date;
            if (apiModel.Date == DateTime.Now.Date)
            {
                phien = DateTime.Now.Hour < 18 ? DateTime.Now.AddDays(-1).Date : DateTime.Now.Date;
            }

            var buyer = _unitOfWork.Buyers.GetAll(x => x.ID == apiModel.BuyerId && x.SellerId == userId);
            if (buyer == null || buyer.Count() == 0)
            {
                throw new Exception("Không có thông tin người mua này !!!");
            }

            var listTran = _unitOfWork.Transactions.GetAllTransactionsByDate(userId, phien.Date);
            var listTranDe = _unitOfWork.TransactionDetails.GetAllByListTransaction(listTran.Select(x => x.ID).ToList()).Where(x => x.BuyerId == apiModel.BuyerId);
            if (listTranDe == null || listTranDe.Count() == 0)
            {
                throw new Exception("Người này chưa mua gì cả !!!");
            }

            foreach (var item in listTranDe)
            {
                item.IsPaid = true;
                _unitOfWork.TransactionDetails.Update(item);
            }

            await _unitOfWork.SaveChangeAsync();
        }
    }
}