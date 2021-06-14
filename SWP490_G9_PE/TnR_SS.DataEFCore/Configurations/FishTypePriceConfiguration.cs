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
    public class FishTypePriceConfiguration
    {
        public FishTypePriceConfiguration(EntityTypeBuilder<FishTypePrice> entity)
        {

            entity.ToTable("FishTypePrice");

            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .IsRequired();

            entity.Property(e => e.Price)
                .IsRequired();

            entity.HasOne(p => p.FishType)
               .WithMany(b => b.FishTypePrices)
               .HasForeignKey(p => p.FishTypeID)
               .OnDelete(DeleteBehavior.ClientNoAction)
               .HasConstraintName("FK_FishTypePrice_FishType");
        
        }
    }
}
