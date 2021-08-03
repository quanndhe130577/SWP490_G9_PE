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
    public class LK_PurchaseDeatil_DrumConfiguration
    {
        public LK_PurchaseDeatil_DrumConfiguration(EntityTypeBuilder<LK_PurchaseDeatil_Drum> entity)
        {

            entity.ToTable("LK_PurchaseDeatil_Drum");

            /*entity.Property(e => e.Weight)
                .IsRequired();*/

            entity.Property(e => e.CreatedAt)
                .IsRequired();

            entity.Property(e => e.UpdatedAt)
                .IsRequired();

            entity.HasOne(p => p.Drum)
               .WithMany(b => b.LK_PurchaseDeatil_Drums)
               .HasForeignKey(p => p.DrumID)
               .OnDelete(DeleteBehavior.Cascade)
               .HasConstraintName("FK_LKPurchaseDrum_Drum");

            entity.HasOne(p => p.PurchaseDetail)
               .WithMany(b => b.LK_PurchaseDeatil_Drums)
               .HasForeignKey(p => p.PurchaseDetailID)
               //.OnDelete(DeleteBehavior.ClientNoAction)
               .OnDelete(DeleteBehavior.Cascade)
               .HasConstraintName("FK_LKPurchaseDrum_PurchaseDetail");
        }
    }
}
