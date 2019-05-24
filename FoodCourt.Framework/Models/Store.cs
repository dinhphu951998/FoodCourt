using System;
using System.Collections.Generic;

namespace FoodCourt.Framework.Models
{
    public partial class Store
    {
        public Store()
        {
            Food = new HashSet<Food>();
            Order = new HashSet<Order>();
            UserStore = new HashSet<UserStore>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool Activated { get; set; }
        public string Logo { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<Food> Food { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<UserStore> UserStore { get; set; }
    }
}
