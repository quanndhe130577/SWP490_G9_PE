using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TnR_SS.Domain.Entities;

namespace TnR_SS.DataEFCore.Configurations
{
    public class TransactionBuyConfiguration
    {
        public TransactionBuyConfiguration(EntityTypeBuilder<TransactionBuy> entity)
        {
            entity.Property(e => e.FishTypeID)
                .IsRequired();
            entity.HasOne(p => p.FishType)
               .WithMany(b => b.TransactionBuys)
               .HasForeignKey(p => p.FishTypeID)
               .HasConstraintName("FK_TransactionBuy_FishType");

            entity.Property(e => e.RoId)
                .IsRequired();
            entity.HasOne(p => p.Ro)
                .WithMany(b => b.TransactionBuys)
                .HasForeignKey(p => p.RoId)
                .HasConstraintName("FK_TransactionBuy_Ro");

            entity.Property(e => e.TongKetMuaId)
                .IsRequired();
            entity.HasOne(p => p.TongKetMua)
                .WithMany(b => b.TransactionBuys)
                .HasForeignKey(p => p.TongKetMuaId)
                .HasConstraintName("FK_TransactionBuy_TongKetMua");

            entity.Property(e => e.ID)
                .IsRequired();

            entity.Property(e => e.BuyPrice)
                .IsRequired();

        }
    }
}
