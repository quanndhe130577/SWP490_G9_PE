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
        public List<PondOwnerApiModel> GetPondOwnerByTraderId(int traderId)
        {
            var pondOnwers = _unitOfWork.PondOwners.GetAllByTraderId(traderId);
            List<PondOwnerApiModel> list = new List<PondOwnerApiModel>();
            foreach (var item in pondOnwers)
            {
                list.Add(_mapper.Map<PondOwner, PondOwnerApiModel>(item));
            }
            return list;
        }

        public async Task<int> AddPondOwnerAsync(PondOwnerApiModel pondOwnerModel)
        {
            PondOwner pondOwner = _mapper.Map<PondOwnerApiModel, PondOwner>(pondOwnerModel);
            await _unitOfWork.PondOwners.CreateAsync(pondOwner);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<int> EditPondOwner(PondOwnerApiModel pondOwnerModel)
        {
            var pO = _unitOfWork.PondOwners.GetAllByTraderId(pondOwnerModel.TraderID).Where(x => x.PhoneNumber == pondOwnerModel.PhoneNumber).FirstOrDefault();
            if (pO != null && pO.ID != pondOwnerModel.ID)
            {
                throw new Exception("Đã tồn tại chủ ao với số điện thoại này !!!");
            }

            PondOwner pondOwner = await _unitOfWork.PondOwners.FindAsync(pondOwnerModel.ID);
            pondOwner.Name = pondOwnerModel.Name;
            pondOwner.Address = pondOwnerModel.Address;
            pondOwner.PhoneNumber = pondOwnerModel.PhoneNumber;
            _unitOfWork.PondOwners.Update(pondOwner);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<int> DeletePondOwner(PondOwner pondOwner)
        {
            try
            {
                _unitOfWork.PondOwners.Delete(pondOwner);
                return await _unitOfWork.SaveChangeAsync();
            }
            catch
            {
                throw new Exception("Thông tin của chủ ao đang được sử dụng, không thể xóa");
            }

        }
    }
}
