using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TnR_SS.Domain.ApiModels.PondOwnerModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.IRepositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class PondOwnerRepository : RepositoryBase<PondOwner>, IPondOwnerRepository
    {
        private readonly IMapper _mapper;
        public PondOwnerRepository(TnR_SSContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public List<PondOwnerResModel> GetPondOwnerByTraderId(int traderId)
        {
            List<PondOwnerResModel> owners = new();
            var rs = from pondOwner in _context.PondOwners.AsEnumerable()
                     join purchases in _context.Purchases.AsEnumerable() on pondOwner.ID.ToString() equals purchases.PondOwnerID.ToString()
                     where purchases.TraderID == traderId
                     group pondOwner by pondOwner.ID.ToString() into listOwner
                     select listOwner;
            foreach (var o in rs)
            {
                owners.Add(_mapper.Map<PondOwnerResModel>(o.ToList()[0]));
            }
            return owners;
        }
    }
}
