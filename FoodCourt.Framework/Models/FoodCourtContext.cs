using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FoodCourt.Framework.Models
{
    public partial class FoodCourtContext : IdentityDbContext<MyIdentity, IdentityRole<int>, int>
    {
        public FoodCourtContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MyIdentity>().ToTable("Users")
                                           .Ignore(c => c.LockoutEnabled)
                                           .Ignore(c => c.NormalizedEmail)
                                           .Ignore(c => c.NormalizedUserName)
                                           .Ignore(c => c.LockoutEnd)
                                           .Ignore(c => c.PhoneNumberConfirmed)
                                           .Ignore(c => c.SecurityStamp)
                                           .Ignore(c => c.ConcurrencyStamp)
                                           .Ignore(c => c.EmailConfirmed)
                                           .Ignore(c => c.TwoFactorEnabled);

            builder.Entity<MyIdentity>().Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Entity<IdentityRole<int>>().ToTable("Roles")
                                                .Ignore(r => r.ConcurrencyStamp)
                                                .Ignore(r => r.NormalizedName);

            builder.Entity<IdentityRole>().Property(c => c.Id).ValueGeneratedOnAdd();

            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
        }
    }
}
