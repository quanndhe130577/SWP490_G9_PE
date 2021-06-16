using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.DrumModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public List<DrumApiModel> GetAllDrumByTruckId(int truckId)
        {
            return _unitOfWork.Drums.GetAll(x => x.TruckID == truckId).Select(x => _mapper.Map<Drum, DrumApiModel>(x)).ToList();
        }

        public async Task<int> CreateDrumAsync(DrumApiModel drumModel)
        {
            var drum = _mapper.Map<DrumApiModel, Drum>(drumModel);
            await _unitOfWork.Drums.CreateAsync(drum);
            await _unitOfWork.SaveChangeAsync();
            return drum.ID;
        }

        public List<DrumApiModel> GetAllDrumByTraderId(int traderId)
        {
            return _unitOfWork.Drums.GetAllByTraderkId(traderId).Select(x => _mapper.Map<Drum, DrumApiModel>(x)).ToList();
        }
    }
}
