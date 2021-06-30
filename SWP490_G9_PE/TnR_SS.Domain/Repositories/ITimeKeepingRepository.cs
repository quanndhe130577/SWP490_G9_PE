using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.TimeKeepingModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Repositories
{
    public interface ITimeKeepingRepository : IRepositoryBase<TimeKeeping>
    {
        List<TimeKeepingApiModel> GetAllWithTraderIdPerDay(int id, DateTime date);
        List<TimeKeepingApiModel> GetAllWithTraderIdPerMonth(int id, DateTime date);
        List<TimeKeepingApiModel> GetStatisticsByTraderIdByMonth(int id, DateTime date);
        List<TimeKeeping> GetAllByEmployeeId(int id);
    }
}
