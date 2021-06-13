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
    public class TimeKeepingConfiguration
    {
        public TimeKeepingConfiguration(EntityTypeBuilder<TimeKeeping> entity)
        {
            entity.ToTable("TimeKeeping");

            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.EmpId)
                .IsRequired();
            entity.HasOne(p => p.Employee)
                .WithMany(b => b.TimeKeepings)
                .HasForeignKey(p => p.EmpId)
                .HasConstraintName("FK_TimeKeeping_Employee");

            entity.Property(e => e.Money)
                .IsRequired();

            entity.Property(e => e.Note)
                .HasMaxLength(50);

            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.WorkDay)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime");

            entity.Property(e => e.UpdatedAt)
                .IsRequired()
                .HasColumnType("datetime");

        }
    }
}
