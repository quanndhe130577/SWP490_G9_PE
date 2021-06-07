using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TnR_SS.API.Areas.AccountManagement.Model.RequestModel;
using TnR_SS.API.Areas.OTPManagement.Model.RequestModel;
using TnR_SS.API.Common.Response;
using TnR_SS.Entity.Models;

namespace TnR_SS.API.Areas.OTPManagement.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OTPController : ControllerBase
    {
        private readonly TnR_SSContext _context;
        private readonly UserManager<UserInfor> _userManager;
        private readonly IMapper _mapper;

        public OTPController(TnR_SSContext context, UserManager<UserInfor> userManager, SignInManager<UserInfor> signInManager, RoleManager<RoleUser> roleManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("register-otp/{phoneNumber}")]
        [AllowAnonymous]
        public async Task<ResponseModel> SendRegisterUserOTP(string phoneNumber)
        {
            if (UserPhoneNumberExists(phoneNumber))
            {
                return new ResponseBuilder().Error("Phone Number existed").ResponseModel;
            }

            //send OTP
            var otpId = await TestOTP_Stringee.SendRequestAsync(phoneNumber);
            if (otpId == 0)
            {
                return new ResponseBuilder<Object>().Error("Send OTP error").ResponseModel;
            }

            return new ResponseBuilder<Object>().Success("Success").WithData(new { OTPID = otpId }).ResponseModel;
        }

        [HttpPost("check-register-otp")]
        [AllowAnonymous]
        public ResponseModel CheckRegisterUserOTP(OTPReqModel modelData)
        {
            //check OTP for phoneNumber
            if (CheckOTPRight(modelData))
            {
                return new ResponseBuilder().Success("OTP Success").ResponseModel;
            }

            return new ResponseBuilder().Error("Invalid OTP").ResponseModel;
        }

        [HttpGet("reset-password-token/{phoneNumber}")]
        [AllowAnonymous]
        public async Task<ResponseModel> GeneratePasswordResetToken(string phoneNumber)
        {
            var userInfor = _userManager.Users.FirstOrDefault(x => x.PhoneNumber.Equals(phoneNumber));
            if (userInfor is null)
            {
                return new ResponseBuilder().WithCode(HttpStatusCode.NotFound).WithMessage("Phone Number haven't registered yet").ResponseModel;
            }

            //generate OTP
            var otpId = await TestOTP_Stringee.SendRequestAsync(phoneNumber);
            if (otpId == 0)
            {
                return new ResponseBuilder().Error("Send OTP error").ResponseModel;
            }

            //generate reset token
            var token = _userManager.GeneratePasswordResetTokenAsync(userInfor).Result;

            return new ResponseBuilder<Object>().Success("Success").WithData(new { resetToken = token, otpid = otpId }).ResponseModel;
        }

        private bool CheckOTPRight(OTPReqModel modelData)
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
        private bool UserPhoneNumberExists(string phoneNumber)
        {
            var rs = _userManager.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
            return rs is not null;
        }
    }
}
