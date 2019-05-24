using System;
using System.Collections.Generic;

namespace FoodCourt.Framework.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int OrderId { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
        public string Description { get; set; }

        public virtual Food Food { get; set; }
        public virtual Order Order { get; set; }
    }
}
