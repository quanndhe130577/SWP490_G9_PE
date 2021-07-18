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
    public class EmployeeDebtConfiguration
    {
        public EmployeeDebtConfiguration(EntityTypeBuilder<EmployeeDebt> entity)
        {
            entity.ToTable("EmployeeDebt");

            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.EmpId)
                .IsRequired();

            entity.HasOne(p => p.Employee)
                .WithMany(b => b.EmployeeDebts)
                .HasForeignKey(p => p.EmpId)
                .HasConstraintName("FK_EmployeeDebt_Employee");

            entity.Property(e => e.Date)
                .IsRequired()
                .HasColumnType("datetime");

            entity.Property(e => e.Debt)
                .IsRequired();

            entity.Property(e => e.Paid)
           .IsRequired().HasDefaultValue(false);
        }
    }
}
