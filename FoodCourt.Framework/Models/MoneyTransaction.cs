using System;
using System.Collections.Generic;

namespace FoodCourt.Framework.Models
{
    public partial class MoneyTransaction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int CreateBy { get; set; }
        public double? Money { get; set; }
        public string Type { get; set; }
        public DateTime? CreateTime { get; set; }

        public virtual Account Account { get; set; }
        public virtual MyIdentity CreateByNavigation { get; set; }
    }
}
