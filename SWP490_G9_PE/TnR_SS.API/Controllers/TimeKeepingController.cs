using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.Domain.Supervisor;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.ApiModels.TimeKeepingModel;
using System.Collections.Generic;
using System;
using TnR_SS.API.Common.Token;

namespace TnR_SS.API.Controller
{
    [Route("api/TimeKeeping")]
    [ApiController]
    public class TimeKeepingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public TimeKeepingController(ITnR_SSSupervisor tnrssSupervisor, IMapper mapper)
        {
            _tnrssSupervisor = tnrssSupervisor;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getByTrader/{date}")]
        public ResponseModel GetByTraderId(DateTime date)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            var rs =  _tnrssSupervisor.GetListTimeKeepingByTraderId(traderId, date);
            return new ResponseBuilder<List<TimeKeepingApiModel>>().Success("Get Info Success").WithData(rs).ResponseModel;
        }

        [HttpGet]
        [Route("getByEmployee/{id}")]
        public ResponseModel GetByEmployeeId(int id)
        {
            var rs = _tnrssSupervisor.GetListTimeKeepingByEmployeeId(id);
            return new ResponseBuilder<List<TimeKeepingApiModel>>().Success("Get Info Success").WithData(rs).ResponseModel;
        }

        [HttpGet]
        [Route("getAll")]
        public ResponseModel GetAll()
        {
            var rs = _tnrssSupervisor.GetListTimeKeeping();
            return new ResponseBuilder<List<TimeKeepingApiModel>>().Success("Get Info Success").WithData(rs).ResponseModel;
        }
        [HttpPost]
        [Route("create")]
        public async Task<ResponseModel> Create(TimeKeepingApiModel timeKeeping)
        {
            var valid = Valid(timeKeeping);
            if (valid.IsValid)
            {
                await _tnrssSupervisor.AddTimeKeeping(timeKeeping);
                return new ResponseBuilder().Success("Thêm thành công").ResponseModel;
            }
            else
            {
                return new ResponseBuilder().Error(valid.Message).ResponseModel;
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<ResponseModel> Update(TimeKeepingApiModel timeKeeping)
        {
            TimeKeeping tk = await _tnrssSupervisor.GetTimeKeeping(timeKeeping.ID);
            if (tk == null)
            {
                return new ResponseBuilder<List<TimeKeepingApiModel>>().Error("Không tìm thấy lịch làm việc").ResponseModel;
            }
            var valid = Valid(timeKeeping);
            if (valid.IsValid)
            {
                await _tnrssSupervisor.EditTimeKeeping(timeKeeping);
                return new ResponseBuilder().Success("Cập nhật thành công").ResponseModel;
            }
            else
            {
                return new ResponseBuilder().Error(valid.Message).ResponseModel;
            }
        }

        [HttpPost]
        [Route("delete/{id}")]
        public async Task<ResponseModel> Delete(int id)
        {
            TimeKeeping timeKeeping = await _tnrssSupervisor.GetTimeKeeping(id);
            if (timeKeeping == null)
            {
                return new ResponseBuilder().Error("Không tìm thấy lịch làm việc").ResponseModel;
            }
            int count = await _tnrssSupervisor.DeleteTimeKeeping(timeKeeping);
            if (count > 0)
            {
                return new ResponseBuilder().Success("Xoá thành công").ResponseModel;
            }
            else
            {
                return new ResponseBuilder().Error("Xoá thất bại").ResponseModel;
            }
        }

        public static TimeKeepingValidModel Valid(TimeKeepingApiModel timeKeeping)
        {
            if (timeKeeping.WorkDay == DateTime.MinValue)
            {
                return new TimeKeepingValidModel() { IsValid = false, Message = "Ngày làm việc được để trống" };
            }
            if (timeKeeping.Status == 0)
            {
                return new TimeKeepingValidModel() { IsValid = false, Message = "Trạng thái không được để trống" };

            }
            if (timeKeeping.EmpId == 0)
            {
                return new TimeKeepingValidModel() { IsValid = false, Message = "Không tìm thấy nhân viên" };
            }
            return new TimeKeepingValidModel() { IsValid = true, Message = "Cập nhật thành công" };
        }
    }
}
