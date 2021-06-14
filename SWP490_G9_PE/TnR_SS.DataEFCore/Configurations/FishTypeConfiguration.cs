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
    public class FishTypeConfiguration
    {
        public FishTypeConfiguration(EntityTypeBuilder<FishType> entity)
        {

            entity.ToTable("FishType");

            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.FishName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.MinWeight)
                .HasMaxLength(12)
                .IsRequired();

            entity.Property(e => e.MaxWeight)
                .HasMaxLength(12)
                .IsRequired();


            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .IsRequired();

            entity.HasOne(p => p.Trader)
               .WithMany(b => b.FishTypes)
               .HasForeignKey(p => p.TraderID)
               .OnDelete(DeleteBehavior.ClientNoAction)
               .HasConstraintName("FK_FishType_UserInfor");
        }
    }
}
