using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.UnitTest
{
    public class FakeUserManager : UserManager<UserInfor>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<UserInfor>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<UserInfor>>().Object,
                  new IUserValidator<UserInfor>[0],
                  new IPasswordValidator<UserInfor>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<UserInfor>>>().Object)
        { }
        public static Mock<UserManager<UserInfor>> MockUserManager<TUser>(List<UserInfor> ls)
        {
            var store = new Mock<IUserStore<UserInfor>>();
            var mgr = new Mock<UserManager<UserInfor>>(new Mock<IUserStore<UserInfor>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<UserInfor>>().Object,
                  new IUserValidator<UserInfor>[0],
                  new IPasswordValidator<UserInfor>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<UserInfor>>>().Object);
            mgr.Object.UserValidators.Add(new UserValidator<UserInfor>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<UserInfor>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<UserInfor>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<UserInfor>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<UserInfor, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<UserInfor>())).ReturnsAsync(IdentityResult.Success);


            return mgr;
        }
    }
}
