using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.EmployeeModel;
using TnR_SS.Domain.ApiModels.EmployeeModel.ResponseModel;
using TnR_SS.Domain.ApiModels.FishTypeModel.ResponseModel;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public EmployeeController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [HttpGet("getall")]
        public ResponseModel GetAllEmployeeByTraderId()
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            var list = _tnrssSupervisor.GetAllEmployeeByTraderId(traderId);
            return new ResponseBuilder<List<EmployeeApiModel>>().Success("Lấy thông tin nhân viên thành công").WithData(list).ResponseModel;
        }

        [HttpGet("getall/{status}")]
        public ResponseModel GetAllEmployeeByStatus(string status)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            var list = _tnrssSupervisor.GetAllEmployeeByStatus(status, traderId);
            return new ResponseBuilder<List<EmployeeApiModel>>().Success("Lấy thông tin nhân viên thành công").WithData(list).ResponseModel;
        }

        [HttpPost("create")]
        public async Task<ResponseModel> CreateEmployeeAsync(EmployeeApiModel employee)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.CreateEmployeesAsync(employee, traderId);
            return new ResponseBuilder().Success("Tạo nhân viên thành công").ResponseModel;
        }

        [HttpPost("update")]
        public async Task<ResponseModel> UpdateEmployeeAsync(EmployeeApiModel employee)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.UpdateEmployeeAsync(employee, traderId);
            return new ResponseBuilder().Success("Cập nhật thông tin nhân viên thành công").ResponseModel;
        }

        [HttpPost("delete/{empId}")]
        public async Task<ResponseModel> DeleteEmployeeAsync(int empId)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.DeleteEmployeeAsync(empId, traderId);
            return new ResponseBuilder().Success("Xóa thông tin nhân viên thành công").ResponseModel;
        }

        [HttpGet("detail/{empId}")]
        public ResponseModel DetailEmployee(int empId)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            var detail = _tnrssSupervisor.GetDetailEmployee(traderId, empId);
            return new ResponseBuilder<EmployeeApiModel>().Success("Lấy thông tin nhân viên thành công").WithData(detail).ResponseModel;
        }
        [HttpGet("salaryDetail/{date}")]
        public ResponseModel SalaryDetailEmployee(DateTime date)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            var detail = _tnrssSupervisor.GetAllEmployeeSalaryDetailByTraderId(traderId, date);
            return new ResponseBuilder<List<EmployeeSalaryDetailApiModel>>().Success("Lấy thông tin lương nhân viên thành công").WithData(detail).ResponseModel;
        }
    }
}
