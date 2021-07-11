using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var obj = _mapper.Map<CostIncurredApiModel, CostIncurred>(incurred);
            obj.UserId = traderId;
            await _unitOfWork.CostIncurreds.CreateAsync(obj);
            await _unitOfWork.SaveChangeAsync();
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
