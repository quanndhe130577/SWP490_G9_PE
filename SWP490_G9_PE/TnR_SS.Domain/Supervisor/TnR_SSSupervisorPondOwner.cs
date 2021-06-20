using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.PondOwnerModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task<PondOwner> GetPondOwner(int id)
        {
            PondOwner pondOwner = await _unitOfWork.PondOwners.FindAsync(id);
            return pondOwner;
        }
        public List<PondOwnerAPIModel> GetPondOwnerByTraderId(int traderId)
        {
            var pondOnwers = _unitOfWork.PondOwners.GetAllByTraderId(traderId);
            List<PondOwnerAPIModel> list = new List<PondOwnerAPIModel>();
            foreach (var item in pondOnwers)
            {
                list.Add(_mapper.Map<PondOwner, PondOwnerAPIModel>(item));
            }
            return list;
        }

        public async Task<int> AddPondOwner(PondOwnerAPIModel pondOwnerModel)
        {
            PondOwner pondOwner = _mapper.Map<PondOwnerAPIModel, PondOwner>(pondOwnerModel);
            await _unitOfWork.PondOwners.CreateAsync(pondOwner);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<int> EditPondOwner(PondOwnerAPIModel pondOwnerModel)
        {
            PondOwner pondOwner = await _unitOfWork.PondOwners.FindAsync(pondOwnerModel.ID);
            pondOwner.Name = pondOwnerModel.Name;
            pondOwner.Address = pondOwnerModel.Address;
            pondOwner.PhoneNumber = pondOwnerModel.PhoneNumber;
            _unitOfWork.PondOwners.Update(pondOwner);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<int> DeletePondOwner(PondOwner pondOwner)
        {
            _unitOfWork.PondOwners.Delete(pondOwner);
            return await _unitOfWork.SaveChangeAsync();
        }
    }
}
