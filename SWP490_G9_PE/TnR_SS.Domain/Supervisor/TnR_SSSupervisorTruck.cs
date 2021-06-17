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
        public List<TruckApiModel> GetAllTruckByTraderId(int traderId)
        {
            var listTruck = _unitOfWork.Trucks.GetAllByTraderId(traderId);
            return listTruck.Select(x => _mapper.Map<Truck, TruckApiModel>(x)).ToList();
        }

        public async Task<int> CreateTruckAsync(TruckApiModel truckModel)
        {
            var truck = _mapper.Map<TruckApiModel, Truck>(truckModel);
            await _unitOfWork.Trucks.CreateAsync(truck);
            return truck.ID;
        }
    }
}
