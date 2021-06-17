using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class TimeKeepingRepository : RepositoryBase<TimeKeeping>, ITimeKeepingRepository
    {
        public TimeKeepingRepository(TnR_SSContext context) : base(context) { }
    }
}
