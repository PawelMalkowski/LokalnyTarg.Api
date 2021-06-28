using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LokalnyTarg.Data.Identity
{
    public class LokalnyTargIdentityContext: IdentityDbContext
    {
        public LokalnyTargIdentityContext(DbContextOptions<LokalnyTargIdentityContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUser>(entity =>
            {
                entity.Property(m => m.Id).HasMaxLength(127);
                entity.Property(m => m.Email).HasMaxLength(127);
                entity.Property(m => m.NormalizedEmail).HasMaxLength(127);
                entity.Property(m => m.NormalizedUserName).HasMaxLength(127);
                entity.Property(m => m.UserName).HasMaxLength(127);
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.Property(m => m.Id).HasMaxLength(127);
                entity.Property(m => m.Name).HasMaxLength(127);
                entity.Property(m => m.NormalizedName).HasMaxLength(127);
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.Property(m => m.ProviderDisplayName).HasMaxLength(63);
                entity.Property(m => m.LoginProvider).HasMaxLength(63);
                entity.Property(m => m.ProviderKey).HasMaxLength(63);
                entity.Property(m => m.UserId).HasMaxLength(127);
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(63);
                entity.Property(m => m.RoleId).HasMaxLength(63);
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(m => m.UserId).HasMaxLength(63);
                entity.Property(m => m.LoginProvider).HasMaxLength(63);
                entity.Property(m => m.Name).HasMaxLength(63);
            });

        }
    }
}
