using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.Domain.ApiModels.RoleUserModel.RequestModel;
using TnR_SS.Domain.ApiModels.RoleUserModel.ResponseModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controller
{
    [Route("api/role")]
    [ApiController]
    public class RoleUsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public RoleUsersController(ITnR_SSSupervisor tnrssSupervisor, IMapper mapper)
        {
            _tnrssSupervisor = tnrssSupervisor;
            _mapper = mapper;
        }

        // GET: api/RoleUsers
        [HttpGet]
        [Route("get-all")]
        public ResponseModel GetRoleUsers()
        {
            return new ResponseBuilder<List<AllRoleResModel>>().Success("Get list role success").WithData(_tnrssSupervisor.GetAllResRoles()).ResponseModel;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ResponseModel> CreateRoleUsers(CreateRoleReqModel roleData)
        {
            RoleUser roleUser = new RoleUser(roleData.Name, roleData.DisplayName);

            IdentityResult result = await _tnrssSupervisor.AddRoleUserAsync(roleUser);
            if (result.Succeeded)
            {
                return new ResponseBuilder<RoleUser>().Success("Get list role success").WithData(roleUser).ResponseModel;
            }

            var errors = result.Errors.Select(x => x.Description).ToList();
            return new ResponseBuilder().Errors(errors).ResponseModel;
        }
    }
}
