using System.ComponentModel.DataAnnotations;

namespace TnR_SS.Domain.ApiModels.RoleUserModel.ResponseModel
{
    public class CreateRoleReqModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string DisplayName { get; set; }
    }
}
