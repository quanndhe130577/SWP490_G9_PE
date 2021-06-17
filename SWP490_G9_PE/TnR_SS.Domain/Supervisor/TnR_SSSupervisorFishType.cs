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

        public async Task CreateFishTypesAsync(List<FishTypeApiModel> listType)
        {
            foreach (var obj in listType)
            {
                var fishType = _mapper.Map<FishTypeApiModel, FishType>(obj);
                await _unitOfWork.FishTypes.CreateAsync(fishType);
            }
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task UpdateFishTypeAsync(FishTypeApiModel fishTypeModel)
        {
            var fishType = await _unitOfWork.FishTypes.FindAsync(fishTypeModel.ID);
            fishType = _mapper.Map<FishTypeApiModel, FishType>(fishTypeModel, fishType);
            _unitOfWork.FishTypes.Update(fishType);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
