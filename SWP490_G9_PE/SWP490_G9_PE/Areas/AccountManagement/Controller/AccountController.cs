using AutoMapper;
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
using TnR_SS.API.Common.HandleSHA256;
using TnR_SS.API.Common.Response;
using TnR_SS.API.Common.Token;
using TnR_SS.Areas.API.AccountManagement.Model;
using TnR_SS.Entity.Models;

namespace TnR_SS.API.Areas.AccountManagement.Controller
{
    [Route("api/test")]
    [ApiController]
    [Authorize]
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
        public async Task<ResponseModel> Register(UserModel userData)
        {
            if (ModelState.IsValid)
            {
                if (UserPhoneNumberExists(userData.PhoneNumber))
                {
                    return new ResponseBuilder().Error("Phone Number existed").ResponseModel;
                }

                var userInfor = _mapper.Map<UserModel, UserInfor>(userData);
                var result = await _userManager.CreateAsync(userInfor, userInfor.Password);

                if (result.Succeeded)
                {
                    /*var token = TokenGeneration.GetTokenUser(userInfor.Id.ToString("######"));
                    ResponseLoginModel rlm = new ResponseLoginModel()
                    {
                        Token = token,
                        UserID = userInfor.Id.ToString("######")
                    };
                    ResponseBuilder<ResponseLoginModel> rpB = new ResponseBuilder<ResponseLoginModel>().WithData(rlm);
                    return rpB.ResponseModel;*/
                    return new ResponseBuilder().Success("Create Success").ResponseModel;
                }

                List<String> listError = new List<string>();

                foreach (var error in result.Errors)
                {
                    listError.Add(error.Description);
                }
                //ResponseModel<Object> rpB1 = new ResponseModel<Object>(HttpStatusCode.BadRequest, false, "Invalid information", "Login", new { error = listError });
                ResponseBuilder<Object> rpB1 = new ResponseBuilder<Object>().WithCode(HttpStatusCode.BadRequest).WithData(listError);
                return rpB1.ResponseModel;

            }

            return new ResponseBuilder().Error("Invalid information").ResponseModel;
        }
        #endregion
        #region Login
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ResponseModel> Login(LoginModel userData)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.Users.SingleOrDefault(u => u.PhoneNumber == userData.PhoneNumber);
                if (user is null)
                {
                    return new ResponseBuilder().Error("User not found").ResponseModel;
                }

                var userSigninResult = await _signInManager.PasswordSignInAsync(user, EncryptHandle.EncryptString(userData.Password + user.SaltPassword), true, false);
                //var userSigninResult = await _userManager.CheckPasswordAsync(user, HandleSHA256.EncryptString(userData.Password + user.SaltPassword));

                if (userSigninResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    var token = TokenGeneration.GetTokenUser(user.Id.ToString("######"));
                    ResponseLoginModel rlm = new ResponseLoginModel()
                    {
                        Token = token,
                        UserID = user.Id.ToString("######")
                    };
                    ResponseBuilder<ResponseLoginModel> rpB = new ResponseBuilder<ResponseLoginModel>().WithData(rlm);
                    return rpB.ResponseModel;
                }

                List<String> listError = new List<String>();

                return new ResponseBuilder().Error("Invalid Phone number or password").ResponseModel;
            }

            return new ResponseBuilder().Error("Login failed").ResponseModel;
        }
        #endregion

        [HttpPut("update/{id}")]
        //[Route("update")]
        public async Task<ResponseModel> UpdateUserInfor(int id, UserModel userData)
        {
            //var userInfor = await _context.UserInfors.FindAsync(id);
            var userInfor = _userManager.Users.FirstOrDefault(x => x.Id == id);
            if (userInfor == null)
            {
                return new ResponseBuilder().WithCode(HttpStatusCode.NotFound).WithMessage("User have not existed").ResponseModel;
            }

            userInfor = _mapper.Map<UserModel, UserInfor>(userData, userInfor);
            await _userManager.UpdateAsync(userInfor);
            //_context.Entry(userInfor).State = EntityState.Modified;

            try
            {
                await _userManager.UpdateAsync(userInfor);
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                return new ResponseBuilder().WithCode(HttpStatusCode.Conflict).WithMessage("").ResponseModel;

            }

            return new ResponseBuilder().Success("Update Success").ResponseModel;

        }

        [HttpPut("change-password/{id}")]
        //[Route("change-password")]
        public async Task<ResponseModel> ChangePassword(int id, [FromBody] ChangePasswordModel changePasswordModel)
        {
            if (changePasswordModel.Password != changePasswordModel.ConfirmPassword)
            {
                return new ResponseBuilder().Error("Invalid confirm password").ResponseModel;
            }

            var userInfor = _userManager.Users.FirstOrDefault(x => x.Id == id);
            if (userInfor is null)
            {
                return new ResponseBuilder().Error("Invalid information").ResponseModel;
            }

            userInfor = _mapper.Map<ChangePasswordModel, UserInfor>(changePasswordModel, userInfor);
 
                await _userManager.UpdateAsync(userInfor);
  

            return new ResponseBuilder().Success("Update Success").ResponseModel;

        }

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
