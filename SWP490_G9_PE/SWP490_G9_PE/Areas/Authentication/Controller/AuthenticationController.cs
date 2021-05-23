using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading.Tasks;
using TnR_SS.API.Authentication.Common;
using TnR_SS.API.Authentication.Model;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Entity.Models;

namespace TnR_SS.API.Authentication.Controller
{
    //[Authorize]
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly TnR_SSContext _context;

        public AuthenticationController(TnR_SSContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ResponseModel> Login(LoginModel loginData)
        {
            var user = await _context.UserInfors.FirstOrDefaultAsync(u => u.PhoneNumber == loginData.PhoneNumber);
            if (user != null)
            {
                var rs = LoginHandle.CheckPassword(user.Password, loginData.Password, user.SaltPassword);
                if (rs)
                {
                    var token = TokenGeneration.GetTokenUser(user.Id.ToString("######"));
                    ResponseLoginModel rlm = new ResponseLoginModel()
                    {
                        Token = token,
                        UserID = user.Id.ToString("######")
                    };
                    ResponseBuilder<ResponseLoginModel> rpB = new ResponseBuilder<ResponseLoginModel>().WithData(rlm);
                    return rpB.ResponseModel;
                }

            }
            return new ResponseModel()
            {
                StatusCode = HttpStatusCode.BadGateway,
                Type = null,
                Message = "Login failed",
                Success = false
            };

        }
    }
}
