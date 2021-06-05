using AutoMapper;
using TnR_SS.API.Areas.RoleManagement.Model.ResponseModel;
using TnR_SS.Entity.Models;

namespace TnR_SS.API.Areas.RoleManagement.Common
{
    public class RoleMapperProfile : Profile
    {
        public RoleMapperProfile()
        {
            CreateMap<RoleUser, RoleResModel>();
        }
    }
}
