using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.DrumModel;
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
            truck.Name = truckModel.Name;
            truck.LicensePlate = truckModel.LicensePlate;
            _unitOfWork.Trucks.Update(truck);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<int> DeleteTruck(Truck truck)
        {
            _unitOfWork.Trucks.Delete(truck);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<List<TruckDateModel>> GetDetailTrucksByDate(int traderId, DateTime date)
        {
            var listPurchaseDetail = _unitOfWork.PurchaseDetails.GetPurchaseDetailByDate(traderId, date);
            Dictionary<int, DrumWeightModel> dicDrumWeight = new Dictionary<int, DrumWeightModel>();
            foreach (var purchaseDetail in listPurchaseDetail)
            {
                // list Drum của purchase detail đó
                var listDrum = _unitOfWork.Drums.GetDrumsByPurchaseDetail(purchaseDetail);
                foreach (var drum in listDrum)
                {
                    if (!dicDrumWeight.ContainsKey(drum.ID))
                    {
                        DrumWeightModel drumW = _mapper.Map<Drum, DrumWeightModel>(drum);
                        Basket basket = await _unitOfWork.Baskets.FindAsync(purchaseDetail.BasketId);
                        // trừ đi cân nặng basket rồi chia đều weight cho các drum
                        drumW.TotalWeight = (purchaseDetail.Weight - basket.Weight) / listDrum.Count;
                        dicDrumWeight.Add(drum.ID, drumW);
                    }
                    else
                    {
                        dicDrumWeight[drum.ID].TotalWeight += purchaseDetail.Weight / listDrum.Count;
                    }
                }
            }

            Dictionary<int, TruckDateModel> dicTruckDate = new Dictionary<int, TruckDateModel>();
            foreach (var drumW in dicDrumWeight.Values)
            {
                if (dicTruckDate.ContainsKey(drumW.TruckId))
                {
                    dicTruckDate[drumW.TruckId].ListDrumWeight.Add(drumW);
                }
                else
                {
                    Truck truck = await _unitOfWork.Trucks.FindAsync(drumW.TruckId);
                    TruckDateModel truckDateModel = _mapper.Map<Truck, TruckDateModel>(truck);
                    truckDateModel.ListDrumWeight.Add(drumW);
                    dicTruckDate.Add(drumW.TruckId, truckDateModel);
                }
            }

            return dicTruckDate.Values.ToList();
        }

    }
}
