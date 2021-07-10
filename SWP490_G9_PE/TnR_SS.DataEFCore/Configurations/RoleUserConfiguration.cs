using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.DataEFCore.Configurations
{
    public class RoleUserConfiguration
    {
        public RoleUserConfiguration(EntityTypeBuilder<RoleUser> entity)
        {
            entity.ToTable("RoleUser");

            var listRole = new RoleUser[]
             {
                new RoleUser(RoleName.WeightRecorder, "Chủ bến"){ Id = 3 },
                new RoleUser(RoleName.Trader, "Thương lái"){ Id = 1 },
                new RoleUser(RoleName.Admin, "Admin"){ Id = 2 },
             };


            entity.HasData(listRole);

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();
        }
    }
}
