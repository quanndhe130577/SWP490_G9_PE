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
    public class TraderOfWeightRecorderConfiguration
    {
        public TraderOfWeightRecorderConfiguration(EntityTypeBuilder<TraderOfWeightRecorder> entity)
        {

            entity.ToTable("TraderOfWeightRecorder");

            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();

            entity.HasOne(p => p.Trader)
               .WithMany(b => b.LK_Trader)
               .HasForeignKey(p => p.TraderId)
               .OnDelete(DeleteBehavior.ClientNoAction)
               .HasConstraintName("FK_TraderOfWeightRecorder_Trader");

            entity.HasOne(p => p.WeightRecorder)
               .WithMany(b => b.LK_WeightRecorder)
               .HasForeignKey(p => p.WeightRecorderId)
               .OnDelete(DeleteBehavior.ClientNoAction)
               .HasConstraintName("FK_TraderOfWeightRecorder_WeightRecorder");
        }
    }
}
