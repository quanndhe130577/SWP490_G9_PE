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

    }
}
