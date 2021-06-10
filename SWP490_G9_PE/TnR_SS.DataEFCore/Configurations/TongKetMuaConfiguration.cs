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
    public class TongKetMuaConfiguration
    {
        public TongKetMuaConfiguration(EntityTypeBuilder<TongKetMua> entity)
        {
            entity.Property(e => e.TraderID)
                .IsRequired();
            entity.HasOne(p => p.UserInfor)
               .WithMany(b => b.TongKetMuas)
               .HasForeignKey(p => p.TraderID)
               .HasConstraintName("FK_TongKetMua_UserInfor");

            entity.Property(e => e.PondOwnerID)
                .IsRequired();
            entity.HasOne(p => p.PondOwner)
                .WithMany(b => b.TongKetMuas)
                .HasForeignKey(p => p.PondOwnerID)
                .HasConstraintName("FK_TongKetMua_PondOwner");

            entity.Property(e => e.TotalAmount)
                .IsRequired();

            entity.Property(e => e.TotalWeight)
                .IsRequired();

            entity.Property(e => e.PayForPondOwner)
                .IsRequired();

            entity.Property(e => e.Date)
                .IsRequired();

            entity.Property(e => e.TotalAmount)
                .IsRequired();

        }
    }
}
