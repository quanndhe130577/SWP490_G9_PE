using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TnR_SS.API.Areas.AccountManagement.Model;
using TnR_SS.API.Areas.AccountManagement.Model.RequestModel;
using TnR_SS.API.Areas.AccountManagement.Model.ResponseModel;
using TnR_SS.API.Common.OTP;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Entity.Models;

namespace TnR_SS.API.Areas.AccountManagement.Controller
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly TnR_SSContext _context;
        private readonly UserManager<UserInfor> _userManager;
        private readonly SignInManager<UserInfor> _signInManager;
        private readonly RoleManager<RoleUser> _roleManager;
        private readonly IMapper _mapper;

        public AccountController(TnR_SSContext context, UserManager<UserInfor> userManager, SignInManager<UserInfor> signInManager, RoleManager<RoleUser> roleManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        #region Register
        [HttpGet("register-otp/{phoneNumber}")]
        [AllowAnonymous]
        public async Task<ResponseModel> SendRegisterUserOTP(string phoneNumber)
        {
            if (UserPhoneNumberExists(phoneNumber))
            {
                return new ResponseBuilder().Error("Phone Number existed").ResponseModel;
            }

            //send OTP
            var otpId = await TestOTP_Stringee.SendRequestAsync();

            return new ResponseBuilder<Object>().Success("Success").WithData(new { OTPID = otpId }).ResponseModel;
        }

        [HttpPost("check-register-otp")]
        [AllowAnonymous]
        public ResponseModel CheckRegisterUserOTP(OTPReqModel modelData)
        {
            //check OTP for phoneNumber
            if (VerifyRegisterUserOTP(modelData))
            {
                return new ResponseBuilder().Success("OTP Success").ResponseModel;
            }

            return new ResponseBuilder().Error("Invalid OTP").ResponseModel;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ResponseModel> Register(RegisterUserReqModel userData)
        {
            if (ModelState.IsValid)
            {
                //check OTP for phoneNumber
                if (!CheckOTPDone(userData.OTPID, userData.PhoneNumber))
                {
                    return new ResponseBuilder().Error("Access denied").ResponseModel;
                }

                if (UserPhoneNumberExists(userData.PhoneNumber))
                {
                    return new ResponseBuilder().Error("Phone Number existed").ResponseModel;
                }

                bool checkRoleExists = await _roleManager.RoleExistsAsync(userData.RoleNormalizedName);
                if (!checkRoleExists)
                {
                    return new ResponseBuilder().Error("Role user does not existed").ResponseModel;
                }

                var userInfor = _mapper.Map<RegisterUserReqModel, UserInfor>(userData);
                var result = await _userManager.CreateAsync(userInfor, userData.Password);

                if (result.Succeeded)
                {
                    //add role to user
                    var result_addUserToRole = await _userManager.AddToRoleAsync(userInfor, userData.RoleNormalizedName);
                    if (result_addUserToRole.Succeeded)
                    {
                        return new ResponseBuilder().Success("Register Success").ResponseModel;
                    }

                    //add role to user failed
                    await _userManager.DeleteAsync(userInfor);
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
                var user = _userManager.Users.SingleOrDefault(u => u.PhoneNumber == userData.PhoneNumber);
                if (user is null)
                {
                    return new ResponseBuilder().Error("User not found").ResponseModel;
                }

                await _signInManager.SignOutAsync();
                var userSigninResult = await _signInManager.PasswordSignInAsync(user, userData.Password, true, false);
                if (userSigninResult.Succeeded)
                {

                    await _signInManager.SignInAsync(user, false);
                    var token = TokenManagement.GetTokenUser(user.Id);
                    LoginResModel rlm = new LoginResModel()
                    {
                        Token = token,
                        User = _mapper.Map<UserInfor, UserResModel>(user)
                    };

                    //set role 
                    rlm.User.RoleDisplayName = GetRoleDisplayName(user);

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

            var userInfor = _userManager.Users.FirstOrDefault(x => x.Id == id);
            if (userInfor is null)
            {
                return new ResponseBuilder().WithCode(HttpStatusCode.NotFound).WithMessage("User have not registered").ResponseModel;
            }

            userInfor = _mapper.Map<UpdateUserReqModel, UserInfor>(userData, userInfor);
            var result = await _userManager.UpdateAsync(userInfor);

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

                var userInfor = _userManager.Users.FirstOrDefault(x => x.Id == id);
                if (userInfor is null)
                {
                    return new ResponseBuilder().Error("Invalid information").ResponseModel;
                }

                var result = await _userManager.ChangePasswordAsync(userInfor, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);

                if (result.Succeeded)
                {
                    //await _signInManager.RefreshSignInAsync(userInfor);
                    var token = TokenManagement.GetTokenUser(userInfor.Id);
                    LoginResModel rlm = new LoginResModel()
                    {
                        Token = token,
                        User = _mapper.Map<UserInfor, UserResModel>(userInfor)
                    };
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
        [HttpGet("reset-password-token/{phoneNumber}")]
        [AllowAnonymous]
        public ResponseModel GeneratePasswordResetToken(string phoneNumber)
        {
            var userInfor = _userManager.Users.FirstOrDefault(x => x.PhoneNumber.Equals(phoneNumber));
            if (userInfor is null)
            {
                return new ResponseBuilder().WithCode(HttpStatusCode.NotFound).WithMessage("Phone Number haven't registered yet").ResponseModel;
            }

            //generate OTP
            var rs = TestOTP_Stringee.SendRequestAsync();

            //generate reset token
            var token = _userManager.GeneratePasswordResetTokenAsync(userInfor).Result;

            return new ResponseBuilder<Object>().Success("Success").WithData(new { resetToken = token }).ResponseModel;
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<ResponseModel> ResetPassword(ResetPasswordReqModel resetData)
        {
            var userInfor = _userManager.Users.FirstOrDefault(x => x.PhoneNumber.Equals(resetData.PhoneNumber));

            if (userInfor is null)
            {
                return new ResponseBuilder().WithCode(HttpStatusCode.NotFound).WithMessage("Invalid Information").ResponseModel;
            }

            // check OTP for User
            if (!resetData.OTP.Equals("123456"))
            {
                return new ResponseBuilder().Error("Invalid OTP").ResponseModel;
            }

            var result = await _userManager.ResetPasswordAsync(userInfor, resetData.Token, resetData.NewPassword);
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

        [HttpPost("change-phone-number/{id}")]
        public async Task<ResponseModel> SendChangePhoneNumberOTP(int id, ChangePhoneNumberReqModel dataModel)
        {
            if (!TokenManagement.CheckUserIdFromToken(HttpContext, id))
            {
                return new ResponseBuilder().Error("Access denied").ResponseModel;
            }

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (!_userManager.CheckPasswordAsync(user, dataModel.CurrentPassword).Result)
            {
                return new ResponseBuilder().Error("Invalid password").ResponseModel;
            }

            if (UserPhoneNumberExists(dataModel.NewPhoneNumber))
            {
                return new ResponseBuilder().Error("Phone Number existed").ResponseModel;
            }

            //send OTP
            var otpId = await TestOTP_Stringee.SendRequestAsync();

            return new ResponseBuilder<Object>().Success("Success").WithData(new { OTPID = otpId }).ResponseModel;
        }

        [HttpPost("check-change-phone-otp/{id}")]
        public async Task<ResponseModel> CheckChangePhoneNumberOTP(int id, CheckChangePhoneNumberOTPReqModel modelData)
        {
            if (!TokenManagement.CheckUserIdFromToken(HttpContext, id))
            {
                return new ResponseBuilder().Error("Access denied").ResponseModel;
            }

            if (UserPhoneNumberExists(modelData.NewPhoneNumber))
            {
                return new ResponseBuilder().Error("Phone Number existed").ResponseModel;
            }
            //check OTP for phoneNumber
            if (VerifyRegisterUserOTP(modelData))
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                user.PhoneNumber = modelData.NewPhoneNumber;

                var rs = await _userManager.UpdateAsync(user);
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
            await _signInManager.SignOutAsync();
            return new ResponseBuilder().Success("Logout Success").ResponseModel;
        }

        private bool UserPhoneNumberExists(string phoneNumber)
        {
            var rs = _userManager.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
            return rs is not null;
        }
        #endregion

        #region common
        private string GetRoleDisplayName(UserInfor user)
        {
            var userRoles = _userManager.GetRolesAsync(user);
            return _roleManager.FindByNameAsync(userRoles.Result[0]).Result.DisplayName;
        }

        private bool VerifyRegisterUserOTP(OTPReqModel modelData)
        {
            // get OTP Entity
            if (modelData.OTPID != 1) return false;

            //check
            if (modelData.OTP == "123456" /*&& status*/)
            {
                // update status OTP in db
                return true;
            }
            return false;
        }

        private bool CheckOTPDone(int OTPID, string PhoneNumber)
        {
            string rs = "WAITING";
            // get OTP Entity status
            if (OTPID == 1) rs = "DONE";

            if (rs == "DONE" /*&& checkPhone*/)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
