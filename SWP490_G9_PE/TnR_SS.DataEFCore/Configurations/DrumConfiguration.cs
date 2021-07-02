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
    public class DrumConfiguration
    {
        public DrumConfiguration(EntityTypeBuilder<Drum> entity)
        {

            entity.ToTable("Drum");

            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.Type)
                .HasMaxLength(50);

            entity.Property(e => e.Number)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(e => e.MaxWeight)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime");

            entity.HasOne(p => p.Truck)
               .WithMany(b => b.Drums)
               .HasForeignKey(p => p.TruckID)
               .OnDelete(DeleteBehavior.ClientNoAction)
               .HasConstraintName("FK_Drum_Truck");
        }
    }
}
