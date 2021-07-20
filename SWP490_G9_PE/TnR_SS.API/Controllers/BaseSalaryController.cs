using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.Domain.ApiModels.HistorySalaryEmpModel;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/salary")]
    [ApiController]
    [Authorize]
    public class BaseSalaryController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public BaseSalaryController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [HttpPost("create/{empId}")]
        public async Task<ResponseModel> CreateBaseSalaryAsync(BaseSalaryEmpApiModel salary, int empId)
        {
            await _tnrssSupervisor.CreateBaseSalaryAsync(salary, empId);
            return new ResponseBuilder().Success("Tạo lương cho nhân viên thành công").ResponseModel;
        }

        [HttpGet("getall/{empId}")]
        public ResponseModel GetAllBaseSalary(int empId)
        {
            var listSalary = _tnrssSupervisor.GetAllSalaryByEmpId(empId);
            return new ResponseBuilder<List<BaseSalaryEmpApiModel>>().Success("Lấy thông tin lương nhân viên thành công").WithData(listSalary).ResponseModel;
        }

        [HttpPost("update/{empId}")]
        public async Task<ResponseModel> UpdateBaseSalaryAsync(BaseSalaryEmpApiModel salaryApi, int empId)
        {
            await _tnrssSupervisor.UpdateBaseSalaryAsync(salaryApi, empId);
            return new ResponseBuilder().Success("Cập nhật lương cho nhân viên thành công").ResponseModel;
        }

        [HttpPost("delete/{empId}/{salaryId}")]
        public async Task<ResponseModel> DeleteBaseSalaryAsync(int empId, int salaryId)
        {
            await _tnrssSupervisor.DeleteBaseSalaryAsync(salaryId, empId);
            return new ResponseBuilder().Success("Xóa lương cho nhân viên thành công").ResponseModel;
        }

        [HttpPost("detail/{empId}/{salaryId}")]
        public ResponseModel DetailBaseSalaryAsync(int empId, int salaryId)
        {
            var salary = _tnrssSupervisor.GetDetailBaseSalary(salaryId, empId);
            return new ResponseBuilder<BaseSalaryEmpApiModel>().Success("Lấy thông tin lương cho nhân viên thành công").WithData(salary).ResponseModel;
        }

        [HttpGet("getsalary/{empId}/{date}")]
        public ResponseModel GetSalaryByDate(int empId, DateTime date)
        {
            var salary = _tnrssSupervisor.GetSalaryByDate(date, empId);
            return new ResponseBuilder<BaseSalaryEmpApiModel>().Success("Lấy lương hiện tại của nhân viên thành công").WithData(salary).ResponseModel;
        }
    }
}
