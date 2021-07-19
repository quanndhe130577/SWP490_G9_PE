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
    public class TransactionDetailConfiguration
    {
        public TransactionDetailConfiguration(EntityTypeBuilder<TransactionDetail> entity)
        {

            entity.ToTable("TransactionDetail");

            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.IsPaid)
                .IsRequired();

            entity.Property(e => e.SellPrice)
                .IsRequired();

            entity.Property(e => e.Weight)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime");

            entity.Property(e => e.UpdatedAt)
                .IsRequired()
                .HasColumnType("datetime");

            entity.HasOne(p => p.FishType)
                .WithMany(b => b.TransactionDetails)
                .HasForeignKey(x => x.FishTypeId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_TransactionDetail_FishType");

            entity.HasOne(p => p.Transaction)
                .WithMany(b => b.TransactionDetails)
                .HasForeignKey(x => x.TransId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_TransactionDetail_Transaction");

            entity.Property(e => e.BuyerId)
                .IsRequired(false);

            entity.HasOne(p => p.Buyer)
                .WithMany(b => b.TransactionDetails)
                .HasForeignKey(x => x.BuyerId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_TransactionDetail_Buyer");

        }
    }
}
