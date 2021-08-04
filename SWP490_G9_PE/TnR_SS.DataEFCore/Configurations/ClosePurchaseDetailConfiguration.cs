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
    public class ClosePurchaseDetailConfiguration
    {
        public ClosePurchaseDetailConfiguration(EntityTypeBuilder<ClosePurchaseDetail> entity)
        {

            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.Price)
                .IsRequired();

            entity.Property(e => e.Weight)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired()
                .HasColumnType("datetime");

            entity.Property(e => e.CreatedAt)
               .HasColumnType("datetime");


            entity.HasOne(p => p.PurchaseDetail)
                .WithOne(b => b.ClosePurchaseDetail)
                .HasForeignKey<ClosePurchaseDetail>(p => p.PurchaseDetailId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_ClosePurchaseDetail_PurchaseDetail");
        }
    }
}
