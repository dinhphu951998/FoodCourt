using System;
using System.Collections.Generic;

namespace FoodCourt.Framework.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public DateTime? CreateTime { get; set; }
        public double? Money { get; set; }
        public int CreateBy { get; set; }
        public int AccountId { get; set; }
        public int OrderId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Order Order { get; set; }
    }
}
