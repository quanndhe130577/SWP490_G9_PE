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
                new RoleUser(){ Id = 1, Name = RoleName.Admin, DisplayName = "Admin", NormalizedName = "ADMIN"},
                new RoleUser(){ Id = 2, Name = RoleName.Trader, DisplayName = "Thương lái", NormalizedName = "TRADER"},
                new RoleUser(){ Id = 3, Name = RoleName.WeightRecorder, DisplayName = "Chủ bến", NormalizedName = "WEIGHT RECORDER"},
             };


            entity.HasData(listRole);

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();
        }
    }
}
