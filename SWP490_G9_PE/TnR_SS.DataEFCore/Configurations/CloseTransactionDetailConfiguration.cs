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
    public class CloseTransactionDetailConfiguration
    {
        public CloseTransactionDetailConfiguration(EntityTypeBuilder<CloseTransactionDetail> entity)
        {

            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.SellPrice)
                .IsRequired();

            entity.Property(e => e.Weight)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired()
                .HasColumnType("datetime");

            entity.Property(e => e.CreatedAt)
               .HasColumnType("datetime");


            entity.HasOne(p => p.TransactionDetail)
                .WithOne(b => b.CloseTransactionDetail)
                .HasForeignKey<CloseTransactionDetail>(p => p.TransactionDetailId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_CloseTransactionDetail_TransactionDetail");
        }
    }
}
