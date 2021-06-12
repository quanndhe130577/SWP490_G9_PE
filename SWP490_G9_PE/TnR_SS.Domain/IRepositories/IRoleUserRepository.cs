using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.Domain.IRepositories
{
    public interface IRoleUserRepository : IRepositoryBase<RoleUser>
    {
        Task<bool> RoleExistsAsync(string roleName);
        Task<RoleUser> FindByNameAsync(string roleName);
        List<RoleUser> AllRoleUser();
        Task<IdentityResult> CreateIdentityAsync(RoleUser role);
    }
}
