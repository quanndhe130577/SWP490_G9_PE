
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWP490_G9_PE.Common;
using SWP490_G9_PE.Common.Response;
using SWP490_G9_PE.Common.Token;
using SWP490_G9_PE.Models;
using SWP490_G9_PE.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SWP490_G9_PE.Controllers
{
    //[Authorize]
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly InventoryContext _context;

        public AuthenticationController(InventoryContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ResponseModel> Login(LoginModel loginModel)
        {
            var rs = await _context.UserInfos.FirstOrDefaultAsync(u => u.Email == loginModel.Email && u.Password == loginModel.Password);
            if (rs != null)
            {
                var token = TokenGeneration.GetToken(rs);
                return new ResponseModel<string>(token);
            }
            return new ResponseModel()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Type = null,
                Message = "Login failed",
                Success = false
            };
        }
    }
}
