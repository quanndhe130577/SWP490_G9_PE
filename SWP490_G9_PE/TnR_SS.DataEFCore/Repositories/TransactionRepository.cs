using System;
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
    }
}
