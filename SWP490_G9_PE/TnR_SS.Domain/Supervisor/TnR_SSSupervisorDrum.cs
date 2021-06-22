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

        public async Task UpdateDrumAsync(DrumApiModel drum, int traderId)
        {
            var drumEdit = await _unitOfWork.Drums.FindAsync(drum.ID);
            drumEdit = _mapper.Map<DrumApiModel, Drum>(drum, drumEdit);
            if (drumEdit.TruckID == traderId)
            {
                _unitOfWork.Drums.Update(drumEdit);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Update fail");
            }
        }

        public async Task DeleteDrumAsync(int drumId, int truckId)
        {
            var drum = await _unitOfWork.Drums.FindAsync(drumId);
            if (drum.TruckID == truckId)
            {
                _unitOfWork.Drums.Delete(drum);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Delete fail");
            }
        }

        public DrumApiModel GetDetailDrum(int drumId, int truckId)
        {
            var listEmp = GetAllDrumByTruckId(truckId);
            foreach (var obj in listEmp)
            {
                if (obj.TruckID == truckId)
                {
                    return obj;
                }
            }
            return null;
        }
    }
}
