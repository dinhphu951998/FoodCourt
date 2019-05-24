using Doitsu.Service.Core;
using FoodCourt.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodCourt.Framework.ViewModels
{
    public class TransactionViewModel : BaseViewModel<Transaction>
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int CreateBy { get; set; }
        public double? Money { get; set; }
        public string Type { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
