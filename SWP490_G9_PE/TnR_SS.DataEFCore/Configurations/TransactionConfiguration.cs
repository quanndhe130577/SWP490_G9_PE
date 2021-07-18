
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
    public class TransactionConfiguration
    {
        public TransactionConfiguration(EntityTypeBuilder<Transaction> entity)
        {

            entity.ToTable("Transaction");

            entity.Property(e => e.ID).HasColumnName("ID").IsRequired();

            entity.Property(e => e.CommissionUnit)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime");

            entity.Property(e => e.Date)
                .IsRequired()
                .HasColumnType("date");

            entity.Property(e => e.UpdatedAt)
                .IsRequired()
                .HasColumnType("datetime");

            entity.HasOne(p => p.Trader)
                .WithMany(b => b.TransactionTraders)
                .HasForeignKey(x => x.TraderId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_Transaction_UserInfor-Trader");

            entity.Property(e => e.WeightRecorderId)
               .IsRequired(false);

            entity.HasOne(p => p.WeightRecorder)
                .WithMany(b => b.TransactionWeightRecorders)
                .HasForeignKey(x => x.WeightRecorderId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_Transaction_UserInfor-WeightRecorder");

        }
    }
}
