using Doitsu.Service.Core;
using FoodCourt.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.ViewModels
{
    public class StoreViewModel : BaseViewModel<Store>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? Activated { get; set; }
    }
}
