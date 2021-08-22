using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
                          Id = 1,
                          UserName = "0966848112",
                          PhoneNumber = "0966848112",
                          FirstName = "Q",
                          LastName = "Q"
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

        /*[Fact(DisplayName = "Repository: GetUserByPhoneNumber")]
        public void GetUserByPhoneNumber()
        {
            List<UserInfor> _users = new List<UserInfor>
                 {
                      new UserInfor() {
                          Id = 1,
                          UserName = "0966848112",
                          PhoneNumber = "0966848112",
                          FirstName = "Q",
                          Lastname = "Q"
                      }
                 };

            UserInfor user = new UserInfor()
            {
                Id = 1,
                Avatar = null,
                Dob = DateTime.Parse("10/21/1999"),
                FirstName = "Quan",
                Lastname = "Nguyen",
                IdentifyCode = "123456789",
                PhoneNumber = "0915152665",
                UserName = "0915152665",
            };

            Mock dbContextMock = new Mock<TnR_SSContext>();
            var userManageMock = FakeUserManager.MockUserManager<UserInfor>(_users);
            userManageMock.Setup(x => x.Users.SingleOrDefault(It.IsAny<Func<UserInfor, bool>>())).Returns(user);
            userManageMock.Setup(x => x.Users.SingleOrDefault(It.IsAny<Func<UserInfor, bool>>())).Returns((string phoneNumber) => userManageMock.Object.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber));

            UserManager<UserInfor> _userManager = userManageMock.Object;
            SignInManager<UserInfor> signInManagerMock = new FakeSignInManager();
            UserInforRepository userInforRepository = new UserInforRepository((TnR_SSContext)dbContextMock.Object, _userManager, signInManagerMock);

            var rs = userInforRepository.GetUserByPhoneNumber("0966848112");

            Assert.Equal(1, rs.Id);
        }*/

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
                          LastName = "Q"
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
                PhoneNumber = "0985191100"
            };

            //var rs_create = await _userManager.CreateAsync(user, "12345678");

            Mock<ITnR_SSSupervisor> mock = new Mock<ITnR_SSSupervisor>();
            mock.Setup(repo => repo.GetUserByPhoneNumber(loginModel.PhoneNumber)).Returns(user);
            mock.Setup(repo => repo.SignInWithPasswordAsync(user, loginModel.Password)).ReturnsAsync(res);

            AccountController accCon = new AccountController(mock.Object);
            var rs = await accCon.Login(loginModel);
            Assert.Equal("Login success", rs.Message);


        }
    }
}
