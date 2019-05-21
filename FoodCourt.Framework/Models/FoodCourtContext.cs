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

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Food> Food { get; set; }
        public virtual DbSet<MoneyTransaction> MoneyTransaction { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<PurchasedTransaction> PurchasedTransaction { get; set; }
        public virtual DbSet<Store> Store { get; set; }

    }
}
