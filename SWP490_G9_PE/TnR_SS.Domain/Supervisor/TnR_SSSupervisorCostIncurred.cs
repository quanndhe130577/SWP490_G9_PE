using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.CostIncurredModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public List<CostIncurredApiModel> GetAllCostIncurredTraderId(int traderId)
        {
            var listIncurred = _unitOfWork.CostIncurreds.GetAllCostIncurredByTraderId(traderId);
            List<CostIncurredApiModel> list = new();
            foreach (var obj in listIncurred)
            {
                list.Add(_mapper.Map<CostIncurred, CostIncurredApiModel>(obj));
            }
            return list;
        }

        public async Task CreateCostIncurredAsync(CostIncurredApiModel incurred, int traderId)
        {
            var regexCost = @"^[1-9]\d*(\.\d+)?$";
            if (incurred.TypeOfCost == null || incurred.TypeOfCost.Trim() == "")
            {
                throw new Exception("Loại chi phí đang để trống!");
            }
            else if(incurred.Name == null || incurred.Name.Trim() == "")
            {
                throw new Exception("Tên chi phí không được để trống!");
            }else if(!Regex.IsMatch(incurred.Cost.ToString(), regexCost))
            {
                throw new Exception("Chi phí không hợp lệ!");
            }
            else
            {
                var obj = _mapper.Map<CostIncurredApiModel, CostIncurred>(incurred);
                obj.UserId = traderId;
                await _unitOfWork.CostIncurreds.CreateAsync(obj);
                await _unitOfWork.SaveChangeAsync();
            }
        }

        public async Task UpdateCostIncurredAsync(CostIncurredApiModel incurred, int traderId)
        {
            var incurredEdit = await _unitOfWork.CostIncurreds.FindAsync(incurred.ID);
            incurredEdit = _mapper.Map<CostIncurredApiModel, CostIncurred>(incurred, incurredEdit);
            if (incurredEdit.UserId == traderId)
            {

                _unitOfWork.CostIncurreds.Update(incurredEdit);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Thông tin chi phí không đúng");
            }
        }

        public async Task DeleteCostIncurredAsync(int incurredId, int userId)
        {
            var incurredEdit = await _unitOfWork.CostIncurreds.FindAsync(incurredId);
            if (incurredEdit.UserId == userId)
            {
                _unitOfWork.CostIncurreds.Delete(incurredEdit);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Thông tin chi phí không đúng");
            }
        }

        public CostIncurredApiModel GetDetailCostIncurred(int traderId, int incurredId)
        {
            var listEmp = _unitOfWork.CostIncurreds.GetAllCostIncurredByTraderId(traderId);
            foreach (var obj in listEmp)
            {
                if (obj.ID == incurredId)
                {
                    return _mapper.Map<CostIncurred, CostIncurredApiModel>(obj);
                }
            }
            return null;
        }
    }
}
