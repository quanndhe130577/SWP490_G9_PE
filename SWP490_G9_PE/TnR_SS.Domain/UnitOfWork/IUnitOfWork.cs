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
        IFishTypePriceRepository FishTypePrices { get; }
        Task<int> SaveChangeAsync();
    }
}
