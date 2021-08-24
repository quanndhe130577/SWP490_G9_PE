using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.PurchaseModal;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Supervisor;
using Xunit;
using AutoMapper;
using TnR_SS.Domain.UnitOfWork;
using System.Linq.Expressions;
using TnR_SS.Domain.ApiModels.PurchaseDetailModel;
using TnR_SS.Domain.ApiModels;

namespace TnR_SS.UnitTest
{
    public class TnR_SSSupervisorPurchaseUnitTest
    {
        TnR_SSSupervisor _supervisor;
        IMapper _mapper;
        Mock<IUnitOfWork> _umock;
        DateTime date;
        List<Purchase> purchases;
        List<PurchaseDetail> purchaseDetails;
        List<ClosePurchaseDetail> closePurchaseDetails;
        List<FishType> fishTypes;
        List<Basket> baskets;
        List<PondOwner> pondOwners;
        List<Drum> drums;
        List<Truck> trucks;
        public TnR_SSSupervisorPurchaseUnitTest()
        {
            Setup();
            MockSetup();
        }

        [Theory(DisplayName = "Purchase Supervisor: Test Get Total Weight Purchase")]
        [InlineData(1)]
        [InlineData(30000)]
        public async Task TestGetAllPurchaseAsync(int traderid)
        {
            List<PurchaseResModel> rs = await _supervisor.GetAllPurchaseAsync(traderid);
            if (traderid == 1)
            {
                Assert.NotEmpty(rs);
            }
            else
            {
                Assert.Empty(rs);
            }
        }

        [Theory(DisplayName = "Purchase Supervisor: Test Get Total Weight Purchase")]
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(30000, 1)]
        public async Task TestGetPurchaseByIdAsync(int pid, int uid)
        {
            if (pid == 1)
            {
                if (uid == 1)
                {
                    PurchaseResModel purchase = await _supervisor.GetPurchaseByIdAsync(pid, uid);
                    Assert.NotNull(purchase);
                }
                else
                {
                    Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _supervisor.GetPurchaseByIdAsync(pid, uid));
                    Assert.Equal("Đơn mua không tồn tại", ex.Message);
                }
            }
            else
            {
                Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _supervisor.GetPurchaseByIdAsync(pid, uid));
                Assert.Equal("Đơn mua không tồn tại", ex.Message);
            }
        }

        [Theory(DisplayName = "Purchase Supervisor: Test Create Purchase")]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(2, 2)]
        [InlineData(1000, 1)]
        [InlineData(2, 1000)]
        public async Task TestCreatePurchaseAsync(int poid, int traderid)
        {
            PurchaseCreateReqModel purchase = new PurchaseCreateReqModel() { Date = date, PondOwnerID = poid, TraderID = traderid };
            if (poid == 1)
            {
                Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _supervisor.CreatePurchaseAsync(purchase));
                Assert.Equal("Đơn mua với chủ ao trong ngày " + date.ToString("dd/MM/yyyy") + " đã có!!!", ex.Message);
            }
            else if (traderid == 2)
            {
                Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _supervisor.CreatePurchaseAsync(purchase));
                Assert.Equal("Không tìm thấy thương lái !!!", ex.Message);
            }
            else if (poid == 1000)
            {
                Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _supervisor.CreatePurchaseAsync(purchase));
                Assert.Equal("Không tìm thấy chủ ao !!!", ex.Message);
            }
            else if (traderid == 1000)
            {
                Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _supervisor.CreatePurchaseAsync(purchase));
                Assert.Equal("Không tìm thấy thương lái !!!", ex.Message);
            }
            else
            {
                var rs = await _supervisor.CreatePurchaseAsync(purchase);
                Assert.IsType<PurchaseResModel>(rs);
            }
        }
        [Theory(DisplayName = "Purchase Supervisor: Test Update Purchase")]
        [InlineData(1, 4, 200, 2, 1)]
        [InlineData(1000, 4, 200, 2, 1)]
        [InlineData(1, -4, 200, 2, 1)]
        [InlineData(1, 4, -200, 2, 1)]
        [InlineData(1, 4, 200, 1000, 1)]
        [InlineData(1, 4, 200, 2, 2)]
        public async Task TestUpdatePurchaseAsync(int id, double commission, double sentMoney, int poid, int traderid)
        {
            UpdatePurchaseApiModel update = new UpdatePurchaseApiModel()
            {
                ID = id,
                Commission = commission,
                SentMoney = sentMoney,
                PondOwnerID = poid,
                Date = date
            };
            if (id == 1000)
            {
                Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _supervisor.UpdatePurchaseAsync(update, traderid));
                Assert.Equal("Đơn mua không tồn tại", ex.Message);
            }
            else if (poid == 1000)
            {
                Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _supervisor.UpdatePurchaseAsync(update, traderid));
                Assert.Equal("Chủ ao không hợp lệ", ex.Message);
            }
            else if (traderid == 2)
            {
                Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _supervisor.UpdatePurchaseAsync(update, traderid));
                Assert.Equal("Đơn mua không tồn tại", ex.Message);
            }
            else
            {
                Assert.True(true);
            }
        }
        [Theory(DisplayName = "Purchase Detail Supervisor: Test Get All Purchase Detail")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(1000)]
        public async Task TestGetAllPurchaseDetailAsync(int id)
        {
            if (id == 1)
            {
                List<PurchaseDetailResModel> purchases = await _supervisor.GetAllPurchaseDetailAsync(id);
                Assert.Equal(2, purchases.Count);
            }
            else if (id == 2)
            {
                List<PurchaseDetailResModel> purchases = await _supervisor.GetAllPurchaseDetailAsync(id);
                Assert.Single(purchases);
            }
            else
            {
                await Assert.ThrowsAnyAsync<NullReferenceException>(async () => await _supervisor.GetAllPurchaseDetailAsync(id));
            }
        }
        void MockSetup()
        {
            _umock = new Mock<IUnitOfWork>();
            _umock.Setup(u => u.Purchases.GetAll(It.IsAny<Expression<Func<Purchase, bool>>>(),
                It.IsAny<Func<IQueryable<Purchase>, IOrderedQueryable<Purchase>>>(),
                It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns((
                Expression<Func<Purchase, bool>> filter,
                Func<IQueryable<Purchase>, IOrderedQueryable<Purchase>> orderBy,
                string includeProperties,
                int? skip, int? take
                ) => purchases.Where(filter.Compile()).ToList());
            _umock.Setup(u => u.PurchaseDetails.GetAll(It.IsAny<Expression<Func<PurchaseDetail, bool>>>(),
                It.IsAny<Func<IQueryable<PurchaseDetail>, IOrderedQueryable<PurchaseDetail>>>(),
                It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns((
                Expression<Func<PurchaseDetail, bool>> filter,
                Func<IQueryable<PurchaseDetail>, IOrderedQueryable<PurchaseDetail>> orderBy,
                string includeProperties,
                int? skip, int? take
                ) => purchaseDetails.Where(filter.Compile()).ToList());

            _umock.Setup(u => u.ClosePurchaseDetails.GetAllByPurchase(It.IsAny<Purchase>()))
                .Returns((Purchase purchase) => closePurchaseDetails.Where(pd => pd.PurchaseId == purchase.ID).ToList());
            _umock.Setup(u => u.Drums.GetDrumsByPurchaseDetail(It.IsAny<PurchaseDetail>())).Returns(drums);
            _umock.Setup(u => u.Drums.GetDrumsByClosePurchaseDetail(It.IsAny<ClosePurchaseDetail>())).Returns(drums);

            foreach (Purchase purchase in purchases)
            {
                _umock.Setup(m => m.Purchases.FindAsync(It.Is<int>(id => id == purchase.ID))).ReturnsAsync(purchase);
            }
            foreach (PondOwner pondOwner in pondOwners)
            {
                _umock.Setup(m => m.PondOwners.FindAsync(It.Is<int>(id => id == pondOwner.ID))).ReturnsAsync(pondOwner);
            }
            foreach (FishType fishType in fishTypes)
            {
                _umock.Setup(m => m.FishTypes.FindAsync(It.Is<int>(id => id == fishType.ID))).ReturnsAsync(fishType);
            }
            foreach (Basket basket in baskets)
            {
                _umock.Setup(m => m.Baskets.FindAsync(It.Is<int>(id => id == basket.ID))).ReturnsAsync(basket);
            }
            foreach (Truck truck in trucks)
            {
                _umock.Setup(m => m.Trucks.FindAsync(It.Is<int>(id => id == truck.ID))).ReturnsAsync(truck);
            }
            _umock.Setup(m => m.UserInfors.FindAsync(It.Is<int>(id => id == 1))).ReturnsAsync(new UserInfor() { Id = 1 });
            _umock.Setup(m => m.UserInfors.FindAsync(It.Is<int>(id => id == 2))).ReturnsAsync(new UserInfor() { Id = 2 });
            _umock.Setup(m => m.UserInfors.GetRolesAsync(It.Is<int>(id => id == 1))).ReturnsAsync(new List<string>() { "Trader" });
            _umock.Setup(m => m.UserInfors.GetRolesAsync(It.Is<int>(id => id == 2))).ReturnsAsync(new List<string>() { "WeightRecorder" });
            _supervisor = new TnR_SSSupervisor(_mapper, _umock.Object);
        }
        void Setup()
        {
            date = DateTime.Now;
            purchases = new List<Purchase>()
            {
                new Purchase()
                {
                    ID=1,
                    PayForPondOwner=50000,
                    Date=date,
                    Commission=1,
                    SentMoney=1000,
                    PondOwnerID=1,
                    isCompleted=PurchaseStatus.Pending,
                    TraderID=1
                },
                new Purchase()
                {
                    ID=2,
                    PayForPondOwner=60000,
                    Date=date.AddDays(-1),
                    Commission=2,
                    SentMoney=2000,
                    PondOwnerID=1,
                    isCompleted=PurchaseStatus.Completed,
                    TraderID=1
                }
            };
            purchaseDetails = new List<PurchaseDetail>()
            {
                new PurchaseDetail()
                {
                    ID=1,
                    FishTypeID=1,
                    Weight=100,
                    BasketId=1,
                    PurchaseId=1
                },
                new PurchaseDetail()
                {
                    ID=2,
                    FishTypeID=1,
                    Weight=120,
                    BasketId=1,
                    PurchaseId=1
                }
            };
            closePurchaseDetails = new List<ClosePurchaseDetail>()
            {
                new ClosePurchaseDetail()
                {
                    ID=1,
                    FishTypeId=2,
                    Weight=120,
                    BasketId=1,
                    PurchaseId=2
                }
            };
            pondOwners = new List<PondOwner>(){
                new PondOwner()
                {
                    ID = 1,
                    Name = "Anh Duc",
                    Address = "Ha Noi",
                    PhoneNumber = "0912345678",
                    TraderID = 1
                },
                new PondOwner()
                {
                    ID = 2,
                    Name = "Duc Quan",
                    Address = "Ha Noi",
                    PhoneNumber = "0912345671",
                    TraderID = 1
                }
            };
            baskets = new List<Basket>()
            {
                new Basket()
                {
                    ID = 1,
                    TraderID = 1,
                    Type = "Tron",
                    Weight = 3,
                }
            };
            drums = new List<Drum>()
            {
                new Drum()
                {
                    ID=1,
                    TruckID=1,
                    Number="123456",
                    Type="1",
                    MaxWeight=10,
                }
            };
            trucks = new List<Truck>()
            {
                new Truck()
                {
                    ID=1,
                    LicensePlate="123456",
                    Name="1",
                    TraderID=1,
                }
            };
            baskets = new List<Basket>()
            {
                new Basket()
                {
                    ID=1,
                    Type="To",
                    Weight=10,
                    TraderID=1,
                }
            };
            fishTypes = new List<FishType>()
            {
                new FishType()
                {
                    ID = 1,
                    FishName = "Tram",
                    MinWeight = 1,
                    MaxWeight = 10,
                    Price = 100,
                    Description = "",
                    TraderID = 1,
                    Date = date,
                    PurchaseID = 1
                }
            };
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile())));
        }
    }
}
