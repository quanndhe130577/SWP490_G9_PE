using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(TnR_SSContext context) : base(context) { }

        public async Task<double> GetTotalWeightAsync(int transId)
        {
            var trans = await _context.Transactions.FindAsync(transId);
            if (trans != null)
            {
                bool checkLackOfData = false;
                if (trans.isCompleted == TransactionStatus.Completed)
                {
                    var listCT = _context.CloseTransactionDetails.Where(x => x.TransactionId == trans.ID).ToList();
                    if (listCT.Count() == 0)
                    {
                        checkLackOfData = true;
                    }
                    else
                    {
                        var totalWeight = listCT.Sum(x => x.Weight);
                        return totalWeight;
                    }
                }

                if (checkLackOfData || trans.isCompleted == TransactionStatus.Pending)
                {
                    var totalWeight = _context.TransactionDetails.Where(x => x.TransId == trans.ID).Sum(x => x.Weight);
                    return totalWeight;
                }
            }
            else
            {
                throw new Exception("Không tìm thấy transaction");
            }

            return 0;
        }


        public async Task<double> GetTotalMoneyAsync(int transId)
        {
            var trans = await _context.Transactions.FindAsync(transId);
            if (trans != null)
            {
                bool checkLackOfData = false;
                if (trans.isCompleted == TransactionStatus.Completed)
                {
                    var listCT = _context.CloseTransactionDetails.Where(x => x.TransactionId == trans.ID).ToList();
                    if (listCT.Count() == 0)
                    {
                        checkLackOfData = true;
                    }
                    else
                    {
                        var totalWeight = listCT.Sum(x => x.Weight * x.SellPrice);
                        return totalWeight;
                    }
                }

                if (checkLackOfData || trans.isCompleted == TransactionStatus.Pending)
                {
                    return _context.TransactionDetails.Where(x => x.TransId == trans.ID).Sum(x => x.Weight * x.SellPrice);
                }
            }
            else
            {
                throw new Exception("Không tìm thấy transaction");
            }

            return 0;
        }

        public async Task<double> GetTotalDebtAsync(int transId)
        {
            var trans = await _context.Transactions.FindAsync(transId);
            if (trans != null)
            {
                bool checkLackOfData = false;
                if (trans.isCompleted == TransactionStatus.Completed)
                {
                    var listCT = _context.CloseTransactionDetails.Where(x => x.TransactionId == trans.ID).ToList();
                    if (listCT.Count() == 0)
                    {
                        checkLackOfData = true;
                    }
                    else
                    {
                        var totalWeight = listCT.Where(x => !x.IsPaid).Sum(x => x.Weight * x.SellPrice);
                        return totalWeight;
                    }
                }

                if (checkLackOfData || trans.isCompleted == TransactionStatus.Pending)
                {
                    return _context.TransactionDetails.Where(x => x.TransId == trans.ID && !x.IsPaid).Sum(x => x.Weight * x.SellPrice);
                }
            }
            else
            {
                throw new Exception("Không tìm thấy transaction");
            }

            return 0;
        }

        public List<Transaction> GetAllTransactionsByDate(int userId, DateTime? date)
        {
            var roleUser = _context.UserRoles.Where(x => x.UserId == userId).Join(
                    _context.RoleUsers,
                    ur => ur.RoleId,
                    ru => ru.Id,
                    (ur, ru) => ru.NormalizedName);

            List<Transaction> listTran = new();

            if (roleUser.Contains(RoleName.WeightRecorder))
            {
                listTran = _context.Transactions.Where(x => x.WeightRecorderId == userId).OrderByDescending(x => x.Date).ToList();
            }
            else if (roleUser.Contains(RoleName.Trader))
            {
                listTran = _context.Transactions.Where(x => x.TraderId == userId).OrderByDescending(x => x.Date).ToList();
            }
            else
            {
                throw new Exception("Tài khoản không hợp lệ");
            }

            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;

            if (date != null)
            {
                // nếu là ngày hiện tại và < 18 giờ thì là bán tiếp => lấy dữ liệu từ 18h hôm trc -> 18h hôm nay
                if (date.Value.Date == DateTime.Now.Date && DateTime.Now.Hour < 18)
                {
                    var temp = date.Value.AddDays(-1);
                    startDate = new DateTime(temp.Year, temp.Month, temp.Day, 18, 0, 0); // 18 h ngày hôm trước
                    endDate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 18, 0, 0); // 18 h ngày hôm nay
                }
                else // lấy dữ liệu từ 18h hôm đó -> 18h hôm sau
                {
                    var temp = date.Value.AddDays(1);
                    startDate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 18, 0, 0); // 18 h ngày hôm đó
                    endDate = new DateTime(temp.Year, temp.Month, temp.Day, 18, 0, 0); // 18 h ngày hôm sau

                }
            }

            /*DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;

            if (date != null)
            {
                // nếu là ngày hiện tại và < 18 giờ thì là bán tiếp => lấy dữ liệu từ 18h hôm trc -> 18h hôm nay
                if (date.Value.Date == DateTime.Now.Date && DateTime.Now.Hour < 18)
                {
                    startDate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day - 1, 18, 0, 0); // 18 h ngày hôm trước
                    endDate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 18, 0, 0); // 18 h ngày hôm nay
                }
                else // lấy dữ liệu từ 18h hôm đó -> 18h hôm sau
                {
                    startDate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day, 18, 0, 0); // 18 h ngày hôm đó
                    endDate = new DateTime(date.Value.Year, date.Value.Month, date.Value.Day + 1, 18, 0, 0); // 18 h ngày hôm sau

                }
            }*/

            listTran = listTran.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();

            return listTran;
        }
    }
}
