using System;
using System.Collections.Generic;

namespace FoodCourt.Framework.Models
{
    public partial class Wallet
    {
        public Wallet()
        {
            MoneyTransaction = new HashSet<MoneyTransaction>();
        }

        public int Id { get; set; }
        public int? Money { get; set; }
        public DateTime? UpdatedTime { get; set; }

        public virtual Users IdNavigation { get; set; }
        public virtual ICollection<MoneyTransaction> MoneyTransaction { get; set; }
    }
}
