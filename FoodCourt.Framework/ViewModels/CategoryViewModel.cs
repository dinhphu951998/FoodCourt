using Doitsu.Service.Core;
using FoodCourt.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.ViewModels
{
    public class CategoryViewModel : BaseViewModel<Category>
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
