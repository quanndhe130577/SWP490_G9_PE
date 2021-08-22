using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.API.Controllers;
using TnR_SS.DataEFCore;
using TnR_SS.DataEFCore.Repositories;
using TnR_SS.Domain.ApiModels.PurchaseModal;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Supervisor;
using Xunit;

namespace TnR_SS.UnitTest
{
    public class PurchaseUnitTest
    {
        [Fact(DisplayName = "Purchase Controller: Test Create purchase")]
        public async Task TestCreatePurchase()
        {
            List<UserInfor> _users = new List<UserInfor>
                 {
                      new UserInfor() {
                          Id = 1,
                          UserName = "0966848112",
                          PhoneNumber = "0966848112",
                          FirstName = "Q",
                          LastName = "Q"
                      }
                 };

            Mock dbContextMock = new Mock<TnR_SSContext>();
            UserManager<UserInfor> userManagerMock = FakeUserManager.MockUserManager<UserInfor>(_users).Object;
            SignInManager<UserInfor> signInManagerMock = new FakeSignInManager();
            UserInforRepository userInforRepository = new UserInforRepository((TnR_SSContext)dbContextMock.Object, userManagerMock, signInManagerMock);
            PurchaseRepository purchaseRepository = new PurchaseRepository((TnR_SSContext)dbContextMock.Object);
            PondOwnerRepository pondOwnerRepository = new PondOwnerRepository((TnR_SSContext)dbContextMock.Object);

            var date = DateTime.Now;

            PurchaseCreateReqModel purchaseData = new PurchaseCreateReqModel()
            {
                Date = date,
                PondOwnerID = 1,
                TraderID = 1
            };

            PurchaseCreateReqModel purchaseModel = new PurchaseCreateReqModel()
            {
                Date = date,
                PondOwnerID = 1,
                TraderID = 1
            };

            PurchaseResModel newPurchase = new PurchaseResModel()
            {
                Date = date,
                Commission = 0,
                isPaid = false,
                PayForPondOwner = 0,
                PondOwnerId = 1,
                PondOwnerName = "",
                SentMoney = 0,
                Status = PurchaseStatus.Pending.ToString(),
                TotalAmount = 0,
                TotalWeight = 0,
                ID = 0
            };

            Mock<ITnR_SSSupervisor> mock = new Mock<ITnR_SSSupervisor>();
            mock.Setup(repo => repo.CreatePurchaseAsync(purchaseModel)).ReturnsAsync(newPurchase);

            PurchaseController purCon = new PurchaseController(mock.Object);
            var rs = await purCon.CreatePurchase(purchaseData);
            Assert.Equal("Thêm đơn mua thành công", rs.Message);
        }
    }
}
