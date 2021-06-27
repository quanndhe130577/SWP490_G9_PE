using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.TruckModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task<Truck> GetTruck(int id)
        {
            return await _unitOfWork.Trucks.FindAsync(id);
        }
        public List<TruckApiModel> GetAllTruckByTraderId(int traderId)
        {
            var listTruck = _unitOfWork.Trucks.GetAllByTraderId(traderId);
            return listTruck.Select(x => _mapper.Map<Truck, TruckApiModel>(x)).ToList();
        }

        public async Task<int> CreateTruckAsync(TruckApiModel truckModel, int traderId)
        {
            var truck = _mapper.Map<TruckApiModel, Truck>(truckModel);
            truck.TraderID = traderId;
            await _unitOfWork.Trucks.CreateAsync(truck);
            await _unitOfWork.SaveChangeAsync();
            return truck.ID;
        }

        public async Task<int> UpdateTruckAsync(TruckApiModel truckModel)
        {
            Truck truck = await _unitOfWork.Trucks.FindAsync(truckModel.Id);
            truck.LicensePlate = truckModel.LicensePlate;
            _unitOfWork.Trucks.Update(truck);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<int> DeleteTruck(Truck truck)
        {
            _unitOfWork.Trucks.Delete(truck);
            return await _unitOfWork.SaveChangeAsync();
        }
    }
}
