using System;
using System.Collections.Generic;

namespace FoodCourt.Framework.Models
{
    public partial class Food
    {
        public Food()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateTime { get; set; }
        public double? Price { get; set; }
        public bool? Activated { get; set; }
        public int? StoreId { get; set; }

        public virtual Store Store { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
