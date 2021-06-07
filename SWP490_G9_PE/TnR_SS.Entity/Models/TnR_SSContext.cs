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
        public virtual DbSet<OTP> OTPs { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer("Server=localhost;Database=TnR_SS;Trusted_Connection=True;");*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            var listRole = new RoleUser[]
            {
                new RoleUser(){ Id = 1, Name = "Admin", DisplayName = "Admin", NormalizedName = "ADMIN"},
                new RoleUser(){ Id = 2, Name = "Trader", DisplayName = "Thương lái", NormalizedName = "TRADER"},
                new RoleUser(){ Id = 3, Name = "Weight Recorder", DisplayName = "Chủ bến", NormalizedName = "WEIGHT RECORDER"},
            };

            modelBuilder.Entity<RoleUser>(entity =>
            {
                entity.HasData(listRole);
            });

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

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                /*entity.Property(e => e.RoleId).HasColumnName("RoleID");


                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserInfors)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserInfor__RoleUser");*/

                //entity.HasData(listUser);
            });

            modelBuilder.Entity<OTP>(entity =>
            {
                entity.ToTable("OTP");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.ExpiredDate).HasColumnType("datetime");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .IsFixedLength(true);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(12)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired();


                /*entity.Property(e => e.RoleId).HasColumnName("RoleID");


                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserInfors)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserInfor__RoleUser");*/

                //entity.HasData(listUser);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
