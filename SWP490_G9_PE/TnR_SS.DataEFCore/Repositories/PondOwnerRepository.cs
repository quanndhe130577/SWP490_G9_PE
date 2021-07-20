using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TnR_SS.Domain.ApiModels.PondOwnerModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class PondOwnerRepository : RepositoryBase<PondOwner>, IPondOwnerRepository
    {
        public PondOwnerRepository(TnR_SSContext context) : base(context)
        {
        }

        public List<PondOwner> GetAllByTraderId(int traderId)
        {
            /*List<PondOwner> owners = new();
            var rs = from pondOwner in _context.PondOwners.AsEnumerable()
                     join purchases in _context.Purchases.AsEnumerable() on pondOwner.ID.ToString() equals purchases.PondOwnerID.ToString()
                     where purchases.TraderID == traderId
                     group pondOwner by pondOwner.ID.ToString() into listOwner
                     select listOwner;
            foreach (var o in rs)
            {
                owners.Add(o.ToList()[0]);
            }
            return owners;
             */
            return _context.PondOwners.Where(x => x.TraderID == traderId).OrderByDescending(pos => pos.CreatedAt).ToList();
            // return _context.PondOwners.OrderByDescending(pos => pos.CreatedAt).ToList();

        }
    }
}
