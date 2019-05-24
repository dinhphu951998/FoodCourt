using System;
using System.Collections.Generic;

namespace FoodCourt.Framework.Models
{
    public partial class UserStore
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? StoreId { get; set; }

        public virtual Store Store { get; set; }
        public virtual MyIdentity User { get; set; }
    }
}
