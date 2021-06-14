using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TnR_SS.Domain.Entities;

namespace TnR_SS.DataEFCore.Configurations
{
    public class PurchaseDetailConfiguration
    {
        public PurchaseDetailConfiguration(EntityTypeBuilder<PurchaseDetail> entity)
        {
            entity.ToTable("PurchaseDetail");

            entity.Property(e => e.FishTypeID)
                .IsRequired();
            entity.HasOne(p => p.FishType)
               .WithMany(b => b.PurchaseDetails)
               .HasForeignKey(p => p.FishTypeID)
               .HasConstraintName("FK_PurchaseDetail_FishType");

            entity.Property(e => e.BasketId)
                .IsRequired();
            entity.HasOne(p => p.Basket)
                .WithMany(b => b.PurchaseDetails)
                .HasForeignKey(p => p.BasketId)
                .HasConstraintName("FK_PurchaseDetail_Basket");

            entity.Property(e => e.TongKetMuaId)
                .IsRequired();
            entity.HasOne(p => p.TongKetMua)
                .WithMany(b => b.PurchaseDetails)
                .HasForeignKey(p => p.TongKetMuaId)
                .HasConstraintName("FK_PurchaseDetail_TongKetMua");

            entity.Property(e => e.ID)
                .IsRequired();

            entity.Property(e => e.BuyPrice)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();
        }
    }
}
