using Doitsu.Service.Core;
using FoodCourt.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.ViewModels
{
    public class OrderViewModel : BaseViewModel<Order>
    {
        public int Id { get; set; }
        public double? Total { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? Activated { get; set; }
        public DateTime? ReceiveTime { get; set; }
        public int? UserId { get; set; }

    }
}
