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
    public class BasketConfiguration
    {
        public BasketConfiguration(EntityTypeBuilder<Basket> entity)
        {

            entity.ToTable("Basket");

            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.Type)
                .IsRequired()
                .IsFixedLength(true);

            entity.Property(e => e.Weight)
                .IsRequired();

        }
    }
}
