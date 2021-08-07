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
    public class PurchaseConfiguration
    {
        public PurchaseConfiguration(EntityTypeBuilder<Purchase> entity)
        {
            entity.ToTable("Purchase");

            entity.Property(e => e.Date)
                .IsRequired()
                .HasColumnType("datetime");

            entity.Property(e => e.TraderID)
                .IsRequired();
            entity.HasOne(p => p.UserInfor)
               .WithMany(b => b.Purchases)
               .HasForeignKey(p => p.TraderID)
               .OnDelete(DeleteBehavior.ClientNoAction)
               .HasConstraintName("FK_Purchase_UserInfor");

            entity.Property(e => e.PondOwnerID)
                .IsRequired();
            entity.HasOne(p => p.PondOwner)
                .WithMany(b => b.Purchases)
                .HasForeignKey(p => p.PondOwnerID)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_Purchase_PondOwner");

            /*entity.Property(e => e.TotalAmount)
                .IsRequired();*/

            /*entity.Property(e => e.TotalWeight)
                .IsRequired();*/

            entity.Property(e => e.PayForPondOwner)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();

        }
    }
}
