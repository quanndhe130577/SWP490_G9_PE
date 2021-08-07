using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Repositories
{
    public interface ITransactionRepository : IRepositoryBase<Transaction>
    {
        Task<double> GetTotalWeightAsync(int transId);
        Task<double> GetTotalMoneyAsync(int transId);
        Task<double> GetTotalDebtAsync(int transId);
        List<Transaction> GetAllTransactionsByDate(int userId, DateTime? date);
    }
}
