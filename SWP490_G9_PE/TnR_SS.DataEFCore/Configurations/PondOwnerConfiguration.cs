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
    public class PondOwnerConfiguration
    {
        public PondOwnerConfiguration(EntityTypeBuilder<PondOwner> entity)
        {

            entity.ToTable("PondOwner");

            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.Name)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(12)
                .IsRequired()
                .IsUnicode(false);

            entity.Property(e => e.Address)
                .IsRequired();

        }
    }
}
