using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TnR_SS.Entity.Models
{
    public partial class TnR_SSContext : IdentityDbContext<UserInfor, RoleUser, int>
    {
        public TnR_SSContext()
        {
        }

        public TnR_SSContext(DbContextOptions<TnR_SSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<RoleUser> RoleUsers { get; set; }
        public virtual DbSet<UserInfor> UserInfors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            var listRole = new RoleUser[]
            {
                new RoleUser(){ Id = 1, DisplayName = "Admin", RoleName = "Admin"},
                new RoleUser(){ Id = 2, DisplayName = "Trader", RoleName = "Trader"},
                new RoleUser(){ Id = 3, DisplayName = "Weight Recorder", RoleName = "Weight Recorder"},
            };

            modelBuilder.Entity<RoleUser>(entity =>
            {
                entity.ToTable("RoleUser");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasData(listRole);
            });

            /*var listUser = new UserInfor[]
            {
                *//*new UserInfor(){ Id = 1, Avatar = null, FirstName = "Nguyen", Lastname = "Quan", RoleId = 2, PhoneNumber = "0966848112" , CreatedDate = new DateTime(2021,5,1), Dob = new DateTime(1999,10,21), IdentifyCode = "123456789", SaltPassword = "qwertyuiopasdfghjklz", Password = "1a56be4be3e34472001aa7e5f5fc5cbe84428edfe902bdf1508fcf33ff517198", SecurityStamp = Guid.NewGuid().ToString()},
                new UserInfor(){ Id = 2, Avatar = null, FirstName = "Anh", Lastname = "Duc", RoleId = 3, PhoneNumber = "0969360445" , CreatedDate = new DateTime(2021,5,1), Dob = new DateTime(1999,1,1), IdentifyCode = "123456789", SaltPassword = "qwertyuiopasdfghjklz", Password = "1a56be4be3e34472001aa7e5f5fc5cbe84428edfe902bdf1508fcf33ff517198", SecurityStamp = Guid.NewGuid().ToString()},
                new UserInfor(){ Id = 3, Avatar = null, FirstName = "Admin", Lastname = "Admin", RoleId = 1, PhoneNumber = "admin" , CreatedDate = new DateTime(2021,5,1), Dob = new DateTime(1999,10,21), IdentifyCode = "123456789", SaltPassword = "qwertyuiopasdfghjklz", Password = "f781bfeada73d5d4d703dca8c3b1b0eba6aa49151ac0fcfaa5d10510eaecdfd3", SecurityStamp = Guid.NewGuid().ToString()},*//*
            };*/

            modelBuilder.Entity<UserInfor>(entity =>
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

                /*entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);*/

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                /*entity.Property(e => e.SaltPassword)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);*/

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserInfors)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserInfor__RoleUser");

                //entity.HasData(listUser);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
