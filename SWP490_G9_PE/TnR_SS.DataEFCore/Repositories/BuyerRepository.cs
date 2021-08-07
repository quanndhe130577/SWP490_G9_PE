using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class BuyerRepository : RepositoryBase<Buyer>, IBuyerRepository
    {
        public BuyerRepository(TnR_SSContext context) : base(context) { }

        public List<Buyer> GetAllBuyer()
        {
            var rs = _context.Buyers.AsEnumerable().OrderByDescending(x => x.Name).ToList();
            return rs;
        }

        /*public List<Buyer> GetBuysOfTransactions(DateTime date)
        {
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;

            // nếu là ngày hiện tại và < 18 giờ thì là bán tiếp => lấy dữ liệu từ 18h hôm trc -> 18h hôm nay
            if (date.Date == DateTime.Now.Date && DateTime.Now.Hour < 18)
            {
                startDate = new DateTime(date.Year, date.Month, date.Day - 1, 18, 0, 0); // 18 h ngày hôm trước
                endDate = new DateTime(date.Year, date.Month, date.Day, 18, 0, 0); // 18 h ngày hôm nay
            }
            else // lấy dữ liệu từ 18h hôm đó -> 18h hôm sau
            {
                startDate = new DateTime(date.Year, date.Month, date.Day, 18, 0, 0); // 18 h ngày hôm đó
                endDate = new DateTime(date.Year, date.Month, date.Day + 1, 18, 0, 0); // 18 h ngày hôm sau
            }

            var rs = _context.Transactions.Join(
                        _context.TransactionDetails,
                        t => t.TraderId,
                        td => td.TransId,
                        (t, td) => new
                        {
                            tran = t,
                            tranDe = td
                        }
                    ).Join(
                        _context.Buyers,
                        lk => lk.tranDe.BuyerId,
                        b => b.ID,
                        (lk, b) => new
                        {
                            tran = lk.tran,
                            tranDe = lk.tranDe,
                            buyer = b
                        }
                    ).Where(x => x.tran.Date >= startDate && x.tran.Date <= endDate);

        }*/
    }
}
