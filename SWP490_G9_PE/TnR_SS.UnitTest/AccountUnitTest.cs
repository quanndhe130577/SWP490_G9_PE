using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading.Tasks;
using TnR_SS.DataEFCore;
using TnR_SS.DataEFCore.Repositories;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;
using Xunit;

namespace TnR_SS.UnitTest
{
    public class AccountUnitTest
    {
        Mock<UserManager<UserInfor>> GetUserManagerMock()
        {
            return new Mock<UserManager<UserInfor>>(
                    new Mock<IUserStore<UserInfor>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<UserInfor>>().Object,
                    new IUserValidator<UserInfor>[0],
                    new IPasswordValidator<UserInfor>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<UserInfor>>>().Object);
        }

        Mock<SignInManager<UserInfor>> GetSignInManagerMock()
        {
            return new Mock<SignInManager<UserInfor>>(
                    GetUserManagerMock().Object,
                    /* IHttpContextAccessor contextAccessor */Mock.Of<IHttpContextAccessor>(),
                    /* IUserClaimsPrincipalFactory<TUser> claimsFactory */Mock.Of<IUserClaimsPrincipalFactory<UserInfor>>(),
                    /* IOptions<IdentityOptions> optionsAccessor */null,
                    /* ILogger<SignInManager<TUser>> logger */null,
                    /* IAuthenticationSchemeProvider schemes */null,
                    /* IUserConfirmation<TUser> confirmation */null);
        }

        [Fact(DisplayName = "Repository: Add account")]
        public async Task AddAccount()
        {
            //Setup DbContext and DbSet mock  
            var dbContextMock = new Mock<TnR_SSContext>();
            var dbSetMock = new Mock<DbSet<UserInfor>>();

            //set up identity
            var userManagerMock = GetUserManagerMock();
            var signInManagerMock = GetSignInManagerMock();

            //Execute method of SUT (ProductsRepository)  
            var userInforRepository = new UserInforRepository(dbContextMock.Object, userManagerMock.Object, signInManagerMock.Object);

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
            await userInforRepository.CreateAsync(user);

            /*Assert.NotEmpty(result);
            Assert.Single(result);*/
            //Assert.Equal(1, 1);
        }

        [Fact(DisplayName = "Repository: Create user with password async")]
        public async Task AddAccountAsync()
        {
            // Setup DbContext and DbSet mock
            var dbContextMock = new Mock<TnR_SSContext>();
            var dbSetMock = new Mock<DbSet<UserInfor>>();

            //set up identity
            var userManagerMock = GetUserManagerMock();
            var signInManagerMock = GetSignInManagerMock();

            //Execute method of SUT (ProductsRepository)  
            var userInforRepository = new UserInforRepository(dbContextMock.Object, userManagerMock.Object, signInManagerMock.Object);

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

            await userInforRepository.CreateWithPasswordAsync(user, "12345678");
        }
    }
}
