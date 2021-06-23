﻿using System;
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

        public async Task<int> CreateDrumAsync(DrumApiModel drumModel, int traderId)
        {
            var truck = _unitOfWork.Trucks.GetAll(x => x.TraderID == traderId).Select(x => x.ID);
            var drum = _mapper.Map<DrumApiModel, Drum>(drumModel);
            if (truck.Contains(drum.TruckID))
            {
                await _unitOfWork.Drums.CreateAsync(drum);
                await _unitOfWork.SaveChangeAsync();
                return drum.ID;
            }
            else
            {
                throw new Exception("TruckID does not contain in this trader");
            }

        }

        public List<DrumApiModel> GetAllDrumByTraderId(int traderId)
        {
            return _unitOfWork.Drums.GetAllByTraderkId(traderId).Select(x => _mapper.Map<Drum, DrumApiModel>(x)).ToList();
        }

        public async Task UpdateDrumAsync(DrumApiModel drum, int userId)
        {
            var drumEdit = await _unitOfWork.Drums.FindAsync(drum.ID);
            drumEdit = _mapper.Map<DrumApiModel, Drum>(drum, drumEdit);
            var truck = _unitOfWork.Trucks.GetAll(x => x.TraderID == userId).Select(x => x.ID);
            if (truck.Contains(drumEdit.TruckID))
            {
                _unitOfWork.Drums.Update(drumEdit);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Update fail");
            }
        }

        public async Task DeleteDrumAsync(int drumId, int userId)
        {
            var drum = await _unitOfWork.Drums.FindAsync(drumId);
            var truck = _unitOfWork.Trucks.GetAll(x => x.TraderID == userId).Select(x => x.ID);
            if (truck.Contains(drum.TruckID))
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
