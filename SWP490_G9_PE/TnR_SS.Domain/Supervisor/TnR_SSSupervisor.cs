using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.IRepositories;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor : ITnR_SSSupervisor
    {
        private readonly IOTPRepository _otpRepository;
        private readonly IUserInforRepository _userInforRepository;
        private readonly IRoleUserRepository _roleUserRepository;

        public TnR_SSSupervisor()
        {
        }

        public TnR_SSSupervisor(IOTPRepository otpRepository,
            IUserInforRepository userInforRepository,
            IRoleUserRepository roleUserRepository
        )
        {
            _otpRepository = otpRepository;
            _userInforRepository = userInforRepository;
            _roleUserRepository = roleUserRepository;
        }

    }
}
