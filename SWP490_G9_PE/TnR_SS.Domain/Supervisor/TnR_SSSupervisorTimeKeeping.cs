using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.BasketModel.ResponseModel;
using TnR_SS.Domain.ApiModels.TimeKeepingModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task<int> AddTimeKeeping(TimeKeepingApiModel timeKeeping)
        {
            TimeKeeping pondOwner = _mapper.Map<TimeKeeping>(timeKeeping);
            await _unitOfWork.TimeKeeping.CreateAsync(pondOwner);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<int> DeleteTimeKeeping(TimeKeeping timeKeeping)
        {
            _unitOfWork.TimeKeeping.Delete(timeKeeping);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<int> EditTimeKeeping(TimeKeepingApiModel timeKeeping)
        {
            TimeKeeping tk = await _unitOfWork.TimeKeeping.FindAsync(timeKeeping.ID);
            tk.WorkDay = timeKeeping.WorkDay;
            tk.Status = timeKeeping.Status;
            tk.Money = timeKeeping.Money;
            tk.Note = timeKeeping.Note;
            tk.EmpId = timeKeeping.EmpId;
            tk.UpdatedAt = DateTime.Now;
            _unitOfWork.TimeKeeping.Update(tk);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<TimeKeeping> GetTimeKeeping(int id)
        {
            TimeKeeping timeKeeping = await _unitOfWork.TimeKeeping.FindAsync(id);
            return timeKeeping;
        }

        public List<TimeKeepingApiModel> GetListTimeKeeping()
        {
            List<TimeKeepingApiModel> timeKeepings = _unitOfWork.TimeKeeping.GetAllAsync().Select(tk => _mapper.Map<TimeKeepingApiModel>(tk)).ToList();
            return timeKeepings;
        }
        public async Task<List<TimeKeepingApiModel>> GetListTimeKeepingByTraderId(int id)
        {
            List<TimeKeepingApiModel> timeKeepings = _unitOfWork.TimeKeeping.GetAllByTraderId(id).Select(tk => _mapper.Map<TimeKeepingApiModel>(tk)).ToList();
            foreach (var tk in timeKeepings)
            {
                var emp = await _unitOfWork.Employees.FindAsync(tk.EmpId);
                //tk.EmpName = emp.FirstName + " " + emp.LastName;
                tk.EmpName = emp.Name;
            }
            return timeKeepings;
        }
        public List<TimeKeepingApiModel> GetListTimeKeepingByEmployeeId(int id)
        {
            List<TimeKeepingApiModel> timeKeepings = _unitOfWork.TimeKeeping.GetAllByEmployeeId(id).Select(tk => _mapper.Map<TimeKeepingApiModel>(tk)).ToList();
            return timeKeepings;
        }
    }
}
