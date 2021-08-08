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
                var tran = _unitOfWork.Transactions.GetAllTransactionsByDate(userId, apiModel.Date).Where(x => x.WeightRecorderId == null).FirstOrDefault();
                if (tran != null)
                {
                    throw new Exception("Đơn bán ngày đã có sẵn, tiếp tục mua thôi <3");
                }

                await CreateTransactionAsync(userId, null, apiModel.Date);
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
                        if (apiModel.ListTraderId == null || apiModel.ListTraderId.Count == 0)
                        {
                            throw new Exception("Hãy chọn ít nhất 1 thương lái !!!");
                        }

                        var listTran = _unitOfWork.Transactions.GetAllTransactionsByDate(wcId, apiModel.Date);
                        foreach (var item in apiModel.ListTraderId)
                        {
                            // nếu date là buổi sáng ngày hôm sau thì chuyển thành buổi chiều ngày hôm trước
                            /*if (apiModel.Date.Hour < 12*//* && apiModel.Date.Hour + apiModel.Date.Minute + apiModel.Date.Second > 0*//*)
                            {
                                apiModel.Date = apiModel.Date.AddHours(-12);
                            }*/

                            //var tran = _unitOfWork.Transactions.GetAll(x => x.TraderId == item && x.WeightRecorderId == wcId && x.Date.Date == apiModel.Date.Date).FirstOrDefault();
                            var tran = listTran.Where(x => x.TraderId == item).FirstOrDefault();

                            if (tran == null)
                            {
                                await CreateTransactionAsync(item, wcId, apiModel.Date);
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
        public async Task<List<TransactionResModel>> GetAllTransactionAsync(int userId, DateTime? date)
        {

            /*var roleUser = await _unitOfWork.UserInfors.GetRolesAsync(userId);
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
                // nếu là ngày hiện tại và < 18 giờ thì là bán tiếp => lấy dữ liệu từ 18h hôm trc -> 18h hôm nay
                if (date.Value.Date == DateTime.Now.Date && DateTime.Now.Hour < 18)
                {
                    var startDate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day - 1, 18, 0, 0); // 18 h ngày hôm trước
                    var endDate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 18, 0, 0); // 18 h ngày hôm nay
                    listTran = listTran.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();
                }
                else // lấy dữ liệu từ 18h hôm đó -> 18h hôm sau
                {
                    var startDate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 18, 0, 0); // 18 h ngày hôm đó
                    var endDate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day + 1, 18, 0, 0); // 18 h ngày hôm sau
                    listTran = listTran.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();
                }
            }*/
            var listTran = _unitOfWork.Transactions.GetAllTransactionsByDate(userId, date);

            List<TransactionResModel> list = new();
            foreach (var item in listTran)
            {
                TransactionResModel tran = new TransactionResModel();
                tran.ID = item.ID;
                tran.Date = date.Value;
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
                listDate = _unitOfWork.Transactions.GetAll(x => x.WeightRecorderId == userId).Select(x => /*x.Date.Date*/
                {
                    if (x.Date.Hour < 18) return x.Date.Date.AddDays(-1);
                    else return x.Date.Date;
                }).OrderByDescending(x => x.Date).Distinct().ToList();
            }
            else if (roleUser.Contains(RoleName.Trader))
            {
                listDate = _unitOfWork.Transactions.GetAll(x => x.TraderId == userId).Select(x => /*x.Date.Date*/
                {
                    if (x.Date.Hour < 18) return x.Date.Date.AddDays(-1);
                    else return x.Date.Date;
                }).OrderByDescending(x => x.Date).Distinct().ToList();
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
                    newGeneral.ListTrader = await WeightRecordGetAllTraderInDate(userId, listTran);
                    newGeneral.ListWeightRecorder.Add(_mapper.Map<UserInfor, UserInformation>(await _unitOfWork.UserInfors.FindAsync(userId)));
                }
                else if (roleUser.Contains(RoleName.Trader))
                {
                    newGeneral.ListTrader.Add(_mapper.Map<UserInfor, UserInformation>(await _unitOfWork.UserInfors.FindAsync(userId)));
                    newGeneral.ListWeightRecorder = await TraderGetAllWeightRecorderInDate(userId, listTran);
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

                            tran.isCompleted = TransactionStatus.Completed;
                            tran.CommissionUnit = chotSoApi.CommissionUnit;
                            _unitOfWork.Transactions.Update(tran);
                            await _unitOfWork.SaveChangeAsync();

                            var listTranDe = _unitOfWork.TransactionDetails.GetAll(x => x.TransId == tranId);
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

        private async Task<List<UserInformation>> WeightRecordGetAllTraderInDate(int wcId, List<Transaction> listTran)
        {
            var listTraderId = listTran.Select(x => x.TraderId).Distinct();
            List<UserInformation> listTrader = new List<UserInformation>();
            foreach (var item in listTraderId)
            {
                listTrader.Add(_mapper.Map<UserInfor, UserInformation>(await _unitOfWork.UserInfors.FindAsync(item)));
            }

            return listTrader;
        }

        private async Task<List<UserInformation>> TraderGetAllWeightRecorderInDate(int traderId, List<Transaction> listTran)
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
