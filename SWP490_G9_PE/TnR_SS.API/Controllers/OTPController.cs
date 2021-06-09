using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.API.Common.HandleOTP;
using TnR_SS.API.Common.HandleOTP.Model;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.API.Model.OTPModel.RequestModel;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.Supervisor;

namespace TnR_SS.API.Controller
{
    [Route("api/OTP")]
    [ApiController]
    public class OTPController : ControllerBase
    {
        /*private readonly TnR_SSContext _context;
        private readonly UserManager<UserInfor> _userManager;
        private readonly IMapper _mapper;
        private readonly HandleOTP _handleOTP;

        public OTPController(TnR_SSContext context, UserManager<UserInfor> userManager, SignInManager<UserInfor> signInManager, RoleManager<RoleUser> roleManager, IMapper mapper, HandleOTP handleOTP)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _handleOTP = handleOTP;
        }*/

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
                return new ResponseBuilder().Error("Phone Number existed").ResponseModel;
            }

            var otpId = await SendOTPByStringee(phoneNumber);
            if (otpId == 0)
            {
                return new ResponseBuilder().Error("Wait a minute then resend OTP").ResponseModel;
            }

            return new ResponseBuilder<Object>().Success("Success").WithData(new { OTPID = otpId }).ResponseModel;
        }

        [HttpPost("check-register")]
        [AllowAnonymous]
        public async Task<ResponseModel> CheckRegisterUserOTP(OTPReqModel modelData)
        {
            if (await _tnrssSupervisor.CheckOTPRightAsync(modelData.OTPID, modelData.Code, modelData.PhoneNumber))
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
            var userInfor = _tnrssSupervisor.GetUserByPhoneNumber(phoneNumber);
            if (userInfor is null)
            {
                return new ResponseBuilder().WithCode(HttpStatusCode.NotFound).WithMessage("Phone Number haven't registered yet").ResponseModel;
            }

            var otpId = await SendOTPByStringee(phoneNumber);
            if (otpId == 0)
            {
                return new ResponseBuilder().Error("Wait a minute then resend OTP").ResponseModel;
            }

            //generate reset token
            var token = await _tnrssSupervisor.GetPasswordResetTokenAsync(userInfor);

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

            var user = _tnrssSupervisor.GetUserById(id);
            if (!await _tnrssSupervisor.CheckUserPassword(user, dataModel.CurrentPassword))
            {
                return new ResponseBuilder().Error("Invalid password").ResponseModel;
            }

            if (_tnrssSupervisor.CheckUserPhoneExists(dataModel.NewPhoneNumber))
            {
                return new ResponseBuilder().Error("Phone Number existed").ResponseModel;
            }

            var otpId = await SendOTPByStringee(dataModel.NewPhoneNumber);
            if (otpId == 0)
            {
                return new ResponseBuilder().Error("Wait a minute then resend OTP").ResponseModel;
            }

            return new ResponseBuilder<Object>().Success("Success").WithData(new { OTPID = otpId }).ResponseModel;
        }
        #endregion

        private async Task<int> SendOTPByStringee(string phoneNumber)
        {
            string token = HandleOTP.GetStringeeToken();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-STRINGEE-AUTH", token);

            Random rd = new Random();
            string OTP_string = rd.Next(1, 999999).ToString("D6");

            var checkOTPExsits = _tnrssSupervisor.CheckPhoneOTPExists(phoneNumber);
            if (checkOTPExsits)
            {
                //return new ResponseBuilder().Error("Wait a minute to resend OTP").ResponseModel;
                return 0;
            }

            StringeeReqModel smsModel = new StringeeReqModel();
            SMSContentReqModel sms = new SMSContentReqModel()
            {
                From = "TnR",
                To = HandleOTP.ModifyPhoneNumber(phoneNumber),
                Text = "Your OTP is " + OTP_string
            };
            smsModel.SMS = sms;

            var response = await client.PostAsync("https://api.stringee.com/v1/sms", new StringContent(JsonConvert.SerializeObject(smsModel), Encoding.UTF8, "application/json"));
            var rs = await response.Content.ReadAsStringAsync();
            StringeeResModel stringeeResModel = JsonConvert.DeserializeObject<StringeeResModel>(rs);
            if (stringeeResModel.SMSSent == "0")
            {
                //return new ResponseBuilder<Object>().Error("Send OTP error").ResponseModel;
                //return 0;
            }

            //create OTP
            OTP otp = new OTP()
            {
                Code = OTP_string,
                PhoneNumber = phoneNumber,
                ExpiredDate = DateTime.Now.AddMinutes(1),
                Status = OTPStatus.Waiting.ToString()
            };

            await _tnrssSupervisor.AddOTPAsync(otp);

            return otp.ID;
        }
    }
}
