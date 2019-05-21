using System;
using System.Collections.Generic;

namespace FoodCourt.Framework.Models
{
    public partial class Account
    {
        public Account()
        {
            MoneyTransaction = new HashSet<MoneyTransaction>();
            PurchasedTransaction = new HashSet<PurchasedTransaction>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public double? Money { get; set; }
        public int? UserId { get; set; }
        public int? StoreId { get; set; }

        public virtual Store Store { get; set; }
        public virtual MyIdentity User { get; set; }
        public virtual ICollection<MoneyTransaction> MoneyTransaction { get; set; }
        public virtual ICollection<PurchasedTransaction> PurchasedTransaction { get; set; }
    }
}
