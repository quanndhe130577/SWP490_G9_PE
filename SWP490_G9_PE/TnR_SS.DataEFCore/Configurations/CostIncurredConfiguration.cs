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
    public class CostIncurredConfiguration
    {
        public CostIncurredConfiguration(EntityTypeBuilder<CostIncurred> entity)
        {
            entity.ToTable("CostIncurred");

            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.Cost)
                .IsRequired();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Note)
                .HasMaxLength(50);

            entity.Property(e => e.CreatedAt)
               .HasColumnType("datetime");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime");

            entity.Property(e => e.UserId)
                .IsRequired();
            entity.HasOne(p => p.User)
                .WithMany(b => b.CostIncurreds)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_CostIncurred_UserInfor");
        }
    }
}
