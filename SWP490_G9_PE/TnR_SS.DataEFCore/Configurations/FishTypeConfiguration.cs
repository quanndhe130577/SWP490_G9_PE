﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TnR_SS.Domain.Entities;

namespace TnR_SS.DataEFCore.Configurations
{
    public class FishTypeConfiguration
    {
        public FishTypeConfiguration(EntityTypeBuilder<FishType> entity)
        {

            entity.ToTable("FishType");

            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.FishName)
                .IsRequired()
                .IsFixedLength(true);

            entity.Property(e => e.Description)
                .IsRequired()
                .IsFixedLength(true);

            entity.Property(e => e.MinWeight)
                .HasMaxLength(12)
                .IsRequired();

            entity.Property(e => e.MaxWeight)
                .HasMaxLength(12)
                .IsRequired();

            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .IsRequired();

        }
    }
}