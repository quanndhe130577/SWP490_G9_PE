using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public List<FishTypeApiModel> GetAllLastFishTypeByTraderId(int traderId)
        {
            var listType = _unitOfWork.FishTypes.GetAllLastByTraderId(traderId);
            List<FishTypeApiModel> list = new List<FishTypeApiModel>();
            foreach (var type in listType)
            {
                list.Add(_mapper.Map<FishType, FishTypeApiModel>(type));
            }
            return list;

        }

        public async Task CreateListFishTypeAsync(List<FishTypeApiModel> listType, int traderId)
        {
            foreach (var obj in listType)
            {
                var fishType = _mapper.Map<FishTypeApiModel, FishType>(obj);
                fishType.TraderID = traderId;
                await _unitOfWork.FishTypes.CreateAsync(fishType);
            }
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task CreateFishTypeAsync(FishTypeApiModel fishType, int traderId)
        {
            var map = _mapper.Map<FishTypeApiModel, FishType>(fishType);
            map.TraderID = traderId;
            await _unitOfWork.FishTypes.CreateAsync(map);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task UpdateFishTypeAsync(FishTypeApiModel fishType, int traderId)
        {
            var fishTypeEdit = await _unitOfWork.FishTypes.FindAsync(fishType.ID);
            fishTypeEdit = _mapper.Map<FishTypeApiModel, FishType>(fishType, fishTypeEdit);
            if (fishTypeEdit.TraderID == traderId)
            {
                _unitOfWork.FishTypes.Update(fishTypeEdit);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Update fail");
            }
        }

        public async Task DeleteFishTypeAsync(int fishTypeId, int traderId)
        {
            var empEdit = await _unitOfWork.FishTypes.FindAsync(fishTypeId);
            if (empEdit.TraderID == traderId)
            {
                _unitOfWork.FishTypes.Delete(empEdit);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Delete fail");
            }
        }
    }
}
