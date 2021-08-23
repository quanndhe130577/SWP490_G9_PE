using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.API.Controllers;
using TnR_SS.DataEFCore;
using TnR_SS.DataEFCore.Repositories;
using TnR_SS.Domain.ApiModels.AccountModel.ResponseModel;
using TnR_SS.Domain.ApiModels.PurchaseModal;
using TnR_SS.Domain.ApiModels.TransactionModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Supervisor;
using Xunit;

namespace TnR_SS.UnitTest
{
    public class PurchaseUnitTest
    {
        public PurchaseUnitTest()
        {
        }

        [Theory(DisplayName = "Purchase Controller: Test Create purchase")]
        [InlineData(1, 1, 2)]
        [InlineData(1, 2, 2)]
        public async Task TestCreatePurchase(int userid, int poid, int traderid)
        {
            Mock<ITnR_SSSupervisor> mock = new Mock<ITnR_SSSupervisor>();
            mock.Setup(m => m.CreatePurchaseAsync(It.IsAny<PurchaseCreateReqModel>()));
            PurchaseController purchase = new PurchaseController(mock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                        {
                            new Claim("ID", userid.ToString()),
                        }, "mock"))
                    }
                }
            };
            var rs = await purchase.CreatePurchase(new PurchaseCreateReqModel()
            {
                Date = DateTime.Now,
                PondOwnerID = poid,
                TraderID = traderid
            });
            if (userid == traderid)
            {
                Assert.Equal("Thêm đơn mua thành công", rs.Message);
            }
            else
            {
                Assert.Equal("Truy cập bị trừ chối", rs.Message);
            }
        }

        [Fact(DisplayName = "Purchase Controller: Test Get All purchase")]
        public async Task TestGetallPurchase()
        {
            Mock<ITnR_SSSupervisor> mock = new Mock<ITnR_SSSupervisor>();
            PurchaseController purchase = new PurchaseController(mock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                        {
                            new Claim("ID", 1.ToString()),
                        }, "mock"))
                    }
                }
            };
            var rs = await purchase.GetAll();
            Assert.Equal("Lấy thông tin tất cả đơn mua thành công", rs.Message);
        }

        // [Theory(DisplayName = "Purchase Controller: Demo Setup")]
        // [InlineData(1)]
        // [InlineData(2)]
        // public async Task DemoSetup(int id)
        // {
        //     Mock<ITnR_SSSupervisor> mock = new Mock<ITnR_SSSupervisor>();
        //     mock.Setup(m => m.GetAllPurchaseAsync(It.Is<int>(i => i == 1))).ReturnsAsync(new List<PurchaseResModel>());
        //     mock.Setup(m => m.GetAllPurchaseAsync(It.Is<int>(i => i != 1))).Throws(new Exception("NotFound"));

        //     if (id == 1)
        //     {
        //         var rs = await mock.Object.GetAllPurchaseAsync(id);
        //         Assert.Empty(rs);
        //     }
        //     else
        //     {
        //         Action action = async () => await mock.Object.GetAllPurchaseAsync(id);
        //         Exception ex = Assert.Throws<Exception>(action);
        //         Assert.Equal("NoFound", ex.Message);
        //     }
        // }
    }
}
