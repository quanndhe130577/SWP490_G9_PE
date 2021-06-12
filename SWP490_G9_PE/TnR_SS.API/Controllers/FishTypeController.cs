using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controllers
{
    [Route("api/fishtype")]
    [ApiController]
    public class FishTypeController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public FishTypeController(ITnR_SSSupervisor tnrssSupervisor)
        {
            _tnrssSupervisor = tnrssSupervisor;
        }


    }
}
