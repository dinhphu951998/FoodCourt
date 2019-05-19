using System;
using System.Collections.Generic;

namespace FoodCourt.Framework.Models
{
    public partial class Users
    {
        public Users()
        {
            MoneyTransaction = new HashSet<MoneyTransaction>();
            Order = new HashSet<Order>();
            PurchasedTransaction = new HashSet<PurchasedTransaction>();
            UserRoles = new HashSet<UserRoles>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public int AccessFailedCount { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool? Activated { get; set; }
        public DateTime? BirthDate { get; set; }

        public virtual Wallet Wallet { get; set; }
        public virtual ICollection<MoneyTransaction> MoneyTransaction { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<PurchasedTransaction> PurchasedTransaction { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
