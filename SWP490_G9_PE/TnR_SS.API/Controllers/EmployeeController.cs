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

        [HttpGet("getallemp")]
        public ResponseModel GetAllEmployeeByTraderId()
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            ListEmployeeModel list = new();
            list.ListEmployee= _tnrssSupervisor.GetAllEmployeeByTraderId(traderId);
            return new ResponseBuilder<ListEmployeeModel>().Success("Get all employee").WithData(list).ResponseModel;
        }

        [HttpPost("create")]
        public async Task<ResponseModel> CreateEmployeeAsynce(EmployeeApiModel employee)
        {
            var traderId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.CreateEmployeesAsync(employee, traderId);
            return new ResponseBuilder().Success("Create Employee Success").ResponseModel;
        }

        [HttpPost("update")]
        public async Task<ResponseModel> UpdateEmployeeAsync(EmployeeApiModel employee)
        {
            await _tnrssSupervisor.UpdateEmployeeAsync(employee);
            return new ResponseBuilder().Success("Update Employee Success").ResponseModel;
        }

        [HttpPost("delete/{empId}")]
        public async Task<ResponseModel> DeleteEmployeeAsync(int empId)
        {
            await _tnrssSupervisor.DeleteEmployeeAsync(empId);
            return new ResponseBuilder().Success("Delete Employee Success").ResponseModel;
        }
    }
}
