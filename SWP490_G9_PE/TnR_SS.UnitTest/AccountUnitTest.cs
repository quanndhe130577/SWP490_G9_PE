using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.API.Controller;
using TnR_SS.DataEFCore;
using TnR_SS.DataEFCore.Repositories;
using TnR_SS.Domain.ApiModels.AccountModel.RequestModel;
using TnR_SS.Domain.ApiModels.AccountModel.ResponseModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;
using TnR_SS.Domain.Supervisor;
using Xunit;

namespace TnR_SS.UnitTest
{
    public class AccountUnitTest
    {
        [Fact(DisplayName = "Repository: Create user with password async")]
        public async Task AddAccountAsync()
        {
            List<UserInfor> _users = new List<UserInfor>
                 {
                      new UserInfor() {
                          Id = userId,
                          UserName = userName,
                          PhoneNumber = phoneNumber,
                          FirstName = firstName,
                          LastName = lastName
                      }
                 };
            Mock dbContextMock = new Mock<TnR_SSContext>();
            UserManager<UserInfor> _userManager = FakeUserManager.MockUserManager<UserInfor>(_users).Object;
            SignInManager<UserInfor> signInManagerMock = new FakeSignInManager();
            UserInforRepository userInforRepository = new UserInforRepository((TnR_SSContext)dbContextMock.Object, _userManager, signInManagerMock);

            UserInfor user = new UserInfor()
            {
                Avatar = null,
                Dob = DateTime.Parse("10/21/1999"),
                FirstName = "Quan",
                LastName = "Nguyen",
                IdentifyCode = "123456789",
                PhoneNumber = "0966848112",
                UserName = "0966848112",
            };

            var rs = await userInforRepository.CreateWithPasswordAsync(user, "12345678");

            Assert.True(rs.Succeeded && _users.Count == 2);
        }

        //[Fact(DisplayName = "Repository: GetUserByPhoneNumber")]
        //public void GetUserByPhoneNumber()
        //{
        //    List<UserInfor> _users = new List<UserInfor>
        //         {
        //              new UserInfor() {
        //                  Id = 1,
        //                  UserName = "0966848112",
        //                  PhoneNumber = "0966848112",
        //                  FirstName = "Q",
        //                  LastName = "Q"
        //              }
        //         };
        //    UserInfor user = new UserInfor()
        //    {
        //        Id = 1,
        //        Avatar = null,
        //        Dob = DateTime.Parse("10/21/1999"),
        //        FirstName = "Quan",
        //        LastName = "Nguyen",
        //        IdentifyCode = "123456789",
        //        PhoneNumber = "0915152665",
        //        UserName = "0915152665",
        //    };

        //    Mock dbContextMock = new Mock<TnR_SSContext>();
        //    var userManageMock = FakeUserManager.MockUserManager<UserInfor>(_users);
        //    userManageMock.Setup(x => x.Users.SingleOrDefault(It.IsAny<Func<UserInfor, bool>>())).Returns(user);
        //    userManageMock.Setup(x => x.Users.SingleOrDefault(It.IsAny<Func<UserInfor, bool>>())).Returns((string phoneNumber) => userManageMock.Object.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber));
        //    UserManager<UserInfor> _userManager = userManageMock.Object;
        //    SignInManager<UserInfor> signInManagerMock = new FakeSignInManager();
        //    UserInforRepository userInforRepository = new UserInforRepository((TnR_SSContext)dbContextMock.Object, _userManager, signInManagerMock);

        //    var rs = userInforRepository.GetUserByPhoneNumber("0966848112");

        //    Assert.Equal(1, rs.Id);
        //}

        [Fact(DisplayName = "Account Controller: Test Login")]
        public async Task Login()
        {
            List<UserInfor> _users = new List<UserInfor>
                 {
                      new UserInfor() {
                          Id = 1,
                          UserName = "0966848112",
                          PhoneNumber = "0966848112",
                          FirstName = "Q",
                          LastName = "Q",
                      }
                 };
            Mock dbContextMock = new Mock<TnR_SSContext>();
            UserManager<UserInfor> _userManager = FakeUserManager.MockUserManager<UserInfor>(_users).Object;
            SignInManager<UserInfor> signInManagerMock = new FakeSignInManager();
            UserInforRepository userInforRepository = new UserInforRepository((TnR_SSContext)dbContextMock.Object, _userManager, signInManagerMock);

            LoginReqModel loginModel = new LoginReqModel()
            {
                PhoneNumber = "0985191100",
                Password = "1234578"
            };

            UserInfor user = new UserInfor()
            {
                Id = 1,
                Avatar = null,
                Dob = DateTime.Parse("10/21/1999"),
                FirstName = "Quan",
                LastName = "Nguyen",
                IdentifyCode = "123456789",
                PhoneNumber = "0985191100",
                UserName = "0985191100",
            };

            UserResModel res = new UserResModel()
            {
                UserID = 1,
                Avatar = null,
                Dob = DateTime.Parse("10/21/1999"),
                FirstName = "Quan",
                LastName = "Nguyen",
                IdentifyCode = "123456789",
                PhoneNumber = "0985191100",
                RoleName = "Trader"
            };

            Mock<ITnR_SSSupervisor> mock = new Mock<ITnR_SSSupervisor>();
            mock.Setup(repo => repo.GetUserByPhoneNumber(loginModel.PhoneNumber)).Returns(user);
            mock.Setup(repo => repo.SignInWithPasswordAsync(user, loginModel.Password)).ReturnsAsync(res);

            AccountController accCon = new AccountController(mock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                        {
                            new Claim("ID", "1"),
                        }, "mock"))
                    }
                }
            };
            NullReferenceException ex = await Assert.ThrowsAsync<NullReferenceException>(async () => await accCon.Login(loginModel));
            Assert.IsType<NullReferenceException>(ex);
        }
    }
}
