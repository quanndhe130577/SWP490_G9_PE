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
    public class UserInforConfiguration
    {
        public UserInforConfiguration(EntityTypeBuilder<UserInfor> entity)
        {

            entity.ToTable("UserInfor");

            entity.HasIndex(e => new { e.Id, e.PhoneNumber }, "UC_PhoneNumber")
                .IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ_PhoneNumber")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Avatar).IsUnicode(false);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.IdentifyCode)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Lastname)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(12)
                .IsUnicode(false);

        }
    }
}
