using Doitsu.Service.Core;
using FoodCourt.Framework.Constants;
using FoodCourt.Framework.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.ViewModels
{
    public class FoodViewModel : BaseViewModel<Food>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateTime { get; set; }
        public double? Price { get; set; }
        public bool? Activated { get; set; }
        public int? StoreId { get; set; }
    }
}