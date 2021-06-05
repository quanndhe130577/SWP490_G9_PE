﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TnR_SS.API.Areas.RoleManagement.Model.ResponseModel;
using TnR_SS.API.Common.Response;
using TnR_SS.Entity.Models;

namespace TnR_SS.API.Areas.RoleManagement.Controller
{
    [Route("api/role")]
    [ApiController]
    public class RoleUsersController : ControllerBase
    {
        private readonly TnR_SSContext _context;
        private readonly RoleManager<RoleUser> _roleManager;
        private readonly IMapper _mapper;

        public RoleUsersController(TnR_SSContext context, RoleManager<RoleUser> roleManager, IMapper mapper)
        {
            _context = context;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        // GET: api/RoleUsers
        [HttpGet]
        [Route("get-all")]
        public ResponseModel GetRoleUsers()
        {
            //var roles = await _roleManager.Roles.ToListAsync();
            List<AllRoleResModel> roleRes = new List<AllRoleResModel>();
            foreach (var role in _roleManager.Roles)
            {
                roleRes.Add(_mapper.Map<RoleUser, AllRoleResModel>(role));
            }

            return new ResponseBuilder<List<AllRoleResModel>>().Success("Get list role success").WithData(roleRes).ResponseModel;
        }

    }
}
