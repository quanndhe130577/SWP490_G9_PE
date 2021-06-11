﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.ApiModels.RoleUserModel.RequestModel;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.Supervisor
{
    public partial class TnR_SSSupervisor
    {
        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleUserRepository.RoleExistsAsync(roleName);
        }
        public async Task<string> GetRoleDisplayNameAsync(UserInfor user)
        {
            var userRoles = await _userInforRepository.GetRolesAsync(user);
            return _roleUserRepository.FindByNameAsync(userRoles[0]).Result.DisplayName;
        }
        public List<AllRoleResModel> GetAllResRoles()
        {
            var userRoles = _roleUserRepository.AllRoleUser();
            List<AllRoleResModel> roleRes = new List<AllRoleResModel>();
            foreach (var role in userRoles)
            {
                roleRes.Add(_mapper.Map<RoleUser, AllRoleResModel>(role));
            }

            return roleRes;
        }
        public async Task<IdentityResult> AddRoleUserAsync(RoleUser role)
        {
            return await _roleUserRepository.CreateAsync(role);
        }

    }
}