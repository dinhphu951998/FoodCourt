using System;
using System.Collections.Generic;

namespace FoodCourt.Framework.Models
{
    public partial class MoneyTransaction
    {
        public int WalletId { get; set; }
        public double? Money { get; set; }
        public string Type { get; set; }
        public DateTime? CreateTime { get; set; }
        public int CreateBy { get; set; }

        public virtual Users CreateByNavigation { get; set; }
        public virtual Wallet Wallet { get; set; }
    }
}
