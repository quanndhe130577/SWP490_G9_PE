using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TnR_SS.API.Areas.OTPManagement.Model.RequestModel;
using TnR_SS.API.Common.HandleOTP;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Entity.Models;

namespace TnR_SS.API.Areas.OTPManagement.Controller
{
    [Route("api/OTP")]
    [ApiController]
    public class OTPController : ControllerBase
    {
        private readonly TnR_SSContext _context;
        private readonly UserManager<UserInfor> _userManager;
        private readonly IMapper _mapper;
        private readonly HandleOTP _handleOTP;

        public OTPController(TnR_SSContext context, UserManager<UserInfor> userManager, SignInManager<UserInfor> signInManager, RoleManager<RoleUser> roleManager, IMapper mapper, HandleOTP handleOTP)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _handleOTP = handleOTP;
        }

        #region Register OTP
        [HttpGet("register/{phoneNumber}")]
        [AllowAnonymous]
        public async Task<ResponseModel> SendRegisterUserOTP(string phoneNumber)
        {
            if (UserPhoneNumberExists(phoneNumber))
            {
                return new ResponseBuilder().Error("Phone Number existed").ResponseModel;
            }

            //send OTP
            var otpId = await _handleOTP.SendOTPByStringeeAsync(phoneNumber);
            if (otpId == 0)
            {
                return new ResponseBuilder<Object>().Error("Send OTP error").ResponseModel;
            }

            return new ResponseBuilder<Object>().Success("Success").WithData(new { OTPID = otpId }).ResponseModel;
        }

        [HttpPost("check-register")]
        [AllowAnonymous]
        public async Task<ResponseModel> CheckRegisterUserOTP(OTPReqModel modelData)
        {
            if (await _handleOTP.CheckOTPRightAsync(modelData.OTPID, modelData.Code, modelData.PhoneNumber))
            {
                return new ResponseBuilder().Success("OTP Success").ResponseModel;
            }

            return new ResponseBuilder().Error("Invalid OTP").ResponseModel;
        }
        #endregion

        #region Reset Password OTP
        [HttpGet("reset-password/{phoneNumber}")]
        [AllowAnonymous]
        public async Task<ResponseModel> GeneratePasswordResetToken(string phoneNumber)
        {
            var userInfor = _userManager.Users.FirstOrDefault(x => x.PhoneNumber.Equals(phoneNumber));
            if (userInfor is null)
            {
                return new ResponseBuilder().WithCode(HttpStatusCode.NotFound).WithMessage("Phone Number haven't registered yet").ResponseModel;
            }

            //generate OTP
            var otpId = await _handleOTP.SendOTPByStringeeAsync(phoneNumber);
            if (otpId == 0)
            {
                return new ResponseBuilder().Error("Send OTP error").ResponseModel;
            }

            //generate reset token
            var token = _userManager.GeneratePasswordResetTokenAsync(userInfor).Result;

            return new ResponseBuilder<Object>().Success("Success").WithData(new { resetToken = token, otpid = otpId }).ResponseModel;
        }
        #endregion

        #region Change Phone Number
        [HttpPost("change-phone/{id}")]
        public async Task<ResponseModel> SendChangePhoneNumberOTP(int id, ChangePhoneReqModel dataModel)
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
            var otpId = await _handleOTP.SendOTPByStringeeAsync(dataModel.NewPhoneNumber);
            if (otpId == 0)
            {
                return new ResponseBuilder<Object>().Error("Send OTP error").ResponseModel;
            }

            return new ResponseBuilder<Object>().Success("Success").WithData(new { OTPID = otpId }).ResponseModel;
        }
        #endregion

        #region Common
        private bool UserPhoneNumberExists(string phoneNumber)
        {
            var rs = _userManager.Users.SingleOrDefault(u => u.PhoneNumber == phoneNumber);
            return rs is not null;
        }
        #endregion
    }
}
