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
            await _unitOfWork.TimeKeepings.CreateAsync(pondOwner);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<int> DeleteTimeKeeping(TimeKeeping timeKeeping)
        {
            _unitOfWork.TimeKeepings.Delete(timeKeeping);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<int> EditTimeKeeping(TimeKeepingApiModel timeKeeping)
        {
            TimeKeeping tk = await _unitOfWork.TimeKeepings.FindAsync(timeKeeping.ID);
            tk.WorkDay = timeKeeping.WorkDay;
            tk.Status = timeKeeping.Status;
            tk.Note = timeKeeping.Note;
            tk.EmpId = timeKeeping.EmpId;
            tk.UpdatedAt = DateTime.Now;
            _unitOfWork.TimeKeepings.Update(tk);
            return await _unitOfWork.SaveChangeAsync();
        }

        public async Task<TimeKeeping> GetTimeKeeping(int id)
        {
            TimeKeeping timeKeeping = await _unitOfWork.TimeKeepings.FindAsync(id);
            return timeKeeping;
        }

        public List<TimeKeepingApiModel> GetListTimeKeeping()
        {
            List<TimeKeepingApiModel> timeKeepings = _unitOfWork.TimeKeepings.GetAllAsync().Select(tk => _mapper.Map<TimeKeepingApiModel>(tk)).ToList();
            return timeKeepings;
        }
        public List<TimeKeepingApiModel> GetListTimeKeepingByTraderIdWithDate(int id, DateTime date)
        {
            List<TimeKeepingApiModel> timeKeepings = _unitOfWork.TimeKeepings.GetAllWithTraderIdPerDay(id, date).ToList();
            return timeKeepings;
        }
        public List<TimeKeepingApiModel> GetListTimeKeepingByTraderIdWithMonth(int id, DateTime date)
        {
            List<TimeKeepingApiModel> timeKeepings = _unitOfWork.TimeKeepings.GetAllWithTraderIdPerMonth(id, date).ToList();
            return timeKeepings;
        }
        public List<TimeKeepingApiModel> GetStatisticsByTraderIdByMonth(int id, DateTime date)
        {
            List<TimeKeepingApiModel> timeKeepings = _unitOfWork.TimeKeepings.GetAllWithTraderIdPerDay(id, date).ToList();
            return timeKeepings;
        }
        public List<TimeKeepingApiModel> GetListTimeKeepingByEmployeeId(int id)
        {
            List<TimeKeepingApiModel> timeKeepings = _unitOfWork.TimeKeepings.GetAllByEmployeeId(id).Select(tk => _mapper.Map<TimeKeepingApiModel>(tk)).ToList();
            return timeKeepings;
        }

        public async Task<int> PaidTimeKeeping(int id, DateTime date)
        {
            List<TimeKeeping> timeKeepings = _unitOfWork.TimeKeepings.GetTimeKeepingPaid(id, date);
            foreach (TimeKeeping timeKeeping in timeKeepings)
            {
                timeKeeping.Note = TimeKeepingNote.IsPaid;
                _unitOfWork.TimeKeepings.Update(timeKeeping);
            }
            return await _unitOfWork.SaveChangeAsync();
        }
    }
}
