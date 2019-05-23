using System;
using System.Collections.Generic;

namespace FoodCourt.Framework.Models
{
    public partial class Account
    {
        public Account()
        {
            Payment = new HashSet<Payment>();
            Transaction = new HashSet<Transaction>();
        }

        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string Type { get; set; }
        public double? Money { get; set; }
        public int? UserId { get; set; }
        public int? StoreId { get; set; }

        public virtual Store Store { get; set; }
        public virtual MyIdentity User { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
        public virtual ICollection<Transaction> Transaction { get; set; }
    }
}
