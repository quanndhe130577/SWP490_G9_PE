using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Repositories
{
    public interface ITransactionDetailRepository : IRepositoryBase<TransactionDetail>
    {
        List<TransactionDetail> GetAllByWcIDAndDate(int userId, DateTime? date);
        List<TransactionDetail> GetAllByTraderIdAndDate(int traderId, DateTime? date);
        Task DeleteByTransactionIdAsync(int tranId);
        List<TransactionDetail> GetAllByListTransaction(List<int> listTranId);
    }
}
