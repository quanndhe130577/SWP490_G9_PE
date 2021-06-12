using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.IRepositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class PondOwnerRepository : RepositoryBase<PondOwner>, IPondOwnerRepository
    {
        public PondOwnerRepository(TnR_SSContext context) : base(context) { }
    }
}
