using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TnR_SS.DataEFCore.Repositories;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;
using TnR_SS.Domain.UnitOfWork;

namespace TnR_SS.DataEFCore.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TnR_SSContext _context;

        public UnitOfWork(TnR_SSContext context, UserManager<UserInfor> _userManager, SignInManager<UserInfor> _signInManager, RoleManager<RoleUser> _roleManager)
        {
            _context = context;
            OTPs = new OTPRepository(_context);
            UserInfors = new UserInforRepository(_context, _userManager, _signInManager);
            RoleUsers = new RoleUserRepository(_context, _roleManager);
            Baskets = new BasketRepository(_context);
            PondOwners = new PondOwnerRepository(_context);
            PurchaseDetails = new PurchaseDetailRepository(_context);
            Purchases = new PurchaseRepository(_context);
            FishTypes = new FishTypeRepository(_context);
            TimeKeeping = new TimeKeepingRepository(_context);
            Trucks = new TruckRepository(_context);
        }

        public IOTPRepository OTPs { get; private set; }

        public IUserInforRepository UserInfors { get; private set; }

        public IRoleUserRepository RoleUsers { get; private set; }

        public IBasketRepository Baskets { get; private set; }

        public IPondOwnerRepository PondOwners { get; private set; }

        public IPurchaseDetailRepository PurchaseDetails { get; private set; }

        public IPurchaseRepository Purchases { get; private set; }

        public IFishTypeRepository FishTypes { get; private set; }
        public ITimeKeepingRepository TimeKeeping { get; private set; }

        public ITruckRepository Trucks { get; private set; }

        public Task<int> SaveChangeAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
