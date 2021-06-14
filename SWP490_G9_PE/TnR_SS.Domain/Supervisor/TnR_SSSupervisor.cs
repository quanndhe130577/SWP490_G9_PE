using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;
using TnR_SS.Domain.UnitOfWork;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor : ITnR_SSSupervisor
    {
        private readonly IUnitOfWork _unitOfWork;


        private readonly IMapper _mapper;

        public TnR_SSSupervisor()
        {
        }

        public TnR_SSSupervisor(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public void Test()
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
            _unitOfWork.UserInfors.CreateAsync(user);
        }
    }
}
