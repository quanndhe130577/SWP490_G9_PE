using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.FishTypeModel;
using TnR_SS.Domain.ApiModels.FishTypeModel.ResponseModel;
using TnR_SS.Domain.ApiModels.PondOwnerModel;
using TnR_SS.Domain.ApiModels.PurchaseDetailModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        readonly Regex regexPrice = new(@"/^\d+$/");
        public async Task<List<FishTypeApiModel>> GetAllLastFishTypeWithPondOwnerId(int traderId)
        {
            var listType = _unitOfWork.FishTypes.GetAllLastByTraderIdAndPondOwnerId(traderId);
            List<FishTypeApiModel> list = new List<FishTypeApiModel>();
            foreach (var type in listType)
            {
                FishType newFish = type;
                newFish.Date = DateTime.Now;
                newFish.ID = 0;
                newFish.PurchaseID = null;
                await _unitOfWork.FishTypes.CreateAsync(newFish);
                await _unitOfWork.SaveChangeAsync();
                list.Add(_mapper.Map<FishType, FishTypeApiModel>(newFish));
            }

            return list;
        }

        public List<FishTypeResModel> GetAllFishTypeByTraderIdAsync(int traderId)
        {
            //var listType = _unitOfWork.FishTypes.GetAllByTraderId(traderId);
            var listType = _unitOfWork.FishTypes.GetAll(x => x.TraderID == traderId && (x.PurchaseID != null || x.Date.Date >= DateTime.Now.AddDays(-7)))
                .OrderByDescending(x => x.Date);
            List<FishTypeResModel> list = new List<FishTypeResModel>();
            foreach (var type in listType)
            {
                FishTypeResModel newFish = _mapper.Map<FishType, FishTypeResModel>(type);
                //newFish.PondOwner = _mapper.Map<PondOwner, PondOwnerApiModel>(await _unitOfWork.PondOwners.FindAsync(newFish.PondOwnerID));
                list.Add(newFish);
            }

            return list;
        }

        public List<WeightRecorderGetAllFishtypeResModel> WeightRecorderGetAllFishTypeByTraderIdAsync(int traderId, DateTime date)
        {
            if (date.Hour < 12)
            {
                date = date.AddDays(-1);
            }

            var listType = _unitOfWork.FishTypes.GetAll(X => X.TraderID == traderId && X.Date.Date == date.Date).Distinct();
            List<WeightRecorderGetAllFishtypeResModel> list = new List<WeightRecorderGetAllFishtypeResModel>();
            foreach (var type in listType)
            {
                WeightRecorderGetAllFishtypeResModel newFish = _mapper.Map<FishType, WeightRecorderGetAllFishtypeResModel>(type);
                //newFish.PondOwner = _mapper.Map<PondOwner, PondOwnerApiModel>(await _unitOfWork.PondOwners.FindAsync(newFish.PondOwnerID));
                list.Add(newFish);
            }

            return list;
        }

        public async Task<List<FishTypeApiModel>> GetFishTypesByPondOwnerIdAndDate(int traderId, int poId, DateTime date)
        {
            var purchaseList = _unitOfWork.Purchases.GetAll(x => x.TraderID == traderId && x.PondOwnerID == poId && x.Date.Date == date.Date).ToList();
            List<PurchaseDetailResModel> list = new List<PurchaseDetailResModel>();
            foreach (var item in purchaseList)
            {
                list.AddRange(await GetAllPurchaseDetailAsync(item.ID));
            }

            var listFishTypeId = list.Select(x => x.FishType.ID).Distinct();
            List<FishTypeApiModel> listFishType = new List<FishTypeApiModel>();
            foreach (var item in listFishTypeId)
            {
                listFishType.Add(_mapper.Map<FishType, FishTypeApiModel>(await _unitOfWork.FishTypes.FindAsync(item)));
            }

            return listFishType;
            //return _unitOfWork.FishTypes.GetAll(x => x.Date.Date == date.Date && x.TraderID == traderId && x.PondOwnerID == poId).Select(x => _mapper.Map<FishType, FishTypeApiModel>(x)).ToList();
        }

        public async Task CreateListFishTypeAsync(ListFishTypeModel listType, int traderId)
        {
            foreach (var obj in listType.ListFishType)
            {
                obj.PurchaseID = listType.PurchaseId;
                var fishType = _mapper.Map<FishTypeApiModel, FishType>(obj);
                fishType.TraderID = traderId;
                if (fishType.MinWeight <= 0 || fishType.MaxWeight <= 0)
                {
                    throw new Exception("Cân nặng phải lớn hơn 0");
                }
                else if (fishType.MaxWeight - fishType.MinWeight < 0)
                {
                    throw new Exception("Cân nặng không hợp lệ");
                }
                else
                {
                    await _unitOfWork.FishTypes.CreateAsync(fishType);
                }
            }
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task CreateFishTypeAsync(FishTypeApiModel fishType, int traderId)
        {
            var map = _mapper.Map<FishTypeApiModel, FishType>(fishType);
            map.TraderID = traderId;

            if (map.MinWeight <= 0 || map.MaxWeight <= 0)
            {
                throw new Exception("Cân nặng phải lớn hơn 0");
            }
            else if (map.MaxWeight - map.MinWeight < 0)
            {
                throw new Exception("Cân nặng không hợp lệ");
            }
            else if (CheckDuplicateFishType(map, traderId) == false)
            {
                throw new Exception("Đã có loại cá này");
            }
            else
            {
                await _unitOfWork.FishTypes.CreateAsync(map);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task UpdateFishTypeAsync(FishTypeApiModel fishType, int traderId)
        {
            var fishTypeEdit = await _unitOfWork.FishTypes.FindAsync(fishType.ID);
            fishTypeEdit = _mapper.Map<FishTypeApiModel, FishType>(fishType, fishTypeEdit);
            if (fishTypeEdit.TraderID == traderId)
            {
                if (fishTypeEdit.MinWeight <= 0 || fishTypeEdit.MaxWeight <= 0)
                {
                    throw new Exception("Cân nặng phải lớn hơn 0");
                }
                else if (fishTypeEdit.MaxWeight - fishTypeEdit.MinWeight < 0)
                {
                    throw new Exception("Cân nặng không hợp lệ");
                }
                else
                {
                    _unitOfWork.FishTypes.Update(fishTypeEdit);
                    await _unitOfWork.SaveChangeAsync();
                }
            }
            else
            {
                throw new Exception("Thông tin giá cá không hợp lệ");
            }
        }

        public async Task UpdateListFishTypeAsync(ListFishTypeModel listFishType, int traderId)
        {
            var strategy = _unitOfWork.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in listFishType.ListFishType)
                        {
                            item.PurchaseID = listFishType.PurchaseId;
                            await UpdateFishTypeAsync(item, traderId);
                        }

                        await transaction.CommitAsync();
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            });
        }

        public async Task DeleteFishTypeAsync(int fishTypeId, int traderId)
        {
            var fishtypeEdit = await _unitOfWork.FishTypes.FindAsync(fishTypeId);
            if (fishtypeEdit.TraderID == traderId)
            {
                try
                {
                    _unitOfWork.FishTypes.Delete(fishtypeEdit);
                    await _unitOfWork.SaveChangeAsync();
                }
                catch
                {
                    throw new Exception("Loại cá này đã được thanh toán trong hóa đơn mua, không thể xóa !!!");
                }
            }
            else
            {
                throw new Exception("Thông tin giá cá không hợp lệ");
            }
        }

        public async Task<List<FishTypeApiModel>> GetListFishTypeByPurchaseIdAsync(int purchaseId, int traderId)
        {
            var purchase = await _unitOfWork.Purchases.FindAsync(purchaseId);
            if (purchase.TraderID != traderId)
            {
                throw new Exception("Đơn mua không tồn tại hoặc đã bị xóa !!!");
            }

            /*var listPurchaseDetail = await GetAllPurchaseDetailAsync(purchaseId);

            var listFishTypeId = listPurchaseDetail.Select(x => x.FishType.ID).Distinct();*/
            var listFishTypeId = _unitOfWork.FishTypes.GetAll(x => x.PurchaseID == purchaseId && x.TraderID == traderId).Select(x => x.ID);
            List<FishTypeApiModel> listFishType = new List<FishTypeApiModel>();
            foreach (var item in listFishTypeId)
            {
                listFishType.Add(_mapper.Map<FishType, FishTypeApiModel>(await _unitOfWork.FishTypes.FindAsync(item)));
            }

            return listFishType;
        }

        private bool CheckDuplicateFishType(FishType model, int traderId)
        {
            var listFish = GetAllFishTypeByTraderIdAsync(traderId);
            foreach (var fish in listFish)
            {
                if (model.FishName == fish.FishName && model.Date.ToString("yyyy/MM/DD") == fish.Date.ToString("yyyy/MM/DD"))
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<FishTypeApiModel> GetNewFishTypeAsync(int traderId, DateTime? date)
        {
            var roleUser = await _unitOfWork.UserInfors.GetRolesAsync(traderId);
            if (!roleUser.Contains(RoleName.Trader))
            {
                throw new Exception("Tài khoản không phù hợp !!!");
            }

            FishType newFish = new FishType();
            newFish.TraderID = traderId;
            newFish.Date = date == null ? DateTime.Now : date.Value;
            newFish.FishName = "Cá mới";

            await _unitOfWork.FishTypes.CreateAsync(newFish);
            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<FishType, FishTypeApiModel>(newFish);
        }
    }
}
