﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;
using TnR_SS.Domain.IRepositories;

namespace TnR_SS.DataEFCore.Repositories
{
    public class RoleUserRepository : IRoleUserRepository
    {
        private readonly TnR_SSContext _context;
        private readonly RoleManager<RoleUser> _roleManager;

        public RoleUserRepository(TnR_SSContext context, RoleManager<RoleUser> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public List<RoleUser> AllRoleUser()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<IdentityResult> CreateAsync(RoleUser role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public void Dispose() => _context.Dispose();

        public async Task<RoleUser> FindByNameAsync(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}