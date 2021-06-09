using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.API.Model.AccountModel.RequestModel;
using TnR_SS.API.Model.AccountModel.ResponseModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controller
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /*private readonly TnR_SSContext _context;
        private readonly UserManager<UserInfor> _userManager;
        private readonly SignInManager<UserInfor> _signInManager;
        private readonly RoleManager<RoleUser> _roleManager;
        private readonly IMapper _mapper;
        private readonly HandleOTP _handleOTP;

        public AccountController(TnR_SSContext context, UserManager<UserInfor> userManager, SignInManager<UserInfor> signInManager,
            RoleManager<RoleUser> roleManager, IMapper mapper, HandleOTP handleOTP)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _handleOTP = handleOTP;
        }*/
        private readonly IMapper _mapper;
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public AccountController(ITnR_SSSupervisor tnrssSupervisor, IMapper mapper)
        {
            _tnrssSupervisor = tnrssSupervisor;
            _mapper = mapper;
        }

        #region Register      
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ResponseModel> Register(RegisterUserReqModel userData)
        {
            if (ModelState.IsValid)
            {
                //check OTP for phoneNumber
                if (!await _tnrssSupervisor.CheckOTPDoneAsync(userData.OTPID, userData.PhoneNumber))
                {
                    return new ResponseBuilder().Error("Access denied").ResponseModel;
                }

                if (_tnrssSupervisor.CheckUserPhoneExists(userData.PhoneNumber))
                {
                    return new ResponseBuilder().Error("Phone Number existed").ResponseModel;
                }

                bool checkRoleExists = await _tnrssSupervisor.RoleExistsAsync(userData.RoleNormalizedName);
                if (!checkRoleExists)
                {
                    return new ResponseBuilder().Error("Role user does not existed").ResponseModel;
                }

                var userInfor = _mapper.Map<RegisterUserReqModel, UserInfor>(userData);
                var result = await _tnrssSupervisor.CreateAsync(userInfor, userData.Password);

                if (result.Succeeded)
                {
                    //add role to user
                    var result_addUserToRole = await _tnrssSupervisor.AddToRoleAsync(userInfor, userData.RoleNormalizedName);
                    if (result_addUserToRole.Succeeded)
                    {
                        return new ResponseBuilder().Success("Register Success").ResponseModel;
                    }

                    //add role to user failed
                    await _tnrssSupervisor.DeleteAsync(userInfor);
                    var errors1 = result_addUserToRole.Errors.Select(x => x.Description).ToList();
                    return new ResponseBuilder().Errors(errors1).ResponseModel;

                }

                var errors = result.Errors.Select(x => x.Description).ToList();
                return new ResponseBuilder().Errors(errors).ResponseModel;

            }

            return new ResponseBuilder().Error("Invalid information").ResponseModel;
        }
        #endregion

        #region Login
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ResponseModel> Login([FromBody] LoginReqModel userData)
        {
            if (ModelState.IsValid)
            {
                var user = _tnrssSupervisor.GetUserByPhoneNumber(userData.PhoneNumber);
                if (user is null)
                {
                    return new ResponseBuilder().Error("User not found").ResponseModel;
                }

                await _tnrssSupervisor.SignOutAsync();
                var userSigninResult = await _tnrssSupervisor.SignInWithPasswordAsync(user, userData.Password);
                if (userSigninResult.Succeeded)
                {

                    await _tnrssSupervisor.SignInAsync(user);
                    var token = TokenManagement.GetTokenUser(user.Id);
                    LoginResModel rlm = new LoginResModel()
                    {
                        Token = token,
                        User = _mapper.Map<UserInfor, UserResModel>(user)
                    };

                    //set role 
                    rlm.User.RoleDisplayName = await _tnrssSupervisor.GetRoleDisplayNameAsync(user);

                    ResponseBuilder<LoginResModel> rpB = new ResponseBuilder<LoginResModel>().Success("Login success").WithData(rlm);
                    return rpB.ResponseModel;
                }

                return new ResponseBuilder().Error("Invalid Phone number or password").ResponseModel;
            }

            return new ResponseBuilder().Error("Login failed").ResponseModel;
        }
        #endregion

        #region update user
        [HttpPut("update/{id}")]
        //[Route("update")]
        public async Task<ResponseModel> UpdateUserInfor(int id, UpdateUserReqModel userData)
        {
            if (!TokenManagement.CheckUserIdFromToken(HttpContext, id))
            {
                return new ResponseBuilder().Error("Access denied").ResponseModel;
            }

            var userInfor = _tnrssSupervisor.GetUserById(id);
            if (userInfor is null)
            {
                return new ResponseBuilder().WithCode(HttpStatusCode.NotFound).WithMessage("User have not registered").ResponseModel;
            }

            userInfor = _mapper.Map<UpdateUserReqModel, UserInfor>(userData, userInfor);
            var result = await _tnrssSupervisor.UpdateUserAsync(userInfor);

            if (result.Succeeded)
            {
                return new ResponseBuilder().Success("Update Success").ResponseModel;
            }

            var errors = result.Errors.Select(x => x.Description).ToList();
            return new ResponseBuilder().Errors(errors).ResponseModel;

        }
        #endregion

        #region change password
        [HttpPut("change-password/{id}")]
        //[Route("change-password")]
        public async Task<ResponseModel> ChangePassword(int id, [FromBody] ChangePasswordReqModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {
                if (!TokenManagement.CheckUserIdFromToken(HttpContext, id))
                {
                    return new ResponseBuilder().Error("Access denied").ResponseModel;
                }

                var userInfor = _tnrssSupervisor.GetUserById(id);
                if (userInfor is null)
                {
                    return new ResponseBuilder().Error("Invalid information").ResponseModel;
                }

                var result = await _tnrssSupervisor.ChangeUserPasswordAsync(userInfor, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);

                if (result.Succeeded)
                {
                    //await _signInManager.RefreshSignInAsync(userInfor);
                    var token = TokenManagement.GetTokenUser(userInfor.Id);
                    LoginResModel rlm = new LoginResModel()
                    {
                        Token = token,
                        User = _mapper.Map<UserInfor, UserResModel>(userInfor)
                    };
                    rlm.User.RoleDisplayName = await _tnrssSupervisor.GetRoleDisplayNameAsync(userInfor);
                    return new ResponseBuilder<LoginResModel>().Success("Update Success").WithData(rlm).ResponseModel;
                }
                else
                {
                    var errors = result.Errors.Select(x => x.Description).ToList();
                    return new ResponseBuilder().Errors(errors).ResponseModel;
                }
            }

            return new ResponseBuilder().Error("Invalid password").ResponseModel;
        }
        #endregion

        #region Reset Password
        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<ResponseModel> ResetPassword(ResetPasswordReqModel resetData)
        {
            var userInfor = _tnrssSupervisor.GetUserByPhoneNumber(resetData.PhoneNumber);

            if (userInfor is null)
            {
                return new ResponseBuilder().WithCode(HttpStatusCode.NotFound).WithMessage("Invalid Information").ResponseModel;
            }

            // check OTP for User
            if (!await _tnrssSupervisor.CheckOTPRightAsync(resetData.OTPID, resetData.Code, resetData.PhoneNumber))
            {
                return new ResponseBuilder().Error("Invalid OTP").ResponseModel;
            }

            var result = await _tnrssSupervisor.ResetUserPasswordAsync(userInfor, resetData.ResetToken, resetData.NewPassword);
            if (result.Succeeded)
            {
                return new ResponseBuilder().Success("Reset Password Success").ResponseModel;
            }
            else
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return new ResponseBuilder().Errors(errors).ResponseModel;
            }
        }
        #endregion

        #region change PhoneNumber 

        [HttpPost("check-change-phone-otp/{id}")]
        public async Task<ResponseModel> CheckChangePhoneNumberOTP(int id, CheckChangePhoneNumberOTPReqModel modelData)
        {
            if (!TokenManagement.CheckUserIdFromToken(HttpContext, id))
            {
                return new ResponseBuilder().Error("Access denied").ResponseModel;
            }

            if (_tnrssSupervisor.CheckUserPhoneExists(modelData.NewPhoneNumber))
            {
                return new ResponseBuilder().Error("Phone Number existed").ResponseModel;
            }
            //check OTP for phoneNumber
            if (await _tnrssSupervisor.CheckOTPRightAsync(modelData.OTPID, modelData.Code, modelData.NewPhoneNumber))
            {
                var user = _tnrssSupervisor.GetUserById(id);
                user.PhoneNumber = modelData.NewPhoneNumber;

                var rs = await _tnrssSupervisor.UpdateUserAsync(user);
                if (rs.Succeeded)
                {
                    return new ResponseBuilder().Success("Success").ResponseModel;
                }

                var errors = rs.Errors.Select(x => x.Description).ToList();
                return new ResponseBuilder().Errors(errors).ResponseModel;
            }


            return new ResponseBuilder().Error("Invalid OTP").ResponseModel;
        }
        #endregion

        #region logout
        [HttpGet("logout")]
        public async Task<ResponseModel> Logout()
        {
            await _tnrssSupervisor.SignOutAsync();
            return new ResponseBuilder().Success("Logout Success").ResponseModel;
        }
        #endregion

    }
}
