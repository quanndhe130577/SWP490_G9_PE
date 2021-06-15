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

            entity.Property(e => e.FishTypePriceID)
                .IsRequired();
            entity.HasOne(p => p.FishTypePrice)
               .WithMany(b => b.PurchaseDetails)
               .HasForeignKey(p => p.FishTypePriceID)
               .OnDelete(DeleteBehavior.ClientNoAction)
               .HasConstraintName("FK_PurchaseDetail_FishTypePrice");

            entity.Property(e => e.BasketId)
                .IsRequired();
            entity.HasOne(p => p.Basket)
                .WithMany(b => b.PurchaseDetails)
                .HasForeignKey(p => p.BasketId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_PurchaseDetail_Basket");

            entity.Property(e => e.PurchaseId)
                .IsRequired();
            entity.HasOne(p => p.Purchase)
                .WithMany(b => b.PurchaseDetails)
                .HasForeignKey(p => p.PurchaseId)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasConstraintName("FK_PurchaseDetail_Purchase");

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
