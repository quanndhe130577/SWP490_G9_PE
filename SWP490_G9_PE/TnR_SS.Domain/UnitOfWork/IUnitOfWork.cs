using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IOTPRepository OTPs { get; }
        IUserInforRepository UserInfors { get; }
        IRoleUserRepository RoleUsers { get; }
        IBasketRepository Baskets { get; }
        IPondOwnerRepository PondOwners { get; }
        IPurchaseDetailRepository PurchaseDetails { get; }
        IPurchaseRepository Purchases { get; }
        IFishTypeRepository FishTypes { get; }
        ITimeKeepingRepository TimeKeepings { get; }
        ITruckRepository Trucks { get; }
        IEmployeeRepository Employees { get; }
        IDrumRepository Drums { get; }
        ICostIncurredRepository CostIncurreds { get; }
        ILK_PurchaseDeatil_DrumRepository LK_PurchaseDetail_Drums { get; }
        IBuyerRepository Buyers { get; }
        ITraderOfWeightRecorderRepository TraderOfWeightRecorders { get; }
        IHistorySalaryEmpRepository HistorySalaryEmps { get; }
        ITransactionRepository Transactions { get; }
        ITransactionDetailRepository TransactionDetails { get; }
        IEmployeeDebtRepository EmployeeDebts { get; }
        Task<int> SaveChangeAsync();
        IDbContextTransaction BeginTransaction();
        IExecutionStrategy CreateExecutionStrategy();
    }

}