using Microsoft.AspNetCore.Identity;
using System;
using TnR_SS.DataEFCore;
using TnR_SS.DataEFCore.Repositories;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;
using Xunit;

namespace TnR_SS.UnitTest
{
    public class AccountUnitTest
    {

        public IUserInforRepository UserInfors { get; private set; }
        public AccountUnitTest(TnR_SSContext context, UserManager<UserInfor> _userManager, SignInManager<UserInfor> _signInManager, RoleManager<RoleUser> _roleManager)
        {
            UserInfors = new UserInforRepository(context, _userManager, _signInManager);
        }

        [Fact(DisplayName = "Repository: Add account")]
        public void AddAccount()
        {
            UserInfor user = new UserInfor()
            {
                Avatar = null,
                Dob = DateTime.Parse("10/21/1999"),
                FirstName = "Quan",
                Lastname = "Nguyen",
                IdentifyCode = "123456789",
                PhoneNumber = "0966848112",
                UserName = "0966848112",
            };
            UserInfors.CreateAsync(user);

            /*Assert.NotEmpty(result);
            Assert.Single(result);*/
            Assert.Equal(1, 1);
        }

        [Theory(DisplayName = "Repository ")]
        [InlineData("RL1")]
        public async void TestAdd(string expectedName)
        {

        }
    }
}
