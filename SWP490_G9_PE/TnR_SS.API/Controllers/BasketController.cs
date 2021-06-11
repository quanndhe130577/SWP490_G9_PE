using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/Ro")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public BasketController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }

        public async Task<Basket> CreateRoAsync(Basket ro)
        {
            
        }
    }
}
