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
        public List<TransactionDetail> GetAllTransactionByWcIDAndDate(int wcId, DateTime? date)
        {
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
                rs = rs.Where(x => x.tran.Date.Date == date.Value.Date);
            }

            return rs.Select(x => x.tranDe).ToList();
        }

        public List<TransactionDetail> GetAllTransactionByTraderIdAndDate(int traderId, DateTime? date)
        {
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
                rs = rs.Where(x => x.tran.Date.Date == date.Value.Date);
            }

            return rs.Select(x => x.tranDe).ToList();
        }

        public async Task DeleteByTransactionIdAsync(int tranId)
        {
            var list = _context.TransactionDetails.Where(x => x.TransId == tranId);
            _context.TransactionDetails.RemoveRange(list);
            await _context.SaveChangesAsync();
        }
    }
}
