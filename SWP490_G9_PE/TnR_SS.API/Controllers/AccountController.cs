using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TnR_SS.API.Common.ImgurAPI;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.AccountModel.RequestModel;
using TnR_SS.Domain.ApiModels.AccountModel.ResponseModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Supervisor;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TnR_SS.API.Controller
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public AccountController(ITnR_SSSupervisor tnrssSupervisor, IMapper mapper)
        {
            _tnrssSupervisor = tnrssSupervisor;
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

                string avatarLink = await ImgurAPI.UploadImgurAsync(userData.AvatarBase64);

                var result = await _tnrssSupervisor.CreateAsync(userData, avatarLink);
                if (result.Succeeded)
                {
                    return new ResponseBuilder().Success("Register Success").ResponseModel;
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

                var userResModel = await _tnrssSupervisor.SignInWithPasswordAsync(user, userData.Password);
                if (userResModel != null)
                {
                    var token = TokenManagement.GetTokenUser(user.Id);
                    LoginResModel rlm = new LoginResModel()
                    {
                        Token = token,
                        User = userResModel
                    };
                    return new ResponseBuilder<LoginResModel>().Success("Login success").WithData(rlm).ResponseModel;
                }

                return new ResponseBuilder().Error("Invalid Phone number or password").ResponseModel;
            }

            return new ResponseBuilder().Error("Login failed").ResponseModel;
        }
        #endregion

        #region update user
        [HttpPut("user/update/{id}")]
        //[Route("update")]
        public async Task<ResponseModel> UpdateUserInfor(int id, UpdateUserReqModel userData)
        {
            if (!TokenManagement.CheckUserIdFromToken(HttpContext, id))
            {
                return new ResponseBuilder().Error("Access denied").ResponseModel;
            }

            string avatarLink = await ImgurAPI.UploadImgurAsync(userData.AvatarBase64);
            var result = await _tnrssSupervisor.UpdateUserAsync(userData, id, avatarLink);

            if (result.Succeeded)
            {
                var userResModel = await _tnrssSupervisor.GetUserResModelByIdAsync(id);
                return new ResponseBuilder<UserResModel>().Success("Update Success").WithData(userResModel).ResponseModel;
            }

            var errors = result.Errors.Select(x => x.Description).ToList();
            return new ResponseBuilder().Errors(errors).ResponseModel;

        }
        #endregion

        #region change password
        [HttpPut("user/change-password/{id}")]
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
                        User = await _tnrssSupervisor.GetUserResModelByIdAsync(id)
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
        [HttpPost("user/reset-password")]
        [AllowAnonymous]
        public async Task<ResponseModel> ResetPassword(ResetPasswordReqModel resetData)
        {
            var userInfor = _tnrssSupervisor.GetUserByPhoneNumber(resetData.PhoneNumber);

            if (userInfor is null)
            {
                return new ResponseBuilder().WithCode(HttpStatusCode.NotFound).WithMessage("Invalid Information").ResponseModel;
            }

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

        [HttpPost("user/change-phone-number/{id}")]
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

            if (await _tnrssSupervisor.CheckOTPRightAsync(modelData.OTPID, modelData.Code, modelData.NewPhoneNumber))
            {
                var rs = await _tnrssSupervisor.UpdatePhoneNumberAsync(id, modelData.NewPhoneNumber);
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

        #region Test
        [HttpGet("test")]
        [AllowAnonymous]
        public void Test()
        {
            _tnrssSupervisor.CreateBasketTest();
        }
        #endregion
    }
}
