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
    }
}
