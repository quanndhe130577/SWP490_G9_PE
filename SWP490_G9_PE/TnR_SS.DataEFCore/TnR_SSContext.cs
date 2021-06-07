using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TnR_SS.DataEFCore.Configurations;
using TnR_SS.Domain.Entities;

#nullable disable

namespace TnR_SS.DataEFCore
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
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (options.IsConfigured)
            {
                options.UseSqlServer("Server = localhost; Database = TnR_SS; Trusted_Connection = True;", builder => builder.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            new OTPConfiguration(modelBuilder.Entity<OTP>());
            new RoleUserConfiguration(modelBuilder.Entity<RoleUser>());
            new UserInforConfiguration(modelBuilder.Entity<UserInfor>());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
