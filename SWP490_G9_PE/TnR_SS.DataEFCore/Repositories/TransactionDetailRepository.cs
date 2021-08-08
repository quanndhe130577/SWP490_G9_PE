using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class TransactionDetailRepository : RepositoryBase<TransactionDetail>, ITransactionDetailRepository
    {
        public TransactionDetailRepository(TnR_SSContext context) : base(context) { }
        public List<TransactionDetail> GetAllByWcIDAndDate(int wcId, DateTime? date)
        {
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

            var rs = _context.Transactions.Join(
                        _context.TransactionDetails,
                        t => t.ID,
                        td => td.TransId,
                        (t, td) => new
                        {
                            tran = t,
                            tranDe = td
                        }
                    ).Where(x => x.tran.WeightRecorderId == wcId);
            if (date != null)
            {
                rs = rs.Where(x => x.tran.Date >= startDate && x.tran.Date <= endDate);
            }

            return rs.Select(x => x.tranDe).ToList();
        }

        public List<TransactionDetail> GetAllByTraderIdAndDate(int traderId, DateTime? date)
        {
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

            var rs = _context.Transactions.Join(
                        _context.TransactionDetails,
                        t => t.ID,
                        td => td.TransId,
                        (t, td) => new
                        {
                            tran = t,
                            tranDe = td
                        }
                    ).Where(x => x.tran.TraderId == traderId);
            if (date != null)
            {
                rs = rs.Where(x => x.tran.Date >= startDate && x.tran.Date <= endDate);
            }

            return rs.Select(x => x.tranDe).ToList();
        }

        public async Task DeleteByTransactionIdAsync(int tranId)
        {
            var list = _context.TransactionDetails.Where(x => x.TransId == tranId);
            _context.TransactionDetails.RemoveRange(list);
            await _context.SaveChangesAsync();
        }

        public List<TransactionDetail> GetAllByListTransaction(List<int> listTranId)
        {
            return _context.TransactionDetails.Where(x => listTranId.Contains(x.TransId)).ToList();

            /*var rs = _context.TransactionDetails.AsEnumerable().Join(
                        trans,
                        td => td.TransId,
                        t => t.ID,
                        (td, t) => td
                    ).ToList();

            return rs;*/
        }
    }
}
