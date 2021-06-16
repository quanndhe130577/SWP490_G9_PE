using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
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

        [HttpGet("getallemp/{traderId}")]
        public ResponseModel GetAllEmployeeByTraderId(int traderId)
        {
            ListEmployeeModel list = new();
            list.ListEmployee= _tnrssSupervisor.GetAllEmployeeByTraderId(traderId);
            return new ResponseBuilder<ListEmployeeModel>().Success("Get all employee").WithData(list).ResponseModel;
        }

        [HttpPost("create/{traderId}")]
        public async Task<ResponseModel> CreateEmployeeAsynce(EmployeeApiModel employee, int traderId)
        {
            await _tnrssSupervisor.CreateEmployeesAsync(employee, traderId);
            return new ResponseBuilder().Success("Create Fish Type Success").ResponseModel;
        }
    }
}
