using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using TnR_SS.API.Common.StringeeAPI;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Domain.ApiModels.OTPModel.RequestModel;
using TnR_SS.Domain.Supervisor;
using TnR_SS.API.Common.TwilioAPI;

namespace TnR_SS.API.Controller
{
    [Route("api/OTP")]
    [ApiController]
    public class OTPController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITnR_SSSupervisor _tnrssSupervisor;

        public OTPController(ITnR_SSSupervisor tnrssSupervisor, IMapper mapper)
        {
            _tnrssSupervisor = tnrssSupervisor;
            _mapper = mapper;
        }

        #region Register OTP
        [HttpGet("register/{phoneNumber}")]
        [AllowAnonymous]
        public async Task<ResponseModel> SendRegisterUserOTP(string phoneNumber)
        {
            if (_tnrssSupervisor.CheckUserPhoneExists(phoneNumber))
            {
                return new ResponseBuilder().Error("Số điện thoại đã tồn tại").ResponseModel;
            }

            var checkOTPExsits = _tnrssSupervisor.CheckPhoneOTPExists(phoneNumber);
            if (checkOTPExsits)
            {
                return new ResponseBuilder().Error("Mã OTP không đúng").ResponseModel;
            }

            //var otpId = await _tnrssSupervisor.SendOTPByStringee(token, phoneNumber);
            /*var otpCode = TwilioAPI.SendOtpRequest(phoneNumber);
            if (otpCode is null)
            {
                return new ResponseBuilder().Error("Nếu không nhận được OTP, hãy nhận lại sau 60s").ResponseModel;
            }*/
            var otpCode = "123456";
            var otpId = await _tnrssSupervisor.AddOTPAsync(otpCode, phoneNumber);

            return new ResponseBuilder<Object>().Success("Success").WithData(new { OTPID = otpId }).ResponseModel;
        }

        [HttpPost("check-register")]
        [AllowAnonymous]
        public async Task<ResponseModel> CheckRegisterUserOTP(OTPReqModel modelData)
        {
            if (await _tnrssSupervisor.CheckOTPRightAsync(modelData.OTPID, modelData.Code, modelData.PhoneNumber))
            {
                return new ResponseBuilder().Success("OTP thành công").ResponseModel;
            }

            //return new ResponseBuilder().Success("OTP Thành công").ResponseModel;
            return new ResponseBuilder().Error("Invalid OTP").ResponseModel;
        }
        #endregion

        #region Reset Password OTP
        [HttpGet("reset-password/{phoneNumber}")]
        [AllowAnonymous]
        public async Task<ResponseModel> GeneratePasswordResetToken(string phoneNumber)
        {
            var userInfor = _tnrssSupervisor.GetUserByPhoneNumber(phoneNumber);
            if (userInfor is null)
            {
                return new ResponseBuilder().WithCode(HttpStatusCode.NotFound).WithMessage("Phone Number haven't registered yet").ResponseModel;
            }

            var checkOTPExsits = _tnrssSupervisor.CheckPhoneOTPExists(phoneNumber);
            if (checkOTPExsits)
            {
                return new ResponseBuilder().Error("Wait a minute then resend OTP").ResponseModel;
            }

            //var otpCode = await StringeeAPI.SendOtpRequestAsync(phoneNumber);
            /*var otpCode = TwilioAPI.SendOtpRequest(phoneNumber);
            if (otpCode is null)
            {
                return new ResponseBuilder().Error("Wait a minute then resend OTP").ResponseModel;
            }*/

            var otpCode = "123456";

            var optId = await _tnrssSupervisor.AddOTPAsync(otpCode, phoneNumber);

            //generate reset token
            var token_reset = await _tnrssSupervisor.GetPasswordResetTokenAsync(userInfor);

            return new ResponseBuilder<Object>().Success("Success").WithData(new { resetToken = token_reset, otpid = optId }).ResponseModel;
        }
        #endregion

        #region Change Phone Number
        [HttpPost("change-phone/{id}")]
        public async Task<ResponseModel> SendChangePhoneNumberOTP(int id, ChangePhoneReqModel dataModel)
        {
            if (!TokenManagement.CheckUserIdFromToken(HttpContext, id))
            {
                return new ResponseBuilder().Error("Tài khoản không hợp lệ").ResponseModel;
            }

            var user = await _tnrssSupervisor.GetUserByIdAsync(id);
            if (!await _tnrssSupervisor.CheckUserPassword(user.UserID, dataModel.CurrentPassword))
            {
                return new ResponseBuilder().Error("Số điện thoại hoặc mật khẩu sai").ResponseModel;
            }

            if (_tnrssSupervisor.CheckUserPhoneExists(dataModel.NewPhoneNumber))
            {
                return new ResponseBuilder().Error("Số điện thoại đã tồn tại").ResponseModel;
            }

            var checkOTPExsits = _tnrssSupervisor.CheckPhoneOTPExists(dataModel.NewPhoneNumber);
            if (checkOTPExsits)
            {
                return new ResponseBuilder().Error("Nếu không nhận được OTP, hãy gửi lại sau 60s").ResponseModel;
            }

            //var otpCode = await StringeeAPI.SendOtpRequestAsync(dataModel.NewPhoneNumber);
            var otpCode = TwilioAPI.SendOtpRequest(dataModel.NewPhoneNumber);
            if (otpCode is null)
            {
                return new ResponseBuilder().Error("Nếu không nhận được OTP, hãy gửi lại sau 60s").ResponseModel;
            }

            var otpId = await _tnrssSupervisor.AddOTPAsync(otpCode, dataModel.NewPhoneNumber);

            return new ResponseBuilder<Object>().Success("Thành công").WithData(new { OTPID = otpId }).ResponseModel;
        }
        #endregion
    }
}
