using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TnR_SS.API.Common.Response;
using TnR_SS.API.UserInfors.Model;
using TnR_SS.Entity.Models;

namespace TnR_SS.API.UserInfors.Controller
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserInforController : ControllerBase
    {
        private readonly TnR_SSContext _context;

        public UserInforController(TnR_SSContext context)
        {
            _context = context;
        }

        // GET: api/UserInfor
        [HttpGet]
        public async Task<ResponseModel> GetUserInfors()
        {
            var listUser = await _context.UserInfors.ToListAsync();
            var rs = listUser.Select(x => UserModel.changeToModel(x)).ToList();
            ResponseBuilder<List<UserModel>> rpB = new ResponseBuilder<List<UserModel>>().WithData(rs);
            return rpB.ResponseModel;
        }

        // GET: api/UserInfor/5
        [HttpGet("{id}")]
        public async Task<ResponseModel> GetUserInfor(int id)
        {
            var userInfor = await _context.UserInfors.FindAsync(id);
            ResponseBuilder rpB;
            if (userInfor == null)
            {
                rpB = new ResponseBuilder().WithCode(HttpStatusCode.NotFound);
            }
            else
            {
                rpB = new ResponseBuilder<UserModel>().WithData(UserModel.changeToModel(userInfor));
            }

            return rpB.ResponseModel;
        }

        // PUT: api/UserInfor/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ResponseModel> PutUserInfor(int id, UserInfor userInfor)
        {
            if (id != userInfor.Id)
            {
                return new ResponseBuilder().WithCode(HttpStatusCode.BadRequest).ResponseModel;
            }

            _context.Entry(userInfor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInforExists(id))
                {
                    return new ResponseBuilder().WithCode(HttpStatusCode.NotFound).ResponseModel;
                }
                else
                {
                    throw;
                }
            }

            return new ResponseBuilder().Success("Update Success").ResponseModel;

        }

        // POST: api/UserInfor
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ResponseModel> PostUserInfor(InsertUserModel userData)
        {
            if (!UserPhoneNumberExists(userData.PhoneNumber))
            {
                var userInfor = userData.changeToUserInfor();
                _context.UserInfors.Add(userInfor);
                await _context.SaveChangesAsync();

                return new ResponseBuilder().Success("Insert Success").ResponseModel;
            }
            else
            {
                return new ResponseBuilder().Error("Phone Number Existed").ResponseModel;
            }

        }

        // DELETE: api/UserInfor/5
        [HttpDelete("{id}")]
        public async Task<ResponseModel> DeleteUserInfor(int id)
        {
            var userInfor = await _context.UserInfors.FindAsync(id);
            if (userInfor == null)
            {
                return new ResponseBuilder().WithCode(HttpStatusCode.NotFound).ResponseModel;
            }

            _context.UserInfors.Remove(userInfor);
            await _context.SaveChangesAsync();

            return new ResponseBuilder().Success("Delete Success").ResponseModel;
        }

        private bool UserInforExists(int id)
        {
            return _context.UserInfors.Any(e => e.Id == id);
        }
        private bool UserPhoneNumberExists(string phoneNumber)
        {
            return _context.UserInfors.Any(e => e.PhoneNumber.Equals(phoneNumber));
        }
    }
}
