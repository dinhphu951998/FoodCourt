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
        [Required]
        public string Name { get; set; }
        public DateTime? CreateTime { get; set; }
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = ErrorMessage.PRICE_NOT_VALID)]
        public double? Price { get; set; }
        public bool? Activated { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int? StoreId { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int? CategoryId { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        public string Description { get; set; }
    }
    
}