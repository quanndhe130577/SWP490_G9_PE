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

            entity.Property(e => e.TransactionPrice)
                .IsRequired();

            entity.Property(e => e.PurchasePrice)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime");

            entity.Property(e => e.UpdatedAt)
                .IsRequired()
                .HasColumnType("datetime");

            entity.HasOne(p => p.FishType)
                .WithMany(b => b.FishTypePrices)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey(x => x.FishTypeId)
                .HasConstraintName("FK_FishTypePrice_FishType");

        }
    }
}
