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
        private async Task<Transaction> CreateTransactionAsync(int traderId, int? wcId, DateTime date)
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

        public async Task TraderCreateTransactionAsync(TraderCreateTransactionReqModel apiModel, int userId)
        {
            var roleName = await _unitOfWork.UserInfors.GetRolesAsync(userId);
            if (roleName.Contains(RoleName.Trader))
            {
                var phien = DateTime.Now.Hour < 18 ? DateTime.Now.AddDays(-1).Date : DateTime.Now.Date;
                var tran = _unitOfWork.Transactions.GetAllTransactionsByDate(userId, phien).Where(x => x.WeightRecorderId == null).FirstOrDefault();
                if (tran != null)
                {
                    throw new Exception("Đơn bán ngày đã có sẵn, tiếp tục mua thôi <3");
                }

                await CreateTransactionAsync(userId, null, phien);
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
                        // get closest date tran
                        var phien = DateTime.Now.Hour < 18 ? DateTime.Now.AddDays(-1).Date : DateTime.Now.Date;
                        var closestDate = _unitOfWork.Transactions.GetAll(x => x.WeightRecorderId == wcId && x.Date.Date < phien).OrderByDescending(x => x.Date).Select(x => x.Date).FirstOrDefault();
                        var listCloestTran = _unitOfWork.Transactions.GetAllTransactionsByDate(wcId, closestDate);
                        // check close last phiên
                        foreach (var item in listCloestTran)
                        {
                            if (item.isCompleted != TransactionStatus.Completed)
                            {
                                throw new Exception("Hãy chốt tất cả đơn bán ngày gần nhất !!!");
                            }
                        }

                        if (apiModel.ListTraderId == null || apiModel.ListTraderId.Count == 0)
                        {
                            throw new Exception("Hãy chọn ít nhất 1 thương lái !!!");
                        }


                        var listTran = _unitOfWork.Transactions.GetAllTransactionsByDate(wcId, phien);
                        foreach (var item in apiModel.ListTraderId)
                        {
                            var tran = listTran.Where(x => x.TraderId == item).FirstOrDefault();
                            if (tran == null)
                            {
                                await CreateTransactionAsync(item, wcId, phien);
                            }
                        }

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

        // get transaction detail
        public async Task<List<TransactionResModel>> GetAllTransactionAsync(int userId, DateTime date)
        {
            var phien = date.Date;
            if (date.Date == DateTime.Now.Date)
            {
                phien = DateTime.Now.Hour < 18 ? DateTime.Now.AddDays(-1).Date : DateTime.Now.Date;
            }

            var listTran = _unitOfWork.Transactions.GetAllTransactionsByDate(userId, phien);
            List<TransactionResModel> list = new();
            foreach (var item in listTran)
            {
                TransactionResModel tran = new TransactionResModel();
                tran.ID = item.ID;
                tran.Date = phien;
                tran.CommissionUnit = item.CommissionUnit;
                tran.Trader = _mapper.Map<UserInfor, UserInformation>(await _unitOfWork.UserInfors.FindAsync(item.TraderId));
                tran.WeightRecorder = item.WeightRecorderId != null ? _mapper.Map<UserInfor, UserInformation>(await _unitOfWork.UserInfors.FindAsync(item.WeightRecorderId)) : null;
                tran.TransactionDetails = await GetListTransactionDetailModelAsync(item);
                tran.Status = item.isCompleted.ToString();

                list.Add(tran);
            }

            return list.OrderByDescending(x => x.Status).ToList();
        }

        private async Task<List<TransactionDetailInformation>> GetListTransactionDetailModelAsync(Transaction tran)
        {
            List<TransactionDetailInformation> list = new List<TransactionDetailInformation>();
            bool checkLackOfDate = false;
            if (tran.isCompleted == TransactionStatus.Completed)
            {
                var listCloseTD = _unitOfWork.CloseTransactionDetails.GetAll(x => x.TransactionId == tran.ID);
                if (listCloseTD == null || listCloseTD.Count() == 0)
                {
                    checkLackOfDate = true;
                }
                else
                {
                    foreach (var tranDe in listCloseTD)
                    {
                        TransactionDetailInformation apiModel = _mapper.Map<CloseTransactionDetail, TransactionDetailInformation>(tranDe);
                        list.Add(apiModel);
                    }
                }
            }

            if (checkLackOfDate || tran.isCompleted == TransactionStatus.Pending)
            {
                var listTranDe = _unitOfWork.TransactionDetails.GetAll(x => x.TransId == tran.ID).OrderByDescending(x => x.ID);
                foreach (var tranDe in listTranDe)
                {
                    TransactionDetailInformation apiModel = _mapper.Map<TransactionDetail, TransactionDetailInformation>(tranDe);
                    apiModel.FishType = _mapper.Map<FishType, FishTypeApiModel>(await _unitOfWork.FishTypes.FindAsync(tranDe.FishTypeId));
                    apiModel.Buyer = _mapper.Map<Buyer, BuyerApiModel>(await _unitOfWork.Buyers.FindAsync(tranDe.BuyerId));
                    list.Add(apiModel);
                }
            }

            return list;
        }


        //get transaction general
        public async Task<List<GetGeneralTransactionFollowDateResModel>> GetAllTransactionFollowDateAsync(int userId)
        {
            var roleUser = await _unitOfWork.UserInfors.GetRolesAsync(userId);
            var listDate = new List<DateTime>();
            if (roleUser.Contains(RoleName.WeightRecorder))
            {
                listDate = _unitOfWork.Transactions.GetAll(x => x.WeightRecorderId == userId).Select(x => x.Date.Date
                /* {
                     if (x.Date.Hour < 18) return x.Date.Date.AddDays(-1);
                     else return x.Date.Date;
                 }*/).OrderByDescending(x => x.Date).Distinct().ToList();
            }
            else if (roleUser.Contains(RoleName.Trader))
            {
                listDate = _unitOfWork.Transactions.GetAll(x => x.TraderId == userId).Select(x => x.Date.Date
                /*{
                    if (x.Date.Hour < 18) return x.Date.Date.AddDays(-1);
                    else return x.Date.Date;
                }*/).OrderByDescending(x => x.Date).Distinct().ToList();
            }
            else
            {
                throw new Exception("Tài khoản không hợp lệ");
            }

            var listGeTran = new List<GetGeneralTransactionFollowDateResModel>();
            foreach (var date in listDate)
            {
                GetGeneralTransactionFollowDateResModel newGeneral = new GetGeneralTransactionFollowDateResModel();
                newGeneral.Date = date;
                var listTran = _unitOfWork.Transactions.GetAllTransactionsByDate(userId, date);

                // add trader
                if (roleUser.Contains(RoleName.WeightRecorder))
                {
                    newGeneral.ListTrader = await WeightRecordGetAllTraderInTransactions(userId, listTran);
                    newGeneral.ListWeightRecorder.Add(_mapper.Map<UserInfor, UserInformation>(await _unitOfWork.UserInfors.FindAsync(userId)));
                }
                else if (roleUser.Contains(RoleName.Trader))
                {
                    newGeneral.ListTrader.Add(_mapper.Map<UserInfor, UserInformation>(await _unitOfWork.UserInfors.FindAsync(userId)));
                    newGeneral.ListWeightRecorder = await TraderGetAllWeightRecorderInTransactions(userId, listTran);
                }

                newGeneral.TotalWeight = await GetTotalWeightForGeneral(userId, listTran);
                newGeneral.TotalMoney = await GetTotalMoneyForGeneral(userId, listTran);
                newGeneral.TotalDebt = await GetTotalDebtForGeneral(userId, listTran);

                listGeTran.Add(newGeneral);
            }

            return listGeTran;
        }

        public async Task DeleteTransactionAsync(int tranId, int userId)
        {
            var strategy = _unitOfWork.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var dbTransaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        var tran = await _unitOfWork.Transactions.FindAsync(tranId);
                        if (tran == null || (tran.WeightRecorderId != null && tran.WeightRecorderId != userId) || (tran.WeightRecorderId == null && tran.TraderId != userId))
                        {
                            throw new Exception("Đơn mua này không tồn tại hoặc đã bị xóa !!!");
                        }

                        if (tran.isCompleted == TransactionStatus.Completed)
                        {
                            throw new Exception("Đơn bán đã được chốt sổ không thể xóa !!!");
                        }

                        await _unitOfWork.TransactionDetails.DeleteByTransactionIdAsync(tranId);
                        _unitOfWork.Transactions.Delete(tran);
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

        public async Task ChotSoTransactionAsync(ChotSoTransactionReqModal chotSoApi, int userId)
        {
            var strategy = _unitOfWork.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var dbTransaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        foreach (var tranId in chotSoApi.listTranId)
                        {
                            var tran = await _unitOfWork.Transactions.FindAsync(tranId);
                            if (tran == null || (tran.WeightRecorderId != null && tran.WeightRecorderId != userId) || (tran.WeightRecorderId == null && tran.TraderId != userId))
                            {
                                throw new Exception("Có đơn mua không tồn tại hoặc đã bị xóa !!!");
                            }

                            if (tran.isCompleted.Equals(TransactionStatus.Completed))
                            {
                                throw new Exception("Có đơn mua đã đã được chốt !!");
                            }

                            var listTranDe = _unitOfWork.TransactionDetails.GetAll(x => x.TransId == tranId);
                            if (listTranDe.Count() == 0)
                            {
                                throw new Exception("Không có đơn bán nào để chốt sổ !!");
                            }

                            tran.isCompleted = TransactionStatus.Completed;
                            tran.CommissionUnit = chotSoApi.CommissionUnit;
                            _unitOfWork.Transactions.Update(tran);
                            await _unitOfWork.SaveChangeAsync();

                            foreach (var trandDe in listTranDe)
                            {
                                CloseTransactionDetail closeTD = new CloseTransactionDetail();
                                closeTD.SellPrice = trandDe.SellPrice;
                                closeTD.Weight = trandDe.Weight;
                                closeTD.TransactionId = tran.ID;
                                closeTD.IsPaid = trandDe.IsPaid;

                                FishType ft = await _unitOfWork.FishTypes.FindAsync(trandDe.FishTypeId);
                                closeTD.FishTypeId = ft.ID;
                                closeTD.FishName = ft.FishName;
                                closeTD.FishTypeDescription = ft.Description;
                                closeTD.FishTypeMinWeight = ft.MinWeight;
                                closeTD.FishTypeMaxWeight = ft.MaxWeight;
                                closeTD.FishTypePrice = (float)ft.Price;

                                if (trandDe.BuyerId != null)
                                {
                                    var buyer = await _unitOfWork.Buyers.FindAsync(trandDe.BuyerId);
                                    closeTD.BuyerId = buyer.ID;
                                    closeTD.BuyerName = buyer.Name;
                                    closeTD.BuyerAddress = buyer.Address;
                                    closeTD.BuyerPhoneNumber = buyer.PhoneNumber;
                                }

                                await _unitOfWork.CloseTransactionDetails.CreateAsync(closeTD);
                                await _unitOfWork.SaveChangeAsync();
                            }
                        }

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

        private async Task<List<UserInformation>> WeightRecordGetAllTraderInTransactions(int wcId, List<Transaction> listTran)
        {
            var listTraderId = listTran.Select(x => x.TraderId).Distinct();
            List<UserInformation> listTrader = new List<UserInformation>();
            foreach (var item in listTraderId)
            {
                listTrader.Add(_mapper.Map<UserInfor, UserInformation>(await _unitOfWork.UserInfors.FindAsync(item)));
            }

            return listTrader;
        }

        private async Task<List<UserInformation>> TraderGetAllWeightRecorderInTransactions(int traderId, List<Transaction> listTran)
        {
            var listWeightRecorderId = listTran.Where(x => x.WeightRecorderId != null).Select(x => x.WeightRecorderId).Distinct();
            List<UserInformation> listTrader = new List<UserInformation>();
            foreach (var item in listWeightRecorderId)
            {
                listTrader.Add(_mapper.Map<UserInfor, UserInformation>(await _unitOfWork.UserInfors.FindAsync(item)));
            }

            return listTrader;
        }

        private async Task<double> GetTotalWeightForGeneral(int userId, List<Transaction> listTran)
        {
            double totalWeight = 0.0;
            foreach (var tran in listTran)
            {
                totalWeight += await _unitOfWork.Transactions.GetTotalWeightAsync(tran.ID);
            }

            return totalWeight;
        }
        private async Task<double> GetTotalMoneyForGeneral(int userId, List<Transaction> listTran)
        {
            double totalWeight = 0.0;
            foreach (var tran in listTran)
            {
                totalWeight += await _unitOfWork.Transactions.GetTotalMoneyAsync(tran.ID);
            }

            return totalWeight;
        }
        private async Task<double> GetTotalDebtForGeneral(int userId, List<Transaction> listTran)
        {
            double totalWeight = 0.0;
            foreach (var tran in listTran)
            {
                totalWeight += await _unitOfWork.Transactions.GetTotalDebtAsync(tran.ID);
            }

            return totalWeight;
        }
    }
}
