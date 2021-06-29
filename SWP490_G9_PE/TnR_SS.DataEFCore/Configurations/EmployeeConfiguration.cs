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
    public class EmployeeConfiguration
    {
        public EmployeeConfiguration(EntityTypeBuilder<Employee> entity)
        {
            entity.ToTable("Employee");

            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            /*entity.Property(e => e.LastName)
                .HasMaxLength(50);*/

            entity.Property(e => e.DOB)
                .HasColumnType("date");

            entity.Property(e => e.Address)
                .HasMaxLength(50);

            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.TraderId)
                .IsRequired();

            entity.HasOne(p => p.UserInfor)
                .WithMany(b => b.Employees)
                .HasForeignKey(p => p.TraderId)
                .HasConstraintName("FK_Employee_UserInfor");

            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime");

            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime");
        }
    }
}
