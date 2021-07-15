using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.TransactionDetailModel;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionDetailController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public TransactionDetailController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        [Route("create")]
        public async Task<ResponseModel> Create(CreateTransactionDetailApiModel model)
        {
            var wcId = TokenManagement.GetUserIdInToken(HttpContext);
            await _tnrssSupervisor.CreateTransactionDetailAsync(model, wcId);
            return new ResponseBuilder().Success("Tạo đơn bán thành công").ResponseModel;
        }
    }
}
