using Microsoft.AspNetCore.Identity;
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
            return await _unitOfWork.RoleUsers.RoleExistsAsync(roleName);
        }

        private async Task<string> GetRoleDisplayNameAsync(UserInfor user)
        {
            var userRoles = await _unitOfWork.UserInfors.GetRolesAsync(user);
            if (userRoles.Count == 0 || userRoles == null)
            {
                throw new Exception("Tài khoản bị lỗi");
            }

            var rs = await _unitOfWork.RoleUsers.FindByNameAsync(userRoles[0]);
            if (rs == null)
            {
                throw new Exception("Tài khoản không có quyền !!!");
            }

            return rs.DisplayName;
        }

        private async Task<string> GetRoleNameAsync(UserInfor user)
        {
            var userRoles = await _unitOfWork.UserInfors.GetRolesAsync(user);
            if (userRoles.Count == 0 || userRoles == null)
            {
                throw new Exception("Tài khoản bị lỗi");
            }

            var rs = await _unitOfWork.RoleUsers.FindByNameAsync(userRoles[0]);
            if (rs == null)
            {
                throw new Exception("Tài khoản không có quyền !!!");
            }
            return rs.Name;
        }

        public List<AllRoleResModel> GetAllResRoles()
        {
            var userRoles = _unitOfWork.RoleUsers.AllRoleUser();
            List<AllRoleResModel> roleRes = new List<AllRoleResModel>();
            foreach (var role in userRoles)
            {
                roleRes.Add(_mapper.Map<RoleUser, AllRoleResModel>(role));
            }

            return roleRes;
        }
        public async Task<IdentityResult> AddRoleUserAsync(RoleUser role)
        {
            return await _unitOfWork.RoleUsers.CreateIdentityAsync(role);
        }

    }
}
