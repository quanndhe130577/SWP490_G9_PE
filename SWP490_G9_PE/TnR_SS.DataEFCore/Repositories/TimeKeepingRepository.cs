using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.TimeKeepingModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Repositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class TimeKeepingRepository : RepositoryBase<TimeKeeping>, ITimeKeepingRepository
    {
        public TimeKeepingRepository(TnR_SSContext context) : base(context) { }
        public List<TimeKeepingApiModel> GetStatisticsByTraderIdByMonth(int id, DateTime date)
        {
            var rs = from timeKeeping in _context.TimeKeepings
                     join employee in _context.Employees on timeKeeping.EmpId equals employee.ID
                     where employee.TraderId == id && timeKeeping.WorkDay.Month == date.Month && timeKeeping.WorkDay.Year == date.Year
                     orderby employee.CreatedAt descending
                     select new TimeKeepingApiModel()
                     {
                         ID = timeKeeping.ID,
                         EmpId = timeKeeping.EmpId,
                         EmpName = employee.Name,
                         Status = timeKeeping.Status,
                         Money = timeKeeping.Money,
                         Note = timeKeeping.Note,
                         WorkDay = timeKeeping.WorkDay,
                     };
            return rs.ToList();
        }
        public List<TimeKeepingApiModel> GetAllWithTraderIdPerDay(int id, DateTime date)
        {
            var rs = from timeKeeping in _context.TimeKeepings
                     join employee in _context.Employees on timeKeeping.EmpId equals employee.ID
                     where employee.TraderId == id && timeKeeping.WorkDay.Day == date.Day &&
                      timeKeeping.WorkDay.Month == date.Month && timeKeeping.WorkDay.Year == date.Year
                     orderby employee.CreatedAt descending
                     select new TimeKeepingApiModel()
                     {
                         ID = timeKeeping.ID,
                         EmpId = timeKeeping.EmpId,
                         EmpName = employee.Name,
                         Status = timeKeeping.Status,
                         Money = timeKeeping.Money,
                         Note = timeKeeping.Note,
                         WorkDay = timeKeeping.WorkDay,
                     };
            return rs.ToList();
        }
        public List<TimeKeepingApiModel> GetAllWithTraderIdPerMonth(int id, DateTime month)
        {
            var rs = from timeKeeping in _context.TimeKeepings
                     join employee in _context.Employees on timeKeeping.EmpId equals employee.ID
                     where employee.TraderId == id && timeKeeping.WorkDay.Month == month.Month && timeKeeping.WorkDay.Year == month.Year
                     select new TimeKeepingApiModel()
                     {
                         ID = timeKeeping.ID,
                         EmpId = timeKeeping.EmpId,
                         EmpName = employee.Name,
                         Status = timeKeeping.Status,
                         Money = timeKeeping.Money,
                         Note = timeKeeping.Note,
                         WorkDay = timeKeeping.WorkDay,
                     };
            return rs.ToList();
        }
        public List<TimeKeeping> GetAllByEmployeeId(int id)
        {
            var rs = _context.TimeKeepings.Where(tk => tk.EmpId == id).ToList();
            return rs;
        }

        public List<TimeKeeping> GetTimeKeepingPaid(int id, DateTime date)
        {
            return _context.TimeKeepings.Where(tk => tk.EmpId == id && tk.WorkDay.Month == date.Month && tk.WorkDay.Year == date.Year).ToList();
        }
    }
}
