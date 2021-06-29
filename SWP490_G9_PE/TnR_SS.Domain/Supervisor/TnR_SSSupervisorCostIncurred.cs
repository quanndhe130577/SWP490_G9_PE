﻿using System;
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

        public async Task CreateCostIncurredAsync(CostIncurredApiModel employee, int traderId)
        {
            var obj = _mapper.Map<CostIncurredApiModel, CostIncurred>(employee);
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
                throw new Exception("Update fail");
            }
        }

        public async Task DeleteCostIncurredAsync(int empId, int traderId)
        {
            var empEdit = await _unitOfWork.Employees.FindAsync(empId);
            if (empEdit.TraderId == traderId)
            {
                _unitOfWork.Employees.Delete(empEdit);
                await _unitOfWork.SaveChangeAsync();
            }
            else
            {
                throw new Exception("Delete fail");
            }
        }

        public CostIncurredApiModel GetDetailCostIncurred(int traderId, int empId)
        {
            var listEmp = _unitOfWork.CostIncurreds.GetAllCostIncurredByTraderId(traderId);
            foreach (var obj in listEmp)
            {
                if (obj.ID == empId)
                {
                    return _mapper.Map<CostIncurred, CostIncurredApiModel>(obj);
                }
            }
            return null;
        }
    }
}
