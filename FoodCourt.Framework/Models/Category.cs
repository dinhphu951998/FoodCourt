using System;
using System.Collections.Generic;

namespace FoodCourt.Framework.Models
{
    public partial class Category
    {
        public Category()
        {
            Food = new HashSet<Food>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        public virtual ICollection<Food> Food { get; set; }
    }
}
