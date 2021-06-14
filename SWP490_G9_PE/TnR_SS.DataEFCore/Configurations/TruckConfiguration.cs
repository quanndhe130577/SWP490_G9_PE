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
    public class TruckConfiguration
    {
        public TruckConfiguration(EntityTypeBuilder<Truck> entity)
        {

            entity.ToTable("Truck");

            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.LicensePlate)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime");

            entity.Property(e => e.UpdatedAt)
                .IsRequired()
                .HasColumnType("datetime");

            entity.Property(e => e.TraderID)
                .IsRequired();
            entity.HasOne(p => p.Trader)
                .WithMany(b => b.Trucks)
                .HasForeignKey(p => p.TraderID)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_Truck_UserInfor");
        }
    }
}
