using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.PondOwnerModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.IRepositories
{
    public interface IPondOwnerRepository : IRepositoryBase<PondOwner>
    {
        List<PondOwnerResModel> GetPondOwnerByTraderId(int traderId);
    }
}
