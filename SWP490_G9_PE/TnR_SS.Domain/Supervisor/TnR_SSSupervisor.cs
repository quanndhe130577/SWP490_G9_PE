using AutoMapper;
using System;
using TnR_SS.Domain.Entities;
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

        public int EndHour = 1;
    }
}
