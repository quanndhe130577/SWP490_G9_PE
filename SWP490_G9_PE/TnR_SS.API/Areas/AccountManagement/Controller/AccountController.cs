﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TnR_SS.API.Areas.AccountManagement.Model;
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
        private readonly IMapper _mapper;

        public AccountController(TnR_SSContext context, UserManager<UserInfor> userManager, SignInManager<UserInfor> signInManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        #region Register
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ResponseModel> Register(RegisterReqModel userData)
        {
            if (ModelState.IsValid)
            {
                if (UserPhoneNumberExists(userData.PhoneNumber))
                {
                    return new ResponseBuilder().Error("Phone Number existed").ResponseModel;
                }

                var userInfor = _mapper.Map<RegisterReqModel, UserInfor>(userData);
                var result = await _userManager.CreateAsync(userInfor, userData.Password);
                if (result.Succeeded)
                {
                    return new ResponseBuilder().Success("Create Success").ResponseModel;
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

                var userSigninResult = await _signInManager.PasswordSignInAsync(user, userData.Password, true, false);
                //var userSigninResult = await _userManager.CheckPasswordAsync(user, HandleSHA256.EncryptString(userData.Password + user.SaltPassword));
                if (userSigninResult.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, false);
                    var token = TokenManagement.GetTokenUser(user.Id);
                    LoginResModel rlm = new LoginResModel()
                    {
                        Token = token,
                        UserInfor = _mapper.Map<UserInfor, UserResModel>(user)
                    };
                    ResponseBuilder<LoginResModel> rpB = new ResponseBuilder<LoginResModel>().Success("Login success").WithData(rlm);
                    return rpB.ResponseModel;
                }

                return new ResponseBuilder().Error("Invalid Phone number or password").ResponseModel;
            }

            return new ResponseBuilder().Error("Login failed").ResponseModel;
        }
        #endregion

        [HttpPut("update/{id}")]
        //[Route("update")]
        public async Task<ResponseModel> UpdateUserInfor(int id, UserReqModel userData)
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

            userInfor = _mapper.Map<UserReqModel, UserInfor>(userData, userInfor);
            await _userManager.UpdateAsync(userInfor);
            //_context.Entry(userInfor).State = EntityState.Modified;

            try
            {
                await _userManager.UpdateAsync(userInfor);
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ResponseBuilder().WithCode(HttpStatusCode.Conflict).WithMessage("Conflict information").ResponseModel;
            }

            return new ResponseBuilder().Success("Update Success").ResponseModel;

        }

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
                        UserInfor = _mapper.Map<UserInfor, UserResModel>(userInfor)
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

    }
}