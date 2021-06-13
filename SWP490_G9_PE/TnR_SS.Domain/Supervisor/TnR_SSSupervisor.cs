using AutoMapper;
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
        private readonly IBasketRepository _basketRepository;
        private readonly IPondOwnerRepository _pondOwnerRepository;
        private readonly IPurchaseDetailRepository _purchaseDetailRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IFishTypeRepository _fishTypeRepository;
 
        private readonly IMapper _mapper;
        public IPondOwnerRepository PondOwner { get; }

        public TnR_SSSupervisor()
        {
        }

        public TnR_SSSupervisor(IOTPRepository otpRepository,
            IUserInforRepository userInforRepository,
            IRoleUserRepository roleUserRepository,
            IBasketRepository basketRepository,
            IPondOwnerRepository pondOwnerRepository,
            IPurchaseDetailRepository purchaseDetailRepository,
            IPurchaseRepository purchaseRepository,
            IFishTypeRepository fishTypeRepository,
            IMapper mapper
        )
        {
            _otpRepository = otpRepository;
            _userInforRepository = userInforRepository;
            _roleUserRepository = roleUserRepository;
            _basketRepository = basketRepository;
            _pondOwnerRepository = pondOwnerRepository;
            _purchaseDetailRepository = purchaseDetailRepository;
            _purchaseRepository = purchaseRepository;
            _fishTypeRepository = fishTypeRepository;
            _mapper = mapper;
            PondOwner = pondOwnerRepository;
        }

        public int Complete()
        {
            return 0;
        }
    }
}
