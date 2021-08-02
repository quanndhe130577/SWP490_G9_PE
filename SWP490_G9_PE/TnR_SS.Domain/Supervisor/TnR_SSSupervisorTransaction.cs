using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.BuyerModel;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.TransactionModel;
using TnR_SS.Domain.ApiModels.UserInforModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        private async Task<Transaction> CreateTransactionAsync(int traderId, int wcId, DateTime date)
        {
            // check role trader in transaction
            var listRole = await _unitOfWork.UserInfors.GetRolesAsync(traderId);
            if (listRole.Contains(RoleName.Trader))
            {
                Transaction tran = new Transaction()
                {
                    TraderId = traderId,
                    WeightRecorderId = wcId,
                    Date = date
                };

                await _unitOfWork.Transactions.CreateAsync(tran);

                await _unitOfWork.SaveChangeAsync();

                return tran;
            }
            else
            {
                throw new Exception("Thông tin thương lái chưa chính xác !!!");
            }
        }

        public async Task CreateListTransactionAsync(CreateListTransactionReqModel apiModel, int wcId)
        {
            var strategy = _unitOfWork.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var dbTransaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in apiModel.ListTraderId)
                        {
                            // nếu date là buổi sáng ngày hôm sau thì chuyển thành buổi chiều ngày hôm trước
                            if (apiModel.Date.Hour < 12/* && apiModel.Date.Hour + apiModel.Date.Minute + apiModel.Date.Second > 0*/)
                            {
                                apiModel.Date = apiModel.Date.AddHours(-12);
                            }

                            var tran = _unitOfWork.Transactions.GetAll(x => x.TraderId == item && x.WeightRecorderId == wcId && x.Date.Date == apiModel.Date.Date).FirstOrDefault();
                            if (tran == null)
                            {
                                await CreateTransactionAsync(item, wcId, apiModel.Date);
                                /*// check role trader in transaction
                                var listRole = await _unitOfWork.UserInfors.GetRolesAsync(item);
                                if (listRole.Contains(RoleName.Trader))
                                {
                                    await _unitOfWork.Transactions.CreateAsync(new Transaction()
                                    {
                                        TraderId = item,
                                        WeightRecorderId = wcId,
                                        Date = apiModel.Date
                                    });
                                }
                                else
                                {
                                    throw new Exception("Thông tin thương lái chưa chính xác !!!");
                                }*/
                                await dbTransaction.CommitAsync();
                            }


                        }

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

        public async Task<List<TransactionResModel>> GetAllTransactionAsync(int userId, DateTime? date)
        {
            var roleUser = await _unitOfWork.UserInfors.GetRolesAsync(userId);
            var listTran = new List<Transaction>();
            if (roleUser.Contains(RoleName.WeightRecorder))
            {
                listTran = _unitOfWork.Transactions.GetAll(x => x.WeightRecorderId == userId).OrderByDescending(x => x.Date).ToList();
            }
            else if (roleUser.Contains(RoleName.Trader))
            {
                listTran = _unitOfWork.Transactions.GetAll(x => x.TraderId == userId).OrderByDescending(x => x.Date).ToList();
            }
            else
            {
                throw new Exception("Tài khoản không hợp lệ");
            }

            if (date != null)
            {
                listTran = listTran.Where(x => x.Date.Date == date.Value.Date).ToList();
            }

            List<TransactionResModel> list = new();
            foreach (var item in listTran)
            {
                TransactionResModel tran = new TransactionResModel();
                tran.ID = item.ID;
                tran.Date = item.Date;
                tran.Trader = _mapper.Map<UserInfor, TraderInformation>(await _unitOfWork.UserInfors.FindAsync(item.TraderId));
                tran.TransactionDetails = await GetListTransactionDetailModelAsync(item.ID);

                list.Add(tran);
            }

            return list;
        }

        private async Task<List<TransactionDetailInformation>> GetListTransactionDetailModelAsync(int tranId)
        {
            List<TransactionDetailInformation> list = new List<TransactionDetailInformation>();

            var listTranDe = _unitOfWork.TransactionDetails.GetAll(x => x.TransId == tranId).OrderByDescending(x => x.ID);
            foreach (var tran in listTranDe)
            {
                TransactionDetailInformation apiModel = _mapper.Map<TransactionDetail, TransactionDetailInformation>(tran);
                apiModel.FishType = _mapper.Map<FishType, FishTypeApiModel>(await _unitOfWork.FishTypes.FindAsync(tran.FishTypeId));
                apiModel.Buyer = _mapper.Map<Buyer, BuyerApiModel>(await _unitOfWork.Buyers.FindAsync(tran.BuyerId));
                list.Add(apiModel);
            }

            return list;
        }
    }
}
