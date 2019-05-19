using System;
using System.Collections.Generic;

namespace FoodCourt.Framework.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public double? Total { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? Activated { get; set; }
        public DateTime? ReceiveTime { get; set; }
        public int? UserId { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
