using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class FishTypeRepository : RepositoryBase<FishType>, IFishTypeRepository
    {
        public FishTypeRepository(TnR_SSContext context) : base(context) { }
        public List<FishType> GetAllLastByTraderIdAndPondOwnerId(int traderId)
        {
            var rs = _context.FishTypes.AsEnumerable().Where(x => x.TraderID == traderId)
                .OrderByDescending(x => x.Date).ThenByDescending(x => x.ID).GroupBy(x => x.FishName)
                .Select(x => x.First()).ToList();
            return rs;
        }

        public List<FishType> GetAllByTraderId(int traderId)
        {
            var rs = _context.FishTypes.AsEnumerable().Where(x => x.TraderID == traderId && (x.PurchaseID != null || x.Date.Date >= DateTime.Now.AddDays(7)))
                .OrderByDescending(x => x.Date).ToList();
            return rs;
        }

        public void RemoveFishTypeByPurchaseId(int purchaseId)
        {
            var rs = _context.FishTypes.Where(x => x.PurchaseID == purchaseId);
            _context.FishTypes.RemoveRange(rs);
        }

        public double GetTotalWeightOfFishType(int fishTypeId)
        {
            return _context.PurchaseDetails.Where(x => x.FishTypeID == fishTypeId)
                .Join(
                    _context.Baskets,
                    pd => pd.BasketId,
                    bk => bk.ID,
                    (pd, bk) => new
                    {
                        realWeight = pd.Weight - bk.Weight
                    }
                )
                .Sum(x => x.realWeight);
        }

        public double GetSellWeightOfFishType(int fishTypeId)
        {
            return _context.TransactionDetails.Where(x => x.FishTypeId == fishTypeId).Sum(x => x.Weight);
        }

        public List<FishType> GetAllFishTypeByPurchaseIds(List<int> listPurchaseId)
        {
            return _context.FishTypes.AsEnumerable().Join(
                    listPurchaseId,
                    ft => ft.PurchaseID,
                    p => p,
                    (ft, p) => ft
                ).ToList();
        }

        public bool CheckFishTypeOfPurchaseInUse(int purchaseId)
        {
            var rs = _context.Transactions.Where(x => x.isCompleted == TransactionStatus.Pending).Join(
                    _context.TransactionDetails,
                    t => t.ID,
                    td => td.TransId,
                    (t, td) => new
                    {
                        fishTypeId = td.FishTypeId
                    }).Distinct().Join(
                        _context.FishTypes,
                        fti => fti.fishTypeId,
                        ft => ft.ID,
                        (fti, ft) => ft.PurchaseID
                    ).Where(x => x == purchaseId);

            if (rs == null || rs.Count() == 0)
            {
                // chưa được dùng
                return false;
            }

            return true;
        }

        public List<FishType> GetAllFishTypeForTransaction(int? traderId, int userId, DateTime date)
        {
            var userRole = _context.UserRoles.Where(x => x.UserId == userId).Join(
                    _context.RoleUsers,
                    ur => ur.RoleId,
                    ru => ru.Id,
                    (ur, ru) => ru.NormalizedName);

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

            List<int> listPurchaseId = new();
            if (userRole.Contains(RoleName.Trader))
            {
                listPurchaseId = _context.Purchases.Where(x => x.TraderID == userId && x.Date <= endDate && x.Date >= startDate).Select(x => x.ID).ToList();
            }
            else if (userRole.Contains(RoleName.WeightRecorder) && traderId != null)
            {
                listPurchaseId = _context.Purchases.Where(x => x.TraderID == traderId && x.Date <= endDate && x.Date >= startDate).Select(x => x.ID).ToList();
            }

            return GetAllFishTypeByPurchaseIds(listPurchaseId);
        }
    }
}

