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
    public class BuyerConfiguration
    {
        public BuyerConfiguration(EntityTypeBuilder<Buyer> entity)
        {

            entity.ToTable("Buyer");

            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.Name)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime");

            entity.Property(e => e.UpdatedAt)
                .IsRequired()
                .HasColumnType("datetime");

            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(11);

            entity.HasOne(p => p.Seller)
                .WithMany(b => b.Buyers)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_Buyer_UserInfor");
        }
    }
}
